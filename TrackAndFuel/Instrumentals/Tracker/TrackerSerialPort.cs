﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using System.Threading;
using System.Windows.Documents;

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
            _serialPort.PortName = name;
            _serialPort.BaudRate = baudrate;
            _serialPort.Parity = Parity.None;
            _serialPort.StopBits = StopBits.One;
            _serialPort.DtrEnable = false;
            _serialPort.RtsEnable = false;
            _serialPort.ReadTimeout = 10;
            _serialPort.WriteTimeout = 100;
        }

        public override void Close()
        {
            try
            {
                _timerDisconnectControl.Stop();
                _serialPort.Close();
                _serialIsActive = false;
            }
            catch (Exception) { }
        }

        public override bool Open(Action<byte[]> updateDataCallback, Action disconnectCallback)
        {
            try
            {
                if (_serialPort.IsOpen == true)
                {
                    _serialPort.Close();
                }
                _serialPort.Open();
                _serialPort.DiscardInBuffer();
                _serialIsActive = _serialPort.IsOpen;

                if (_serialIsActive)
                {
                    new Thread(() =>
                    {
                        var data = new byte[65535];
                        int len = 0;
                        while (true)
                        {
                            try
                            {
                                do
                                {
                                    data[len] = (byte)_serialPort.ReadByte();
                                    if (!_serialIsActive)
                                    {
                                        _serialIsActive = true;
                                    }
                                    len += 1;
                                } while (true);
                            }
                            catch (TimeoutException) { }
                            catch (Exception ex) 
                            {
                                Console.WriteLine("TrackerSerialPort: exception" + ex.ToString());
                                disconnectCallback.Invoke();
                                Close();
                                Thread.CurrentThread.Abort();
                            }
                            if (len != 0)
                            {
                                var result = new byte[len];
                                Array.Copy(data, result, len);
                                updateDataCallback.Invoke(result);
                                len = 0;
                                _serialPort.DiscardInBuffer(); // TODO: control throw
                            }
                            Thread.Sleep(1);
                        }
                    }).Start();

                    _timerDisconnectControl = new System.Timers.Timer(3000);
                    _timerDisconnectControl.AutoReset = true;
                    _timerDisconnectControl.Enabled = true;
                    _timerDisconnectControl.Elapsed += (sourse, e) =>
                    {
                        //if (_serialIsActive)
                        //{
                        //    _serialIsActive = false;
                        //}
                        //else
                        //{
                        //    Close();
                        //    disconnectCallback.Invoke();
                        //}
                    };
                }
            }
            catch (Exception)
            {
                Console.WriteLine("SerialPort: exception " + this.GetType().FullName + "." + System.Reflection.MethodBase.GetCurrentMethod().Name);
            }

            if (!_serialIsActive)
            {
                disconnectCallback.Invoke();
            }
            return _serialIsActive;
        }

        public override bool WriteData(string hintDataOptional, byte[] data)
        {
            bool result = true;
            try
            {
                _serialPort.Write(data, 0, data.Length);
            }
            catch (Exception ex)  {}
            return result;
        }
    }
}
