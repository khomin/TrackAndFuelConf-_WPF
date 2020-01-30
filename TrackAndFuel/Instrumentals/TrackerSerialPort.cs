using System;
using System.Collections.Generic;
using System.IO.Ports;

namespace TrackAndFuel.Instrumentals
{
    class TrackerSerialPort : TrackerDataPortAbstract
    {
        static SerialPort _serialPort;        
        Action disconnectCallback;

        private bool _serialIsActive = false;
        private System.Timers.Timer _timerDisconnectControl;

        public TrackerSerialPort(string name, int baudrate, Parity parity, int dataBits, StopBits stopBits)
        {
            _serialPort = new SerialPort(name, baudrate, parity, dataBits, stopBits);
        }

        public override void Close()
        {
            _timerDisconnectControl.Stop();
            _serialPort.Close();
            _serialIsActive = false;
        }

        public override bool Open(Dictionary<string, object> property, Action<List<int>> updateDataCallback, Action disconnectCallback)
        {
            bool result = false;
            try
            {
                // Attach a method to be called when there
                // is data waiting in the port's buffer
                _serialPort.DataReceived += new SerialDataReceivedEventHandler((o, i) =>
                {
                    bool readyRead = false;
                    List<int> rxData = new List<int>();
                    do
                    {
                        try
                        {
                            int data = _serialPort.ReadByte();
                            rxData.Add(data);
                            _serialIsActive = true;
                        }
                        catch (TimeoutException)
                        {
                            readyRead = true;
                        }
                    } while (readyRead);

                    updateDataCallback.Invoke(rxData);
                });

                // Begin communications
                _serialPort.Open();
                result = true;

                _timerDisconnectControl = new System.Timers.Timer(3000);
                _timerDisconnectControl.AutoReset = true;
                _timerDisconnectControl.Enabled = true;
                _timerDisconnectControl.Elapsed += (sourse, e) =>
                {
                    if (_serialIsActive)
                    {
                        _serialIsActive = false;
                    }
                    else
                    {
                        Close();
                        disconnectCallback.Invoke();
                    }
                };
            }
            catch (Exception)
            {
                Console.WriteLine("SerialPort: exception " + this.GetType().FullName + "." + System.Reflection.MethodBase.GetCurrentMethod().Name);
            }

            if (!result)
            {
                disconnectCallback.Invoke();
            }

            return result;
        }

        public override bool WriteData(byte[] data)
        {
            throw new NotImplementedException();
        }
    }
}
