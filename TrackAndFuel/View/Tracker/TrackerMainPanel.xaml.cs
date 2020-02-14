using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using TrackAndFuel.Instrumentals;
using TrackAndFuel.ViewModel;
using static TrackAndFuel.ViewModel.SettingsViewModel;

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
            if (portName.Contains("Demo"))
            {
                _dataPort = new TrackerSimulationPort();
            }
            else
            {
                _dataPort = new TrackerSerialPort(portName, 115200, Parity.None, 8, StopBits.One);
            }

            var isOpen = _dataPort.Open((newData) =>
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
                try
                {
                    _viewModel.NavigateContent.NavigationService.GoBack();
                }
                catch (Exception ex) 
                {
                    Console.WriteLine(ex.ToString());
                }
            });
        }

        private void ReceiveData(byte[] data)
        {
            _viewModel.ConnectViewModel.IsConnected = true;
            _viewModel.ConnectViewModel.LoadingViewIsShow = Visibility.Hidden;
            _viewModel.ConnectViewModel.StatusConnect = "Connected";

            TrackerParserData parserData = new TrackerParserData();
            var result = parserData.Parse(data);
            var converter = new StructureBinaryConverter();

            if (result != null)
            {
                if (result.type == TrackerTypeData.TypePacketData.Answer)
                {
                    if (result.typeMessage == TrackerTypeData.TypeMessage.SettingsRead)
                    {
                        foreach (var i in result.data)
                        {
                            switch (i.Key)
                            {
                                case TrackerTypeData.KeyParameter.SettingsGsm:
                                    Dispatcher.Invoke(() =>
                                    {
                                        TrackerStructureGsm settingsGsm = converter.Deserialize<TrackerStructureGsm>((byte[])i.Data);
                                        _viewModel.SettingsModel.ApnPinCode = Encoding.ASCII.GetString(settingsGsm.PinCode).Trim('\0');
                                        _viewModel.SettingsModel.Apn = Encoding.ASCII.GetString(settingsGsm.Apn).Trim('\0');
                                        _viewModel.SettingsModel.ApnLogin = Encoding.ASCII.GetString(settingsGsm.ApnUser).Trim('\0');
                                        _viewModel.SettingsModel.ApnPassword = Encoding.ASCII.GetString(settingsGsm.ApnPassword).Trim('\0');
                                        var operatorApn = _viewModel.SettingsModel.Apn.ToUpper();
                                        if (operatorApn.Contains("MTS"))
                                        {
                                            _viewModel.SettingsModel.OperatorListIndex = (int)OperatorType.MTS;
                                        }
                                        else if (operatorApn.Contains("BELLINE"))
                                        {
                                            _viewModel.SettingsModel.OperatorListIndex = (int)OperatorType.Beeline;
                                        }
                                        else if (operatorApn.Contains("MEGAFON"))
                                        {
                                            _viewModel.SettingsModel.OperatorListIndex = (int)OperatorType.Megafon;
                                        }
                                        else if (operatorApn.Contains("TELE2"))
                                        {
                                            _viewModel.SettingsModel.OperatorListIndex = (int)OperatorType.Tele2;
                                        }
                                        else
                                        {
                                            _viewModel.SettingsModel.OperatorListIndex = (int)OperatorType.Custom;
                                        }
                                    });
                                    //App
                                    break;
                                case TrackerTypeData.KeyParameter.SettingsServers:
                                    var settingsCon = converter.Deserialize<TrackerStructureSettingsConnection>((byte[])i.Data);
                                    var connectFirst = _viewModel.SettingsModel.ServersConnectionModel[0];
                                    var connectSecond = _viewModel.SettingsModel.ServersConnectionModel[1];
                                    /* first */
                                    //connectFirst.ProtocolTypeIndex = settingsCon.ProtocolType_0;
                                    //connectFirst.IpDnsAddress = Encoding.ASCII.GetString(settingsCon.ConnectAddr_0).Trim('\0');
                                    //connectFirst.Port = settingsCon.ConnectPort_0.ToString();
                                    //connectFirst.UseCompression = settingsCon.ConnectUseCompression_0;
                                    //connectFirst.ConnectPeriodOfPingMessage = (int)settingsCon.ConnectPeriodOfPingMessage_0;
                                    //connectFirst.ConnectDelayBeforeNextConnecting = (int)settingsCon.ConnectDelayBeforeNextConnecting_0;
                                    //connectFirst.ConnectSendingPeropdDuringParking = (int)settingsCon.ConnectSendingPeropdDuringParking_0;
                                    //connectFirst.ConnectSendingPeropdInSleepMode = (int)settingsCon.ConnectSendingPeropdInSleepMode_0;
                                    //connectFirst.AdditionParams = (int)settingsCon.AdditionParams_0;
                                    ///* second */
                                    //connectFirst.ProtocolTypeIndex = settingsCon.ProtocolType_1;
                                    //connectFirst.IpDnsAddress = Encoding.ASCII.GetString(settingsCon.ConnectAddr_1).Trim('\0');
                                    //connectFirst.Port = settingsCon.ConnectPort_1.ToString();
                                    //connectFirst.UseCompression = settingsCon.ConnectUseCompression_1;
                                    //connectFirst.ConnectPeriodOfPingMessage = (int)settingsCon.ConnectPeriodOfPingMessage_1;
                                    //connectFirst.ConnectDelayBeforeNextConnecting = (int)settingsCon.ConnectDelayBeforeNextConnecting_1;
                                    //connectFirst.ConnectSendingPeropdDuringParking = (int)settingsCon.ConnectSendingPeropdDuringParking_1;
                                    //connectFirst.ConnectSendingPeropdInSleepMode = (int)settingsCon.ConnectSendingPeropdInSleepMode_1;
                                    //connectFirst.AdditionParams = (int)settingsCon.AdditionParams_0;
                                    break;
                                case TrackerTypeData.KeyParameter.SettingsSleep:
                                    var settingsSpeep = converter.Deserialize<TrackerStructureSleep>((byte[])i.Data);
                                    break;
                                case TrackerTypeData.KeyParameter.SettingsGpio:
                                    //var settingsGpio = converter.Deserialize<TrackerStructureGPIO>((byte[])i.Data);
                                    //InputItemSettingsModel gpio1 = _viewModel.SettingsModel.InputsSettingsModelList[0];
                                    //InputItemSettingsModel gpio2 = _viewModel.SettingsModel.InputsSettingsModelList[1];
                                    //InputItemSettingsModel gpio3 = _viewModel.SettingsModel.InputsSettingsModelList[2];
                                    //// gpio1
                                    //gpio1.PortRoleIndex = settingsGpio.IN1_Mode;
                                    //gpio1.SignalAnalysysTime = settingsGpio.IN1_timeBase;
                                    //gpio1.ThresholdUpper = settingsGpio.IN1_LowLevelUpperThreshold;
                                    //gpio1.ThresholdLower = settingsGpio.IN1_HightLevelLowerThreshold;
                                    //gpio1.UseFiltrating = settingsGpio.IN1_IsFiltrationEnable;
                                    //gpio1.LevelOfFiltrationValue = settingsGpio.IN1_AveragingFilterLenght;
                                    //// gpio2
                                    //gpio2.PortRoleIndex = settingsGpio.IN2_Mode;
                                    //gpio2.SignalAnalysysTime = settingsGpio.IN2_timeBase;
                                    //gpio2.ThresholdUpper = settingsGpio.IN2_LowLevelUpperThreshold;
                                    //gpio2.ThresholdLower = settingsGpio.IN2_HightLevelLowerThreshold;
                                    //gpio2.UseFiltrating = settingsGpio.IN2_IsFiltrationEnable;
                                    //gpio2.LevelOfFiltrationValue = settingsGpio.IN2_AveragingFilterLenght;
                                    break;
                                case TrackerTypeData.KeyParameter.SettingsLlsInternal:
                                    //var settingsLls = converter.Deserialize<TrackerStructureSettingsLls>((byte[])i.Data);
                                    //var lls1 = _viewModel.SettingsModel.LlsDataViewModelList[0];
                                    //var lls2 = _viewModel.SettingsModel.LlsDataViewModelList[1];
                                    ///* first */
                                    //lls1.MinLevel = settingsLls.MinLevelLls1.ToString();
                                    //lls1.MaxLevel = settingsLls.MaxLevelLls1.ToString();
                                    //lls1.CntEmpty = settingsLls.CntEmptyLls1.ToString();
                                    //lls1.CntFull = settingsLls.CntFullLls1.ToString();
                                    //lls1.TypeOutMessageIsRelativeLevel = settingsLls.OutTypeLls1 == 0 ? false : true;
                                    //lls1.TypeOfFiltrationIndex = settingsLls.FillUpThresholdLls1;
                                    //lls1.FiltrationAveragingTime = settingsLls.AvarageLenghLls1.ToString();
                                    //lls1.FiltrationLenghtOfMedian = settingsLls.MedianLenghtLls1.ToString();
                                    //lls1.FiltrationProcessNoiseCovarianceQ = settingsLls.QLls1.ToString();
                                    //lls1.FiltrationMeasurementNoiseCovarianceR = settingsLls.RLls1.ToString();
                                    //lls1.TempCompenstationModeIndex = settingsLls.ThermocompTypeLls1;
                                    //lls1.TempCompensationCoef1 = settingsLls.K1Lls1.ToString();
                                    //lls1.TempCompensationCoef2 = settingsLls.K2Lls1.ToString();
                                    //lls1.TypeOfInterpolationIndex = settingsLls.InterpolationTypeLls1;
                                    ///* second */
                                    //lls2.MinLevel = settingsLls.MinLevelLls2.ToString();
                                    //lls2.MaxLevel = settingsLls.MaxLevelLls2.ToString();
                                    //lls2.CntEmpty = settingsLls.CntEmptyLls2.ToString();
                                    //lls2.CntFull = settingsLls.CntFullLls2.ToString();
                                    //lls2.TypeOutMessageIsRelativeLevel = settingsLls.OutTypeLls2 == 0 ? false : true;
                                    //lls2.TypeOfFiltrationIndex = settingsLls.FillUpThresholdLls2;
                                    //lls2.FiltrationAveragingTime = settingsLls.AvarageLenghLls2.ToString();
                                    //lls2.FiltrationLenghtOfMedian = settingsLls.MedianLenghtLls2.ToString();
                                    //lls2.FiltrationProcessNoiseCovarianceQ = settingsLls.QLls2.ToString();
                                    //lls2.FiltrationMeasurementNoiseCovarianceR = settingsLls.RLls2.ToString();
                                    //lls2.TempCompenstationModeIndex = settingsLls.ThermocompTypeLls2;
                                    //lls2.TempCompensationCoef1 = settingsLls.K1Lls2.ToString();
                                    //lls2.TempCompensationCoef2 = settingsLls.K2Lls2.ToString();
                                    //lls2.TypeOfInterpolationIndex = settingsLls.InterpolationTypeLls2;
                                    break;
                                case TrackerTypeData.KeyParameter.SettingsOneWire:
                                    //var settingsOneWire = converter.Deserialize<TrackerStructureSettingsOneWire>((byte[])i.Data);
                                    //var oneWire1 = _viewModel.SettingsModel.OneWireSettingsModelList[0];
                                    //var oneWire2 = _viewModel.SettingsModel.OneWireSettingsModelList[1];
                                    //var oneWire3 = _viewModel.SettingsModel.OneWireSettingsModelList[2];
                                    //var oneWire4 = _viewModel.SettingsModel.OneWireSettingsModelList[3];
                                    ///* onewire1 */
                                    //oneWire1.IsEnable = settingsOneWire.Sensor1IsEnabled;
                                    //oneWire1.SensorName = Encoding.ASCII.GetString(settingsOneWire.Sensor1_Name).Trim('\0');
                                    //oneWire1.LowerAlarmZone = (int)settingsOneWire.Sensor1_AlarmZoneMin;
                                    //oneWire1.UpperAlarmZone = (int)settingsOneWire.Sensor1_AlarmZoneMax;
                                    ///* onewire2 */
                                    //oneWire2.IsEnable = settingsOneWire.Sensor2IsEnabled;
                                    //oneWire2.SensorName = Encoding.ASCII.GetString(settingsOneWire.Sensor2_Name).Trim('\0');
                                    //oneWire2.LowerAlarmZone = (int)settingsOneWire.Sensor2_AlarmZoneMin;
                                    //oneWire2.UpperAlarmZone = (int)settingsOneWire.Sensor2_AlarmZoneMax;
                                    ///* onewire3 */
                                    //oneWire3.IsEnable = settingsOneWire.Sensor3IsEnabled;
                                    //oneWire3.SensorName = Encoding.ASCII.GetString(settingsOneWire.Sensor3_Name).Trim('\0');
                                    //oneWire3.LowerAlarmZone = (int)settingsOneWire.Sensor3_AlarmZoneMin;
                                    //oneWire3.UpperAlarmZone = (int)settingsOneWire.Sensor3_AlarmZoneMax;
                                    ///* onewire5 */
                                    //oneWire4.IsEnable = settingsOneWire.Sensor4IsEnabled;
                                    //oneWire4.SensorName = Encoding.ASCII.GetString(settingsOneWire.Sensor4_Name).Trim('\0');
                                    //oneWire4.LowerAlarmZone = (int)settingsOneWire.Sensor4_AlarmZoneMin;
                                    //oneWire4.UpperAlarmZone = (int)settingsOneWire.Sensor4_AlarmZoneMax;
                                    break;
                                case TrackerTypeData.KeyParameter.SettingsSms:
                                    var settingsSms = converter.Deserialize<TrackerStructureSettingsSms>((byte[])i.Data);
                                    break;
                                case TrackerTypeData.KeyParameter.SettingsGeofence:
                                    //var settingsGeofence = converter.Deserialize<TrackerStructureGeofense>((byte[])i.Data);
                                    break;
                                case TrackerTypeData.KeyParameter.SettingsTrack:
                                    var settingsTrack = converter.Deserialize<TrackerStructureSettingsTrack>((byte[])i.Data);
                                    break;
                                default:
                                    break;
                            }
                        }

                        _viewModel.ConnectViewModel.IsReadyReadWriteSettings = true;
                        Application.Current.Dispatcher.Invoke(delegate
                        {
                            MessageBox.Show("Settings have been read",
                                  "Ok",
                                  MessageBoxButton.OK,
                                  MessageBoxImage.Information);
                        });
                    }
                    if (result.typeMessage == TrackerTypeData.TypeMessage.SettignsWrite)
                    {
                        _viewModel.ConnectViewModel.IsReadyReadWriteSettings = true;
                        Application.Current.Dispatcher.Invoke(delegate
                        {
                            MessageBox.Show("Settings have been recorded",
                                  "Ok",
                                  MessageBoxButton.OK,
                                  MessageBoxImage.Information);
                        });
                    }
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
                        break;
                    case TrackerTypeData.KeyParameter.DbgMessage:
                        Application.Current.Dispatcher.Invoke(delegate
                        {
                            _viewModel.RightPanelModel.StatusInfo.Log.Add(item: new StatusDataViewModel.LogItem { Message = i.Data.ToString(), Type = i.Key });
                        });
                        break;
                    case TrackerTypeData.KeyParameter.imei:
                        Application.Current.Dispatcher.Invoke(delegate
                        {
                            _viewModel.RightPanelModel.CurrentData.ImeiModemValue = i.Data.ToString();
                        });
                        break;
                    case TrackerTypeData.KeyParameter.imsi:
                        break;
                    case TrackerTypeData.KeyParameter.GsmCsq:
                        Application.Current.Dispatcher.Invoke(delegate
                        {
                            _viewModel.RightPanelModel.CurrentData.GmsSignalStrenghtPercentValue = (int)i.Data;
                        });
                        break;
                    case TrackerTypeData.KeyParameter.GnssLat:
                        Application.Current.Dispatcher.Invoke(delegate
                        {
                            _viewModel.RightPanelModel.CurrentData.GnssLatValue = (float)i.Data;
                        });
                        break;
                    case TrackerTypeData.KeyParameter.GnssLon:
                        Application.Current.Dispatcher.Invoke(delegate
                        {
                            _viewModel.RightPanelModel.CurrentData.GnssLonValue = (float)i.Data;
                        });
                        break;
                    case TrackerTypeData.KeyParameter.GnssSat:
                        Application.Current.Dispatcher.Invoke(delegate
                        {
                            _viewModel.RightPanelModel.CurrentData.GnssSatFixValue = (int)i.Data;
                        });
                        break;
                    case TrackerTypeData.KeyParameter.Ain1:
                        Application.Current.Dispatcher.Invoke(delegate
                        {
                            _viewModel.RightPanelModel.CurrentData.Ain1Value = (int)i.Data;
                        });
                        break;
                    case TrackerTypeData.KeyParameter.Ain2:
                        Application.Current.Dispatcher.Invoke(delegate
                        {
                            _viewModel.RightPanelModel.CurrentData.Ain2Value = (int)i.Data;
                        });
                        break;
                    case TrackerTypeData.KeyParameter.Ain3:
                        Application.Current.Dispatcher.Invoke(delegate
                        {
                            _viewModel.RightPanelModel.CurrentData.Ain3Value = (int)i.Data;
                        });
                        break;
                    case TrackerTypeData.KeyParameter.PowerBat:
                        Application.Current.Dispatcher.Invoke(delegate
                        {
                            _viewModel.RightPanelModel.CurrentData.PowerBatteryValue = (int)i.Data;
                        });
                        break;
                    case TrackerTypeData.KeyParameter.PowerExt:
                        Application.Current.Dispatcher.Invoke(delegate
                        {
                            _viewModel.RightPanelModel.CurrentData.PowerExternalValue = (int)i.Data;
                        });
                        break;
                    case TrackerTypeData.KeyParameter.Time:
                        Application.Current.Dispatcher.Invoke(delegate
                        {
                            DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
                            _viewModel.RightPanelModel.CurrentData.Time = dtDateTime.AddSeconds((int)i.Data).ToLocalTime();
                        });
                        break;
                    case TrackerTypeData.KeyParameter.LogRecord:
                        Application.Current.Dispatcher.Invoke(delegate
                        {
                            StructureBinaryConverter structureBinaryConverter = new StructureBinaryConverter();
                            var log = structureBinaryConverter.Deserialize<TrackerStructureLogRecord>((byte[])i.Data);
                            _viewModel.RightPanelModel.CurrentData.LogPositionList.Add(new CurrentDataViewModel.LogPoint(log.Id, log.GnssLongitude, log.GnssLatitude, DateTime.Now));
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
            getSettings();
        }

        private void TrackerConnectPannel_saveConfigEvent(object sender, EventArgs e)
        {
            var parser = new TrackerParserData();
            var converter = new StructureBinaryConverter();
            var data = new List<byte>();
            data.Add((int)TrackerTypeData.TypePacketData.Request);
            data.Add(0); // size packet L byte
            data.Add(0); // size packet H byte
            data.Add((int)TrackerTypeData.TypeMessage.SettignsWrite);
            data.Add(0); // Param count
            var settingsGsm = new TrackerStructureGsm();
            settingsGsm.PinCode = Encoding.Default.GetBytes(_viewModel.SettingsModel.ApnPinCode);
            settingsGsm.Apn = Encoding.Default.GetBytes(_viewModel.SettingsModel.Apn);
            settingsGsm.ApnUser = Encoding.Default.GetBytes(_viewModel.SettingsModel.ApnLogin);
            settingsGsm.ApnPassword = Encoding.Default.GetBytes(_viewModel.SettingsModel.ApnPassword);
            data.AddRange(parser.addParam(new DataItemParam { Key = TrackerTypeData.KeyParameter.SettingsGsm, Type = typeof(byte[]), Data = converter.Serialize(settingsGsm) }));
            data[(int)TrackerTypeData.PacketField.PacketLenByteL] = (byte)(data.Count & 0xFF);
            data[(int)TrackerTypeData.PacketField.PacketLenByteH] = (byte)((data.Count & 0xFF00) >> 8);
            data[(int)TrackerTypeData.PacketField.PacketParamCountIndex] = 1;
            var crc = CrcCalc.Crc16(data.ToArray());
            var crcArray = BitConverter.GetBytes(crc);
            data.AddRange(crcArray);
            _dataPort.WriteData("writeSettings", data.ToArray());
            _viewModel.ConnectViewModel.IsReadyReadWriteSettings = false;
        }

        private void getSettings()
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
            _dataPort.WriteData("readSettings", data.ToArray());
            _viewModel.ConnectViewModel.IsReadyReadWriteSettings = false;
        }

        private List<Tuple<double, double>> _mapList = new List<Tuple<double, double>>();
        private bool DrawMap(double lat, double lon)
        {
            bool result = false;
            LocationCollection locs = new LocationCollection();
            if (_mapList.Count < 500)
            {
                _mapList.Add(new Tuple<double, double>(lat, lon));
                _viewModel.RightPanelModel.CurrentData.Map.Children.Clear();
                foreach (var point in _mapList)
                {
                    locs.Add(new Microsoft.Maps.MapControl.WPF.Location(point.Item1, point.Item2));

                    MapPolyline routeLine = new MapPolyline()
                    {
                        Locations = locs,
                        Stroke = new SolidColorBrush(Colors.Blue),
                        StrokeThickness = 5
                    };
                    _viewModel.RightPanelModel.CurrentData.Map.Children.Add(routeLine);
                    result = true;
                }
            }
            return result;
        }
    }
}
