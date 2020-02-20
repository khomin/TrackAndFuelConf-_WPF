using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using TrackAndFuel.Instrumentals;
using TrackAndFuel.Instrumentals.Tracker;
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
        private TrackerCommandController _commandController = null;
        private TrackerDataToView _dataToView = null;
        public TrackerMainPanel(MainViewModel viewModel, string portName)
        {
            InitializeComponent();
            _viewModel = viewModel;
            DataContext = viewModel;

            _dataToView = new TrackerDataToView();
        }

        private void connectToPort(string portName)
        {
            _commandController = new TrackerCommandController(_dataToView, (error) =>
            {
                Application.Current.Dispatcher.Invoke(delegate
                {
                    MessageBoxResult result = MessageBox.Show("Connection lost",
                                          "Warning",
                                          MessageBoxButton.OK,
                                          MessageBoxImage.Warning);
                    _viewModel.NavigateContent.NavigationService.GoBack();
                });
            }
            , _viewModel, portName);
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
            /* gsm */
            var settingsGsm = new TrackerStructureGsm();
            settingsGsm.PinCode = Encoding.Default.GetBytes(_viewModel.SettingsModel.ApnPinCode);
            settingsGsm.Apn = Encoding.Default.GetBytes(_viewModel.SettingsModel.Apn);
            settingsGsm.ApnUser = Encoding.Default.GetBytes(_viewModel.SettingsModel.ApnLogin);
            settingsGsm.ApnPassword = Encoding.Default.GetBytes(_viewModel.SettingsModel.ApnPassword);
            data.AddRange(parser.addParam(new DataItemParam { Key = TrackerTypeData.KeyParameter.SettingsGsm, Type = typeof(byte[]), Data = converter.Serialize(settingsGsm) }));
            /* oneWire*/
            var settingsOneWire = new TrackerStructureSettingsOneWire();
            settingsOneWire.Sensor1IsEnabled = _viewModel.SettingsModel.OneWireSettingsModelList[0].IsEnable;
            settingsOneWire.Sensor1_Code = Encoding.Default.GetBytes(_viewModel.SettingsModel.OneWireSettingsModelList[0].HexCode);
            settingsOneWire.Sensor1_Name = Encoding.Default.GetBytes(_viewModel.SettingsModel.OneWireSettingsModelList[0].SensorName);
            settingsOneWire.Sensor1_AlarmZoneMax = _viewModel.SettingsModel.OneWireSettingsModelList[0].UpperAlarmZone;
            settingsOneWire.Sensor1_AlarmZoneMin = _viewModel.SettingsModel.OneWireSettingsModelList[0].LowerAlarmZone;
            settingsOneWire.Sensor2IsEnabled = _viewModel.SettingsModel.OneWireSettingsModelList[1].IsEnable;
            settingsOneWire.Sensor2_Code = Encoding.Default.GetBytes(_viewModel.SettingsModel.OneWireSettingsModelList[1].HexCode);
            settingsOneWire.Sensor2_Name = Encoding.Default.GetBytes(_viewModel.SettingsModel.OneWireSettingsModelList[1].SensorName);
            settingsOneWire.Sensor2_AlarmZoneMax = _viewModel.SettingsModel.OneWireSettingsModelList[1].UpperAlarmZone;
            settingsOneWire.Sensor2_AlarmZoneMin = _viewModel.SettingsModel.OneWireSettingsModelList[1].LowerAlarmZone;
            settingsOneWire.Sensor3IsEnabled = _viewModel.SettingsModel.OneWireSettingsModelList[2].IsEnable;
            settingsOneWire.Sensor3_Code = Encoding.Default.GetBytes(_viewModel.SettingsModel.OneWireSettingsModelList[2].HexCode);
            settingsOneWire.Sensor3_Name = Encoding.Default.GetBytes(_viewModel.SettingsModel.OneWireSettingsModelList[2].SensorName);
            settingsOneWire.Sensor3_AlarmZoneMax = _viewModel.SettingsModel.OneWireSettingsModelList[2].UpperAlarmZone;
            settingsOneWire.Sensor3_AlarmZoneMin = _viewModel.SettingsModel.OneWireSettingsModelList[2].LowerAlarmZone;
            settingsOneWire.Sensor4IsEnabled = _viewModel.SettingsModel.OneWireSettingsModelList[3].IsEnable;
            settingsOneWire.Sensor4_Code = Encoding.Default.GetBytes(_viewModel.SettingsModel.OneWireSettingsModelList[3].HexCode);
            settingsOneWire.Sensor4_Name = Encoding.Default.GetBytes(_viewModel.SettingsModel.OneWireSettingsModelList[3].SensorName);
            settingsOneWire.Sensor4_AlarmZoneMax = _viewModel.SettingsModel.OneWireSettingsModelList[3].UpperAlarmZone;
            settingsOneWire.Sensor4_AlarmZoneMin = _viewModel.SettingsModel.OneWireSettingsModelList[3].LowerAlarmZone;
            /* trackconf */
            var settingsTrackConf = new TrackerStructureSettingsTrack();
            settingsTrackConf.MaxDistance = (UInt16)_viewModel.SettingsModel.MaxDistanceBetweenTwoPoints;
            settingsTrackConf.MaxHeading = (UInt16)_viewModel.SettingsModel.MaxHeading;
            settingsTrackConf.AccelThresholdSleep = (byte)_viewModel.SettingsModel.AccelerationThresholdDetermMotion;
            settingsTrackConf.StopToMoveSleep = (UInt16)_viewModel.SettingsModel.MinSpeedForDetectionMotion;
            settingsTrackConf.MoveToStopSleep = (UInt16)_viewModel.SettingsModel.MaxSpeedForDetectionParking;
            settingsTrackConf.MaxStendingTime = (UInt16)_viewModel.SettingsModel.MaxStendingTime;
            settingsTrackConf.MaxSpeep = (UInt16)_viewModel.SettingsModel.MaxDistanceBetweenTwoPoints;
            settingsTrackConf.MinSpeep = (UInt16)_viewModel.SettingsModel.MaxDistanceBetweenTwoPoints;
            settingsTrackConf.PosAccel = (UInt16)_viewModel.SettingsModel.MaxDistanceBetweenTwoPoints;
            settingsTrackConf.NegAccel = (UInt16)_viewModel.SettingsModel.MaxDistanceBetweenTwoPoints;
            settingsTrackConf.IgnType = 0;// (UInt16)_viewModel.SettingsModel.MaxDistanceBetweenTwoPoints;
            settingsTrackConf.IgnThreshold = (UInt16)_viewModel.SettingsModel.MaxDistanceBetweenTwoPoints;

            /* finalize packet, len crc and another */
            data[(int)TrackerTypeData.PacketField.PacketLenByteL] = (byte)(data.Count & 0xFF);
            data[(int)TrackerTypeData.PacketField.PacketLenByteH] = (byte)((data.Count & 0xFF00) >> 8);
            data[(int)TrackerTypeData.PacketField.PacketParamCountIndex] = 2;
            var crc = CrcCalc.Crc16(data.ToArray());
            var crcArray = BitConverter.GetBytes(crc);
            data.AddRange(crcArray);

            _viewModel.ConnectViewModel.CommandDataBuf.Add(new ConnectPanelViewModel.CommandData("writeSettings", data.ToArray()));
        }
    }
}
