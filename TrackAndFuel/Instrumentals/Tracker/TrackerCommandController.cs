using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TrackAndFuel.ViewModel;

namespace TrackAndFuel.Instrumentals.Tracker
{
    class TrackerCommandController
    {
        private TrackerDataToView dataToView = null;
        private Action<string> errorHandler = null;
        private System.Timers.Timer handleRequestTimer;
        private MainViewModel viewModel = null;
        private TrackerDataPortAbstract dataPort;
        public TrackerCommandController(TrackerDataToView trackerDataToView, Action<string> errorHandler, MainViewModel viewModel, string portName) 
        {
            this.dataToView = trackerDataToView;
            this.errorHandler = errorHandler;
            this.viewModel = viewModel;

            if (portName.Contains("Demo"))
            {
                dataPort = new TrackerSimulationPort();
            }
            else
            {
                dataPort = new TrackerSerialPort(portName, 115200, Parity.None, 8, StopBits.One);
            }

            var isOpen = dataPort.Open((newData) =>
            {
                /* updating of new data */
                ReceiveData(newData);
            }, () =>
            {
                /* disconnect callback */
                DisconnectHandle();
            });

            if (isOpen)
            {
                getSettings();
            }

            viewModel.ConnectViewModel.LoadingViewIsShow = Visibility.Visible;
            viewModel.ConnectViewModel.StatusConnect = "Connecting";

            handleRequestTimer = new System.Timers.Timer(2000);
            handleRequestTimer.AutoReset = true;
            handleRequestTimer.Enabled = true;
            handleRequestTimer.Elapsed += (sourse, evendt) =>
            {
                if(viewModel != null)
                {
                    if (viewModel.ConnectViewModel.CommandDataBuf.Count != 0)
                    {
                        var index = viewModel.ConnectViewModel.CommandDataBuf.Count - 1;
                        ConnectPanelViewModel.CommandData i = viewModel.ConnectViewModel.CommandDataBuf[index];
                        if (dataPort != null)
                        {
                            if (dataPort.WriteData(i.key, i.data))
                            {
                                viewModel.ConnectViewModel.CommandDataBuf.RemoveAt(index);
                            }
                        }
                    }
                }
            };
        }
        public void getSettings()
        {
            var parser = new TrackerParserData();
            var data = new List<byte>();
            data.Add((int)TrackerTypeData.TypePacketData.Request);
            data.Add(0); // size packet L byte
            data.Add(0); // size packet H byte
            data.Add((int)TrackerTypeData.TypeMessage.SettingsRead);
            data.AddRange(parser.addParam(new DataItemParam { Key = TrackerTypeData.KeyParameter.SettingsReadAll, Type = typeof(int), Data = 0 }));
            data[(int)TrackerTypeData.PacketField.PacketLenByteL] = (byte)(data.Count & 0xFF);
            data[(int)TrackerTypeData.PacketField.PacketLenByteH] = (byte)((data.Count & 0xFF00) >> 8);
            var crc = CrcCalc.Crc16(data.ToArray());
            var crcArray = BitConverter.GetBytes(crc);
            data.AddRange(crcArray);

            viewModel.ConnectViewModel.CommandDataBuf.Add(new ConnectPanelViewModel.CommandData("readSettings", data.ToArray()));
        }

        private void DisconnectHandle()
        {
            viewModel.ConnectViewModel.IsConnected = false;
            viewModel.ConnectViewModel.LoadingViewIsShow = Visibility.Visible;
            viewModel.ConnectViewModel.StatusConnect = "Disconnected";
            errorHandler.Invoke("Error");
        }

        private void ReceiveData(byte[] data)
        {
            viewModel.ConnectViewModel.IsConnected = true;
            viewModel.ConnectViewModel.LoadingViewIsShow = Visibility.Hidden;
            viewModel.ConnectViewModel.StatusConnect = "Connected";

            TrackerParserData parserData = new TrackerParserData();
            var result = parserData.Parse(data);
            dataToView.InsertData(result, viewModel);
        }
    }
}
