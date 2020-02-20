using MetroDemo.Core;
using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;
using TrackAndFuel.ViewModel;
using static TrackAndFuel.ViewModel.SettingsViewModel;

namespace TrackAndFuel.Instrumentals.Tracker
{
    class TrackerDataToView
    {
        private StructureBinaryConverter converter = new StructureBinaryConverter();
        private List<Tuple<double, double>> _mapList = new List<Tuple<double, double>>();
        public void InsertData(TrackerParserDataAbstract.ParserResult data, ViewModelBase model)
        {
            if (data != null)
            {
                var viewModel = model as MainViewModel;
                if (data.type == TrackerTypeData.TypePacketData.Answer)
                {
                    if (data.typeMessage == TrackerTypeData.TypeMessage.SettingsRead)
                    {
                        InsertSettingsResult(data, viewModel);
                    }
                }
                else
                {
                    if (data.type == TrackerTypeData.TypePacketData.AsyncData)
                    {
                        InsertAsyncData(data, viewModel);
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

        private void InsertAsyncData(TrackerParserDataAbstract.ParserResult data, MainViewModel viewModel)
        {
            switch (data.typeMessage)
            {
                case TrackerTypeData.TypeMessage.Debug:
                case TrackerTypeData.TypeMessage.Status:
                case TrackerTypeData.TypeMessage.Data:
                case TrackerTypeData.TypeMessage.AsyncData:
                case TrackerTypeData.TypeMessage.Log:
                    foreach (var i in data.data)
                    {

                        switch (i.Key)
                        {
                            case TrackerTypeData.KeyParameter.DbgLevel:
                                break;
                            case TrackerTypeData.KeyParameter.DbgMessage:
                                Application.Current.Dispatcher.Invoke(delegate
                                {
                                    viewModel.RightPanelModel.StatusInfo.Log.Add(item: new StatusDataViewModel.LogItem { Message = i.Data.ToString(), Type = i.Key });
                                });
                                break;
                            case TrackerTypeData.KeyParameter.imei:
                                Application.Current.Dispatcher.Invoke(delegate
                                {
                                    viewModel.RightPanelModel.CurrentData.ImeiModemValue = i.Data.ToString();
                                });
                                break;
                            case TrackerTypeData.KeyParameter.imsi:
                                break;
                            case TrackerTypeData.KeyParameter.GsmCsq:
                                Application.Current.Dispatcher.Invoke(delegate
                                {
                                    viewModel.RightPanelModel.CurrentData.GmsSignalStrenghtPercentValue = (int)i.Data;
                                });
                                break;
                            case TrackerTypeData.KeyParameter.GnssLat:
                                Application.Current.Dispatcher.Invoke(delegate
                                {
                                    viewModel.RightPanelModel.CurrentData.GnssLatValue = (float)i.Data;
                                });
                                break;
                            case TrackerTypeData.KeyParameter.GnssLon:
                                Application.Current.Dispatcher.Invoke(delegate
                                {
                                    viewModel.RightPanelModel.CurrentData.GnssLonValue = (float)i.Data;
                                });
                                break;
                            case TrackerTypeData.KeyParameter.GnssSat:
                                Application.Current.Dispatcher.Invoke(delegate
                                {
                                    viewModel.RightPanelModel.CurrentData.GnssSatFixValue = (int)i.Data;
                                });
                                break;
                            case TrackerTypeData.KeyParameter.Ain1:
                                Application.Current.Dispatcher.Invoke(delegate
                                {
                                    viewModel.RightPanelModel.CurrentData.Ain1Value = (int)i.Data;
                                });
                                break;
                            case TrackerTypeData.KeyParameter.Ain2:
                                Application.Current.Dispatcher.Invoke(delegate
                                {
                                    viewModel.RightPanelModel.CurrentData.Ain2Value = (int)i.Data;
                                });
                                break;
                            case TrackerTypeData.KeyParameter.Ain3:
                                Application.Current.Dispatcher.Invoke(delegate
                                {
                                    viewModel.RightPanelModel.CurrentData.Ain3Value = (int)i.Data;
                                });
                                break;
                            case TrackerTypeData.KeyParameter.PowerBat:
                                Application.Current.Dispatcher.Invoke(delegate
                                {
                                    viewModel.RightPanelModel.CurrentData.PowerBatteryValue = (int)i.Data;
                                });
                                break;
                            case TrackerTypeData.KeyParameter.PowerExt:
                                Application.Current.Dispatcher.Invoke(delegate
                                {
                                    viewModel.RightPanelModel.CurrentData.PowerExternalValue = (int)i.Data;
                                });
                                break;
                            case TrackerTypeData.KeyParameter.Time:
                                Application.Current.Dispatcher.Invoke(delegate
                                {
                                    DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
                                    viewModel.RightPanelModel.CurrentData.Time = dtDateTime.AddSeconds((int)i.Data).ToLocalTime();
                                });
                                break;
                            case TrackerTypeData.KeyParameter.LogRecord:
                                Application.Current.Dispatcher.Invoke(delegate
                                {
                                    StructureBinaryConverter structureBinaryConverter = new StructureBinaryConverter();
                                    var log = structureBinaryConverter.Deserialize<TrackerStructureLogRecord>((byte[])i.Data);
                                    viewModel.RightPanelModel.CurrentData.LogPositionList.Add(new CurrentDataViewModel.LogPoint(log.Id, log.GnssLongitude, log.GnssLatitude, DateTime.Now));
                                    if (!DrawMap(log.GnssLatitude, log.GnssLongitude, viewModel))
                                    {
                                        viewModel.ConnectViewModel.IsLogReading = false;
                                        viewModel.ConnectViewModel.CommandDataBuf.Add(new ConnectPanelViewModel.CommandData("stopTestLog", new byte[0]));
                                        MessageBox.Show("It is not possible to read more than 500 items", "Information", MessageBoxButton.OK);
                                    }
                                });
                                break;
                        }
                    }
                    break;
                case TrackerTypeData.TypeMessage.SettingsRead:
                    break;
                case TrackerTypeData.TypeMessage.SettignsWrite:
                    break;
                case TrackerTypeData.TypeMessage.LogCleanFinish:
                    viewModel.ConnectViewModel.IsWaitsForLogClear = false;
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

        private void InsertSettingsResult(TrackerParserDataAbstract.ParserResult data, MainViewModel viewModel)
        {
            foreach (var i in data.data)
            {
                try
                {
                    switch (i.Key)
                    {
                        case TrackerTypeData.KeyParameter.SettingsGsm:
                            Application.Current.Dispatcher.Invoke(() =>
                            {
                                TrackerStructureGsm settingsGsm = converter.Deserialize<TrackerStructureGsm>((byte[])i.Data);
                                viewModel.SettingsModel.ApnPinCode = Encoding.ASCII.GetString(settingsGsm.PinCode).Trim('\0');
                                viewModel.SettingsModel.Apn = Encoding.ASCII.GetString(settingsGsm.Apn).Trim('\0');
                                viewModel.SettingsModel.ApnLogin = Encoding.ASCII.GetString(settingsGsm.ApnUser).Trim('\0');
                                viewModel.SettingsModel.ApnPassword = Encoding.ASCII.GetString(settingsGsm.ApnPassword).Trim('\0');
                                var operatorApn = viewModel.SettingsModel.Apn.ToUpper();
                                if (operatorApn.Contains("MTS"))
                                {
                                    viewModel.SettingsModel.OperatorListIndex = (int)OperatorType.MTS;
                                }
                                else if (operatorApn.Contains("BELLINE"))
                                {
                                    viewModel.SettingsModel.OperatorListIndex = (int)OperatorType.Beeline;
                                }
                                else if (operatorApn.Contains("MEGAFON"))
                                {
                                    viewModel.SettingsModel.OperatorListIndex = (int)OperatorType.Megafon;
                                }
                                else if (operatorApn.Contains("TELE2"))
                                {
                                    viewModel.SettingsModel.OperatorListIndex = (int)OperatorType.Tele2;
                                }
                                else
                                {
                                    viewModel.SettingsModel.OperatorListIndex = (int)OperatorType.Custom;
                                }
                            });
                            //App
                            break;
                        case TrackerTypeData.KeyParameter.SettingsServers:
                            Application.Current.Dispatcher.Invoke(() =>
                            {
                                var settingsCon = converter.Deserialize<TrackerStructureSettingsConnection>((byte[])i.Data);
                                var connectFirst = viewModel.SettingsModel.ServersConnectionModel;
                                /* first */
                                connectFirst.ProtocolTypeIndex = settingsCon.ProtocolType_0;
                                connectFirst.IpDnsAddress = Encoding.ASCII.GetString(settingsCon.ConnectAddr_0).Trim('\0');
                                connectFirst.Port = settingsCon.ConnectPort_0.ToString();
                                connectFirst.UseCompression = settingsCon.ConnectUseCompression_0;
                                connectFirst.ConnectPeriodOfPingMessage = (int)settingsCon.ConnectPeriodOfPingMessage_0;
                                connectFirst.ConnectDelayBeforeNextConnecting = (int)settingsCon.ConnectDelayBeforeNextConnecting_0;
                                connectFirst.ConnectSendingPeropdDuringParking = (int)settingsCon.ConnectSendingPeropdDuringParking_0;
                                connectFirst.ConnectSendingPeropdInSleepMode = (int)settingsCon.ConnectSendingPeropdInSleepMode_0;
                                connectFirst.AdditionParams = (int)settingsCon.AdditionParams_0;
                            });
                            break;
                        case TrackerTypeData.KeyParameter.SettingsSleep:
                            Application.Current.Dispatcher.Invoke(() =>
                            {
                                var settingsSpeep = converter.Deserialize<TrackerStructureSleep>((byte[])i.Data);
                            });
                            break;
                        case TrackerTypeData.KeyParameter.SettingsGpio:
                            Application.Current.Dispatcher.Invoke(() =>
                            {
                                var settingsGpio = converter.Deserialize<TrackerStructureGPIO>((byte[])i.Data);
                                InputItemSettingsModel gpio1 = viewModel.SettingsModel.InputsSettingsModelList[0];
                                InputItemSettingsModel gpio2 = viewModel.SettingsModel.InputsSettingsModelList[1];
                                InputItemSettingsModel gpio3 = viewModel.SettingsModel.InputsSettingsModelList[2];
                                // gpio1
                                gpio1.PortRoleIndex = settingsGpio.IN1_Mode;
                                gpio1.SignalAnalysysTime = settingsGpio.IN1_timeBase;
                                gpio1.ThresholdUpper = settingsGpio.IN1_LowLevelUpperThreshold;
                                gpio1.ThresholdLower = settingsGpio.IN1_HightLevelLowerThreshold;
                                gpio1.UseFiltrating = settingsGpio.IN1_IsFiltrationEnable;
                                gpio1.LevelOfFiltrationValue = settingsGpio.IN1_AveragingFilterLenght;
                                // gpio2
                                gpio2.PortRoleIndex = settingsGpio.IN2_Mode;
                                gpio2.SignalAnalysysTime = settingsGpio.IN2_timeBase;
                                gpio2.ThresholdUpper = settingsGpio.IN2_LowLevelUpperThreshold;
                                gpio2.ThresholdLower = settingsGpio.IN2_HightLevelLowerThreshold;
                                gpio2.UseFiltrating = settingsGpio.IN2_IsFiltrationEnable;
                                gpio2.LevelOfFiltrationValue = settingsGpio.IN2_AveragingFilterLenght;
                            });
                            break;
                        case TrackerTypeData.KeyParameter.SettingsLlsInternal:
                            Application.Current.Dispatcher.Invoke(() =>
                            {
                                var settingsLls = converter.Deserialize<TrackerStructureSettingsLls>((byte[])i.Data);
                                var lls1 = viewModel.SettingsModel.LlsDataViewModelList[0];
                                var lls2 = viewModel.SettingsModel.LlsDataViewModelList[1];
                                /* first */
                                lls1.MinLevel = settingsLls.MinLevelLls1.ToString();
                                lls1.MaxLevel = settingsLls.MaxLevelLls1.ToString();
                                lls1.CntEmpty = settingsLls.CntEmptyLls1.ToString();
                                lls1.CntFull = settingsLls.CntFullLls1.ToString();
                                lls1.TypeOutMessageIsRelativeLevel = settingsLls.OutTypeLls1 == 0 ? false : true;
                                lls1.TypeOfFiltrationIndex = settingsLls.FillUpThresholdLls1;
                                lls1.FiltrationAveragingTime = settingsLls.AvarageLenghLls1.ToString();
                                lls1.FiltrationLenghtOfMedian = settingsLls.MedianLenghtLls1.ToString();
                                lls1.FiltrationProcessNoiseCovarianceQ = settingsLls.QLls1.ToString();
                                lls1.FiltrationMeasurementNoiseCovarianceR = settingsLls.RLls1.ToString();
                                lls1.TempCompenstationModeIndex = settingsLls.ThermocompTypeLls1;
                                lls1.TempCompensationCoef1 = settingsLls.K1Lls1.ToString();
                                lls1.TempCompensationCoef2 = settingsLls.K2Lls1.ToString();
                                lls1.TypeOfInterpolationIndex = settingsLls.InterpolationTypeLls1;
                                /* second */
                                lls2.MinLevel = settingsLls.MinLevelLls2.ToString();
                                lls2.MaxLevel = settingsLls.MaxLevelLls2.ToString();
                                lls2.CntEmpty = settingsLls.CntEmptyLls2.ToString();
                                lls2.CntFull = settingsLls.CntFullLls2.ToString();
                                lls2.TypeOutMessageIsRelativeLevel = settingsLls.OutTypeLls2 == 0 ? false : true;
                                lls2.TypeOfFiltrationIndex = settingsLls.FillUpThresholdLls2;
                                lls2.FiltrationAveragingTime = settingsLls.AvarageLenghLls2.ToString();
                                lls2.FiltrationLenghtOfMedian = settingsLls.MedianLenghtLls2.ToString();
                                lls2.FiltrationProcessNoiseCovarianceQ = settingsLls.QLls2.ToString();
                                lls2.FiltrationMeasurementNoiseCovarianceR = settingsLls.RLls2.ToString();
                                lls2.TempCompenstationModeIndex = settingsLls.ThermocompTypeLls2;
                                lls2.TempCompensationCoef1 = settingsLls.K1Lls2.ToString();
                                lls2.TempCompensationCoef2 = settingsLls.K2Lls2.ToString();
                                lls2.TypeOfInterpolationIndex = settingsLls.InterpolationTypeLls2;
                            });
                            break;
                        case TrackerTypeData.KeyParameter.SettingsOneWire:
                            Application.Current.Dispatcher.Invoke(() =>
                            {
                                var settingsOneWire = converter.Deserialize<TrackerStructureSettingsOneWire>((byte[])i.Data);
                                /* onewire1 */
                                viewModel.SettingsModel.OneWireSettingsModelList[0].IsEnable = settingsOneWire.Sensor1IsEnabled;
                                viewModel.SettingsModel.OneWireSettingsModelList[0].SensorName = Encoding.ASCII.GetString(settingsOneWire.Sensor1_Name).Trim('\0');
                                viewModel.SettingsModel.OneWireSettingsModelList[0].HexCode = Encoding.ASCII.GetString(settingsOneWire.Sensor1_Code).Trim('\0');
                                viewModel.SettingsModel.OneWireSettingsModelList[0].LowerAlarmZone = (int)settingsOneWire.Sensor1_AlarmZoneMin;
                                viewModel.SettingsModel.OneWireSettingsModelList[0].UpperAlarmZone = (int)settingsOneWire.Sensor1_AlarmZoneMax;
                                /* onewire2 */
                                viewModel.SettingsModel.OneWireSettingsModelList[1].IsEnable = settingsOneWire.Sensor2IsEnabled;
                                viewModel.SettingsModel.OneWireSettingsModelList[1].SensorName = Encoding.ASCII.GetString(settingsOneWire.Sensor2_Name).Trim('\0');
                                viewModel.SettingsModel.OneWireSettingsModelList[1].HexCode = Encoding.ASCII.GetString(settingsOneWire.Sensor2_Code).Trim('\0');
                                viewModel.SettingsModel.OneWireSettingsModelList[1].LowerAlarmZone = (int)settingsOneWire.Sensor2_AlarmZoneMin;
                                viewModel.SettingsModel.OneWireSettingsModelList[1].UpperAlarmZone = (int)settingsOneWire.Sensor2_AlarmZoneMax;
                                /* onewire3 */
                                viewModel.SettingsModel.OneWireSettingsModelList[2].IsEnable = settingsOneWire.Sensor3IsEnabled;
                                viewModel.SettingsModel.OneWireSettingsModelList[2].SensorName = Encoding.ASCII.GetString(settingsOneWire.Sensor3_Name).Trim('\0');
                                viewModel.SettingsModel.OneWireSettingsModelList[2].HexCode = Encoding.ASCII.GetString(settingsOneWire.Sensor3_Code).Trim('\0');
                                viewModel.SettingsModel.OneWireSettingsModelList[2].LowerAlarmZone = (int)settingsOneWire.Sensor3_AlarmZoneMin;
                                viewModel.SettingsModel.OneWireSettingsModelList[2].UpperAlarmZone = (int)settingsOneWire.Sensor3_AlarmZoneMax;
                                /* onewire5 */
                                viewModel.SettingsModel.OneWireSettingsModelList[3].IsEnable = settingsOneWire.Sensor4IsEnabled;
                                viewModel.SettingsModel.OneWireSettingsModelList[3].SensorName = Encoding.ASCII.GetString(settingsOneWire.Sensor4_Name).Trim('\0');
                                viewModel.SettingsModel.OneWireSettingsModelList[3].HexCode = Encoding.ASCII.GetString(settingsOneWire.Sensor4_Code).Trim('\0');
                                viewModel.SettingsModel.OneWireSettingsModelList[3].LowerAlarmZone = (int)settingsOneWire.Sensor4_AlarmZoneMin;
                                viewModel.SettingsModel.OneWireSettingsModelList[3].UpperAlarmZone = (int)settingsOneWire.Sensor4_AlarmZoneMax;
                            });
                            break;
                        case TrackerTypeData.KeyParameter.SettingsSms:
                            Application.Current.Dispatcher.Invoke(() =>
                            {
                                var settingsSms = converter.Deserialize<TrackerStructureSettingsSms>((byte[])i.Data);
                            });
                            break;
                        case TrackerTypeData.KeyParameter.SettingsGeofence:
                            Application.Current.Dispatcher.Invoke(() =>
                            {
                                //var settingsGeofence = converter.Deserialize<TrackerStructureGeofense>((byte[])i.Data);
                            });
                            break;
                        case TrackerTypeData.KeyParameter.SettingsTrack:
                            Application.Current.Dispatcher.Invoke(() =>
                            {
                                var settingsTrack = converter.Deserialize<TrackerStructureSettingsTrack>((byte[])i.Data);
                            });
                            break;
                        default:
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("MainPanel: parsing data to ui exception: " + ex.ToString());
                }
            }
            Application.Current.Dispatcher.Invoke(delegate
            {
                MessageBox.Show("Settings have been read",
                      "Ok",
                      MessageBoxButton.OK,
                      MessageBoxImage.Information);
            });
            if (data.typeMessage == TrackerTypeData.TypeMessage.SettignsWrite)
            {
                Application.Current.Dispatcher.Invoke(delegate
                {
                    MessageBox.Show("Settings have been recorded",
                          "Ok",
                          MessageBoxButton.OK,
                          MessageBoxImage.Information);
                });
            }
        }

        private bool DrawMap(double lat, double lon, MainViewModel viewModel)
        {
            bool result = false;
            LocationCollection locs = new LocationCollection();
            if (_mapList.Count < 500)
            {
                _mapList.Add(new Tuple<double, double>(lat, lon));
                viewModel.RightPanelModel.CurrentData.Map.Children.Clear();
                foreach (var point in _mapList)
                {
                    locs.Add(new Microsoft.Maps.MapControl.WPF.Location(point.Item1, point.Item2));

                    MapPolyline routeLine = new MapPolyline()
                    {
                        Locations = locs,
                        Stroke = new SolidColorBrush(Colors.Blue),
                        StrokeThickness = 5
                    };
                    viewModel.RightPanelModel.CurrentData.Map.Children.Add(routeLine);
                    result = true;
                }
            }
            return result;
        }
    }
}
