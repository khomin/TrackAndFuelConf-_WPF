using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Windows;
using System.Windows.Controls;
using TrackAndFuel.Instrumentals;
using TrackAndFuel.ViewModel;

namespace TrackAndFuel.Tracker
{
    /// <summary>
    /// Interaction logic for TrackerMainPanel.xaml
    /// </summary>
    public partial class TrackerMainPanel : Page
    {
        private MainViewModel viewModel;
        private TrackerDataPortAbstract _dataPort;

        public TrackerMainPanel(MainViewModel viewModel)
        {
            InitializeComponent();

            this.viewModel = viewModel;
            DataContext = viewModel;

            connectToPort("Demo");
        }

        private void connectToPort(string portName)
        {
            Dictionary<string, object> connectProperty = new Dictionary<string, object>();

            if (portName.Contains("Demo"))
            {
                _dataPort = new TrackerSimulationPort();
            }
            else
            {
                _dataPort = new TrackerSerialPort(portName, 115200, Parity.None, 8, StopBits.One);
            }

            _dataPort.Open(connectProperty, (newData) =>
            {
                /* updating of new data */
                ReceiveData(newData);
            }, () =>
            {
                /* disconnect callback */
                DisconnectHandle();
            });

            viewModel.ConnectViewModel.LoadingViewIsShow = Visibility.Visible;
            viewModel.ConnectViewModel.StatusConnect = "Connecting";
        }

        private void DisconnectHandle()
        {
            viewModel.ConnectViewModel.StatusConnect = "Disconnected";
            viewModel.ConnectViewModel.IsConnected = false;
            this.Dispatcher.Invoke(() =>
            {
                viewModel.ConnectViewModel.IsConnected = false;
                viewModel.ConnectViewModel.LoadingViewIsShow = Visibility.Visible;
                viewModel.ConnectViewModel.StatusConnect = "Disconnected";
                MessageBoxResult result = MessageBox.Show("Connection lost",
                                      "Warning",
                                      MessageBoxButton.OK,
                                      MessageBoxImage.Warning);
            });
        }

        private void ReceiveData(List<int> data)
        {
            viewModel.ConnectViewModel.IsConnected = true;
            viewModel.ConnectViewModel.LoadingViewIsShow = Visibility.Hidden;
            viewModel.ConnectViewModel.StatusConnect = "Connected";

            TrackerParserData parserData = new TrackerParserData();
            var result = parserData.Parse(data);

            switch (result.type)
            {
                case TrackerTypeData.TypePacketData.Request:
                    break;
                case TrackerTypeData.TypePacketData.Answer:
                    break;
                case TrackerTypeData.TypePacketData.AsyncData:
                    foreach (var i in result.data)
                    {
                        switch (i.Key)
                        {
                            case TrackerTypeData.KeyParameter.DbgLevel:
                                if (i.Data.GetType() == typeof(int))
                                {
                                    Console.WriteLine(i.Data);
                                }
                                else if (i.Data.GetType() == typeof(bool))
                                {
                                    Console.WriteLine(i.Data);
                                }
                                else if (i.Data.GetType() == typeof(Single))
                                {
                                    Console.WriteLine(i.Data);
                                }
                                else if (i.Data.GetType() == typeof(string))
                                {
                                    Console.WriteLine(i.Data);
                                }
                                else
                                {

                                }
                                break;

                            case TrackerTypeData.KeyParameter.DbgMessage:
                                App.Current.Dispatcher.Invoke((Action)delegate
                                {
                                    viewModel.RightPannelModel.StatusInfo.Log.Add(item: new StatusDataViewModel.LogItem { Message = i.Data.ToString(), Type = i.Key });
                                });
                                break;
                        }
                    }
                    break;
            }
        }

        private void TrackerConnectPannel_disconnectEvent(object sender, EventArgs e)
        {
            _dataPort.Close();
        }
    }
}
