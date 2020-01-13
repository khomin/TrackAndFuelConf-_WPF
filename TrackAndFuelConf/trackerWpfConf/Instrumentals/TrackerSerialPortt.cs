using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace trackerWpfConf.Instrumentals
{
    class TrackerSerialPortt
    {
        static SerialPort _serialPort;
        private List<byte[]> dataOutBuff;
        
        Action<List<int>> dataReceivedCallback;
        Action disconnectPortErrorCallback;

        private bool _serialIsActive = false;
        private System.Timers.Timer _timerDisconnectControl;

        public TrackerSerialPortt(string name, int baudrate, Parity parity, int dataBits, StopBits stopBits, Action<List<int>> dataReceivedCallback, Action disconnectPortErrorCallback)
        {
            _serialPort = new SerialPort(name, baudrate, parity, dataBits, stopBits);
            this.dataReceivedCallback = dataReceivedCallback;
            this.disconnectPortErrorCallback = disconnectPortErrorCallback;
        }

        public void close()
        {
            _timerDisconnectControl.Stop();
            _serialPort.Close();
            _serialIsActive = false;
        }

        public bool open()
        {
            bool result = false;
            try {
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
                        }
                        catch (TimeoutException)
                        {
                            readyRead = true;
                        }
                    } while (readyRead);

                    dataReceivedCallback.Invoke(rxData);
                });

                dataOutBuff = new List<byte[]>();

                // Begin communications
                _serialPort.Open();
                result = true;

                _serialIsActive = true;

                _timerDisconnectControl = new System.Timers.Timer(1500);
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
                        close();
                        disconnectPortErrorCallback.Invoke();
                    }
                };
            }
            catch (Exception) 
            {
                Console.WriteLine("SerialPort: exception " + this.GetType().FullName + "." + System.Reflection.MethodBase.GetCurrentMethod().Name);
            }

            if (!result) 
            {
                disconnectPortErrorCallback.Invoke();
            }

            return result;
        }
    }
}
