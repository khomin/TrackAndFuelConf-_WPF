using MetroDemo.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using TrackAndFuel.ViewModel;
using static TrackAndFuel.ViewModel.SettingsViewModel;

namespace TrackAndFuel.Instrumentals.Tracker
{
    class TrackerDataToView
    {
        private StructureBinaryConverter converter = new StructureBinaryConverter();
        public void InsertData(TrackerParserDataAbstract.ParserResult data, ViewModelBase viewModel)
        {
            if (data != null)
            {
                var view = viewModel as MainViewModel;
                if (data.type == TrackerTypeData.TypePacketData.Answer)
                {
                    if (data.typeMessage == TrackerTypeData.TypeMessage.SettingsRead)
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
                                            view.SettingsModel.ApnPinCode = Encoding.ASCII.GetString(settingsGsm.PinCode).Trim('\0');
                                            view.SettingsModel.Apn = Encoding.ASCII.GetString(settingsGsm.Apn).Trim('\0');
                                            view.SettingsModel.ApnLogin = Encoding.ASCII.GetString(settingsGsm.ApnUser).Trim('\0');
                                            view.SettingsModel.ApnPassword = Encoding.ASCII.GetString(settingsGsm.ApnPassword).Trim('\0');
                                            var operatorApn = view.SettingsModel.Apn.ToUpper();
                                            if (operatorApn.Contains("MTS"))
                                            {
                                                view.SettingsModel.OperatorListIndex = (int)OperatorType.MTS;
                                            }
                                            else if (operatorApn.Contains("BELLINE"))
                                            {
                                                view.SettingsModel.OperatorListIndex = (int)OperatorType.Beeline;
                                            }
                                            else if (operatorApn.Contains("MEGAFON"))
                                            {
                                                view.SettingsModel.OperatorListIndex = (int)OperatorType.Megafon;
                                            }
                                            else if (operatorApn.Contains("TELE2"))
                                            {
                                                view.SettingsModel.OperatorListIndex = (int)OperatorType.Tele2;
                                            }
                                            else
                                            {
                                                view.SettingsModel.OperatorListIndex = (int)OperatorType.Custom;
                                            }
                                        });
                                        //App
                                        break;
                                    case TrackerTypeData.KeyParameter.SettingsServers:
                                        Application.Current.Dispatcher.Invoke(() =>
                                        {
                                            var settingsCon = converter.Deserialize<TrackerStructureSettingsConnection>((byte[])i.Data);
                                            var connectFirst = view.SettingsModel.ServersConnectionModel;
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
                                            InputItemSettingsModel gpio1 = view.SettingsModel.InputsSettingsModelList[0];
                                            InputItemSettingsModel gpio2 = view.SettingsModel.InputsSettingsModelList[1];
                                            InputItemSettingsModel gpio3 = view.SettingsModel.InputsSettingsModelList[2];
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
                                            var lls1 = view.SettingsModel.LlsDataViewModelList[0];
                                            var lls2 = view.SettingsModel.LlsDataViewModelList[1];
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
                                            view.SettingsModel.OneWireSettingsModelList[0].IsEnable = settingsOneWire.Sensor1IsEnabled;
                                            view.SettingsModel.OneWireSettingsModelList[0].SensorName = Encoding.ASCII.GetString(settingsOneWire.Sensor1_Name).Trim('\0');
                                            view.SettingsModel.OneWireSettingsModelList[0].HexCode = Encoding.ASCII.GetString(settingsOneWire.Sensor1_Code).Trim('\0');
                                            view.SettingsModel.OneWireSettingsModelList[0].LowerAlarmZone = (int)settingsOneWire.Sensor1_AlarmZoneMin;
                                            view.SettingsModel.OneWireSettingsModelList[0].UpperAlarmZone = (int)settingsOneWire.Sensor1_AlarmZoneMax;
                                            /* onewire2 */
                                            view.SettingsModel.OneWireSettingsModelList[1].IsEnable = settingsOneWire.Sensor2IsEnabled;
                                            view.SettingsModel.OneWireSettingsModelList[1].SensorName = Encoding.ASCII.GetString(settingsOneWire.Sensor2_Name).Trim('\0');
                                            view.SettingsModel.OneWireSettingsModelList[1].HexCode = Encoding.ASCII.GetString(settingsOneWire.Sensor2_Code).Trim('\0');
                                            view.SettingsModel.OneWireSettingsModelList[1].LowerAlarmZone = (int)settingsOneWire.Sensor2_AlarmZoneMin;
                                            view.SettingsModel.OneWireSettingsModelList[1].UpperAlarmZone = (int)settingsOneWire.Sensor2_AlarmZoneMax;
                                            /* onewire3 */
                                            view.SettingsModel.OneWireSettingsModelList[2].IsEnable = settingsOneWire.Sensor3IsEnabled;
                                            view.SettingsModel.OneWireSettingsModelList[2].SensorName = Encoding.ASCII.GetString(settingsOneWire.Sensor3_Name).Trim('\0');
                                            view.SettingsModel.OneWireSettingsModelList[2].HexCode = Encoding.ASCII.GetString(settingsOneWire.Sensor3_Code).Trim('\0');
                                            view.SettingsModel.OneWireSettingsModelList[2].LowerAlarmZone = (int)settingsOneWire.Sensor3_AlarmZoneMin;
                                            view.SettingsModel.OneWireSettingsModelList[2].UpperAlarmZone = (int)settingsOneWire.Sensor3_AlarmZoneMax;
                                            /* onewire5 */
                                            view.SettingsModel.OneWireSettingsModelList[3].IsEnable = settingsOneWire.Sensor4IsEnabled;
                                            view.SettingsModel.OneWireSettingsModelList[3].SensorName = Encoding.ASCII.GetString(settingsOneWire.Sensor4_Name).Trim('\0');
                                            view.SettingsModel.OneWireSettingsModelList[3].HexCode = Encoding.ASCII.GetString(settingsOneWire.Sensor4_Code).Trim('\0');
                                            view.SettingsModel.OneWireSettingsModelList[3].LowerAlarmZone = (int)settingsOneWire.Sensor4_AlarmZoneMin;
                                            view.SettingsModel.OneWireSettingsModelList[3].UpperAlarmZone = (int)settingsOneWire.Sensor4_AlarmZoneMax;
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

                        if (_settingsIsFirstRead)
                        {
                            Application.Current.Dispatcher.Invoke(delegate
                            {
                                MessageBox.Show("Settings have been read",
                                      "Ok",
                                      MessageBoxButton.OK,
                                      MessageBoxImage.Information);
                            });
                            // reset flag, after first read
                            _settingsIsFirstRead = false;
                        }
                    }
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
                else
                {
                    if (data.type == TrackerTypeData.TypePacketData.AsyncData)
                    {
                        switch (data.typeMessage)
                        {
                            case TrackerTypeData.TypeMessage.Debug:
                            case TrackerTypeData.TypeMessage.Status:
                            case TrackerTypeData.TypeMessage.Data:
                            case TrackerTypeData.TypeMessage.AsyncData:
                            case TrackerTypeData.TypeMessage.Log:
                                ParseDataToViewModel(data.data);
                                break;
                            case TrackerTypeData.TypeMessage.SettingsRead:
                                break;
                            case TrackerTypeData.TypeMessage.SettignsWrite:
                                break;
                            case TrackerTypeData.TypeMessage.LogCleanFinish:
                                view.ConnectViewModel.IsWaitsForLogClear = false;
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
    }
}
