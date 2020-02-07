using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
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
        private System.Timers.Timer handleRequestTimer;
        public TrackerMainPanel(MainViewModel viewModel, string portName)
        {
            InitializeComponent();
            _viewModel = viewModel;
            DataContext = viewModel;
            connectToPort(portName);

            handleRequestTimer = new System.Timers.Timer(2000);
            handleRequestTimer.AutoReset = true;
            handleRequestTimer.Enabled = true;
            handleRequestTimer.Elapsed += (sourse, evendt) =>
            {
                if (_viewModel.ConnectViewModel.CommandDataBuf.Count != 0)
                {
                    var index = _viewModel.ConnectViewModel.CommandDataBuf.Count - 1;
                    ConnectPanelViewModel.CommandData i = _viewModel.ConnectViewModel.CommandDataBuf[index];
                    if (_dataPort != null)
                    {
                        if (_dataPort.WriteData(i.key, i.data))
                        {
                            _viewModel.ConnectViewModel.CommandDataBuf.RemoveAt(index);
                        }
                    }
                }
            };
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
            _viewModel.ConnectViewModel.IsConnected = false;
            _viewModel.ConnectViewModel.LoadingViewIsShow = Visibility.Visible;
            _viewModel.ConnectViewModel.StatusConnect = "Disconnected";
            this.Dispatcher.Invoke(() =>
            {
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
                    foreach (var i in result.data)
                    {
                        switch (i.Key)
                        {
                            case TrackerTypeData.KeyParameter.SettingsConnections:
                            case TrackerTypeData.KeyParameter.SettingsOneWire:
                            case TrackerTypeData.KeyParameter.SettingsTrack:
                            case TrackerTypeData.KeyParameter.SettingsGeofence:
                            case TrackerTypeData.KeyParameter.SettingsInputs:
                            case TrackerTypeData.KeyParameter.SettingsSms:
                            case TrackerTypeData.KeyParameter.SettingsLls:
                            case TrackerTypeData.KeyParameter.SettingsCalibration:
                            case TrackerTypeData.KeyParameter.SettingsAcknowledgement:
                            case TrackerTypeData.KeyParameter.SettingsAll:
                                break;
                            default: break;
                        }
                    }
                    //_viewModel.ConnectViewModel.IsReadyReadWriteSettings = true;
                    Application.Current.Dispatcher.Invoke(delegate
                    {
                        MessageBox.Show("Settings have been recorded",
                              "Ok",
                              MessageBoxButton.OK,
                              MessageBoxImage.Information);
                    });
                }
                else
                {
                    if (result.type == TrackerTypeData.TypePacketData.AsyncData)
                    {
                        switch (result.typeMessage)
                        {
                            case TrackerTypeData.TypeMessage.Debug:
                            case TrackerTypeData.TypeMessage.Status:
                            case TrackerTypeData.TypeMessage.Data:
                            case TrackerTypeData.TypeMessage.AsyncData:
                            case TrackerTypeData.TypeMessage.Log:
                                ParseDataToViewModel(result.data);
                                break;
                            case TrackerTypeData.TypeMessage.SettingsRead:
                                break;
                            case TrackerTypeData.TypeMessage.SettignsWrite:
                                break;
                            case TrackerTypeData.TypeMessage.LogCleanFinish:
                                _viewModel.ConnectViewModel.IsWaitsForLogClear = false;
                                MessageBox.Show("The log has been cleared",
                                    "Ok",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Information
                                );
                                break;
                            case TrackerTypeData.TypeMessage.Undefined:
                                throw new InvalidOperationException("Invalid type");
                                break;
                        }
                    }
                    else
                    {
                        throw new System.InvalidOperationException("Undefined type of key");
                    }
                }
            }
            else
            {
                Console.WriteLine("Parser data error");
            }
        }

        private void ParseDataToViewModel(List<DataItemParam> data)
        {
            foreach (var i in data)
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
                    case TrackerTypeData.KeyParameter.LogRecord:
                        Application.Current.Dispatcher.Invoke(delegate
                        {
                            StructureBinaryConverter structureBinaryConverter = new StructureBinaryConverter();
                            var log = structureBinaryConverter.fromBytes((byte[])i.Data);
                            _viewModel.RightPannelModel.CurrentData.LogPositionList.Add(new CurrentDataViewModel.LogPoint(log.Id, log.GnssLongitude, log.GnssLatitude, DateTime.Now));
                            if (!DrawMap(log.GnssLatitude, log.GnssLongitude))
                            {
                                _viewModel.ConnectViewModel.IsLogReading = false;
                                _viewModel.ConnectViewModel.CommandDataBuf.Add(new ConnectPanelViewModel.CommandData("stopTestLog", new byte[0]));
                                MessageBox.Show("It is not possible to read more than 500 items", "Information", MessageBoxButton.OK);
                            }
                        });
                        break;
                }
            }
        }

        private void TrackerConnectPannel_disconnectEvent(object sender, EventArgs e)
        {
            _dataPort.Close();
            _viewModel.ConnectViewModel.IsConnected = false;
            _viewModel.ConnectViewModel.StatusConnect = "Disconnected";
        }

        private void TrackerConnectPannel_loadConfigEvent(object sender, EventArgs e)
        {
            var parser = new TrackerParserData();
            var data = new List<byte>();
            data.Add((int)TrackerTypeData.TypePacketData.Request);
            data.AddRange(parser.addParam(new DataItemParam { Key = TrackerTypeData.KeyParameter.SettingsAll, Type = typeof(int), Data = 0 }));
            data.Add(Crc8Calc.ComputeChecksum(data.ToArray()));
            _dataPort.WriteData("writeSettings", data.ToArray());
            //_viewModel.ConnectViewModel.IsReadyReadWriteSettings = false;
        }

        private void TrackerConnectPannel_saveConfigEvent(object sender, EventArgs e)
        {
            var parser = new TrackerParserData();
            var data = new List<byte>();
            data.Add((int)TrackerTypeData.TypePacketData.Request);
            data.AddRange(parser.addParam(new DataItemParam { Key = TrackerTypeData.KeyParameter.SettingsCalibration, Type = typeof(byte[]), Data = new byte[10] }));
            data.AddRange(parser.addParam(new DataItemParam { Key = TrackerTypeData.KeyParameter.SettingsConnections, Type = typeof(byte[]), Data = new byte[10] }));
            data.AddRange(parser.addParam(new DataItemParam { Key = TrackerTypeData.KeyParameter.SettingsGeofence, Type = typeof(byte[]), Data = new byte[10] }));
            data.AddRange(parser.addParam(new DataItemParam { Key = TrackerTypeData.KeyParameter.SettingsInputs, Type = typeof(byte[]), Data = new byte[10] }));
            data.AddRange(parser.addParam(new DataItemParam { Key = TrackerTypeData.KeyParameter.SettingsLls, Type = typeof(byte[]), Data = new byte[10] }));
            data.AddRange(parser.addParam(new DataItemParam { Key = TrackerTypeData.KeyParameter.SettingsOneWire, Type = typeof(byte[]), Data = new byte[10] }));
            data.AddRange(parser.addParam(new DataItemParam { Key = TrackerTypeData.KeyParameter.SettingsSms, Type = typeof(byte[]), Data = new byte[10] }));
            data.AddRange(parser.addParam(new DataItemParam { Key = TrackerTypeData.KeyParameter.SettingsTrack, Type = typeof(byte[]), Data = new byte[10] }));
            data.Add(Crc8Calc.ComputeChecksum(data.ToArray()));
            _dataPort.WriteData("writeSettings", data.ToArray());
            //_viewModel.ConnectViewModel.IsReadyReadWriteSettings = false;
        }

        private List<Tuple<double, double>> _mapList = new List<Tuple<double, double>>();
        private bool DrawMap(double lat, double lon)
        {
            bool result = false;
            LocationCollection locs = new LocationCollection();
            if (_mapList.Count < 500)
            {
                _mapList.Add(new Tuple<double, double>(lat, lon));
                _viewModel.RightPannelModel.CurrentData.Map.Children.Clear();
                foreach (var point in _mapList)
                {
                    locs.Add(new Microsoft.Maps.MapControl.WPF.Location(point.Item1, point.Item2));

                    MapPolyline routeLine = new MapPolyline()
                    {
                        Locations = locs,
                        Stroke = new SolidColorBrush(Colors.Blue),
                        StrokeThickness = 5
                    };
                    _viewModel.RightPannelModel.CurrentData.Map.Children.Add(routeLine);
                    result = true;
                }
            }
            return result;
        }
    }
}
