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
        private MainViewModel _viewModel;
        private TrackerDataPortAbstract _dataPort;

        public TrackerMainPanel(MainViewModel viewModel, string portName)
        {
            InitializeComponent();
            _viewModel = viewModel;
            DataContext = viewModel;
            connectToPort(portName);
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

            _viewModel.ConnectViewModel.LoadingViewIsShow = Visibility.Visible;
            _viewModel.ConnectViewModel.StatusConnect = "Connecting";
        }

        private void DisconnectHandle()
        {
            _viewModel.ConnectViewModel.StatusConnect = "Disconnected";
            _viewModel.ConnectViewModel.IsConnected = false;
            this.Dispatcher.Invoke(() =>
            {
                _viewModel.ConnectViewModel.IsConnected = false;
                _viewModel.ConnectViewModel.LoadingViewIsShow = Visibility.Visible;
                _viewModel.ConnectViewModel.StatusConnect = "Disconnected";
                MessageBoxResult result = MessageBox.Show("Connection lost",
                                      "Warning",
                                      MessageBoxButton.OK,
                                      MessageBoxImage.Warning);
            });
        }

        private void ReceiveData(List<byte> data)
        {
            _viewModel.ConnectViewModel.IsConnected = true;
            _viewModel.ConnectViewModel.LoadingViewIsShow = Visibility.Hidden;
            _viewModel.ConnectViewModel.StatusConnect = "Connected";

            TrackerParserData parserData = new TrackerParserData();
            var result = parserData.Parse(data.ToArray());

            if (result != null)
            {
                if (result.type == TrackerTypeData.TypePacketData.Answer)
                {

                }
                else
                {
                    if (result.type == TrackerTypeData.TypePacketData.AsyncData)
                    {
                        foreach (var i in result.data)
                        {

                            switch (i.Key)
                            {
                                case TrackerTypeData.KeyParameter.DbgLevel:
                                    Application.Current.Dispatcher.Invoke(delegate
                                    {
                                        _viewModel.RightPannelModel.StatusInfo.Log.Add(item: new StatusDataViewModel.LogItem { Message = i.Data.ToString(), Type = i.Key });
                                    });
                                    break;
                                case TrackerTypeData.KeyParameter.DbgMessage:
                                    break;
                                case TrackerTypeData.KeyParameter.imei:
                                    Application.Current.Dispatcher.Invoke(delegate
                                    {
                                        _viewModel.RightPannelModel.CurrentData.ImeiModemValue = i.Data.ToString();
                                    });
                                    break;
                                case TrackerTypeData.KeyParameter.imsi:
                                    break;
                                case TrackerTypeData.KeyParameter.GsmCsq:
                                    Application.Current.Dispatcher.Invoke(delegate
                                    {
                                        _viewModel.RightPannelModel.CurrentData.GmsSignalStrenghtPercentValue = (int)i.Data;
                                    });
                                    break;
                                case TrackerTypeData.KeyParameter.GnssLat:
                                    Application.Current.Dispatcher.Invoke(delegate
                                    {
                                        _viewModel.RightPannelModel.CurrentData.GnssLatValue = (float)i.Data;
                                    });
                                    break;
                                case TrackerTypeData.KeyParameter.GnssLon:
                                    Application.Current.Dispatcher.Invoke(delegate
                                    {
                                        _viewModel.RightPannelModel.CurrentData.GnssLonValue = (float)i.Data;
                                    });
                                    break;
                                case TrackerTypeData.KeyParameter.GnssSat:
                                    Application.Current.Dispatcher.Invoke(delegate
                                    {
                                        _viewModel.RightPannelModel.CurrentData.GnssSatFixValue = (int)i.Data;
                                    });
                                    break;
                                case TrackerTypeData.KeyParameter.Ain1:
                                    Application.Current.Dispatcher.Invoke(delegate
                                    {
                                        _viewModel.RightPannelModel.CurrentData.Ain1Value = (float)i.Data;
                                    });
                                    break;
                                case TrackerTypeData.KeyParameter.Ain2:
                                    Application.Current.Dispatcher.Invoke(delegate
                                    {
                                        _viewModel.RightPannelModel.CurrentData.Ain2Value = (float)i.Data;
                                    });
                                    break;
                                case TrackerTypeData.KeyParameter.Ain3:
                                    Application.Current.Dispatcher.Invoke(delegate
                                    {
                                        _viewModel.RightPannelModel.CurrentData.Ain3Value = (float)i.Data;
                                    });
                                    break;
                                case TrackerTypeData.KeyParameter.PowerBat:
                                    Application.Current.Dispatcher.Invoke(delegate
                                    {
                                        _viewModel.RightPannelModel.CurrentData.PowerBatteryValue = (float)i.Data;
                                    });
                                    break;
                                case TrackerTypeData.KeyParameter.PowerExt:
                                    Application.Current.Dispatcher.Invoke(delegate
                                    {
                                        _viewModel.RightPannelModel.CurrentData.PowerExternalValue = (float)i.Data;
                                    });
                                    break;
                            }
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("Parser data error");
            }
        }

        public void sendData()
        {


        }

        private void TrackerConnectPannel_disconnectEvent(object sender, EventArgs e)
        {
            _dataPort.Close();
        }
    }
}
