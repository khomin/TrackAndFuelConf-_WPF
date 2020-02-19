using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TrackAndFuel.Instrumentals;
using TrackAndFuel.Instrumentals.Tracker;
using TrackAndFuel.ViewModel;

namespace TrackAndFuel.Tracker
{
    /// <summary>
    /// Interaction logic for SettingsCommunication.xaml
    /// </summary>
    public partial class SettingsCommunication : UserControl
    {
        private MainViewModel viewModel;

        public SettingsCommunication()
        {
            InitializeComponent();

            this.DataContextChanged += (object sender, DependencyPropertyChangedEventArgs e) =>
            {
                viewModel = this.DataContext as MainViewModel;
            };
        }

        private void ChangePasswordClick(object sender, RoutedEventArgs e)
        {
            var dialog = new ChangePasswordDialog(viewModel.SettingsModel.SecurityPassword);
            var ownerContent = (FrameworkElement)Content;
            dialog.Top = ownerContent.ActualHeight / 2;
            dialog.Left = ownerContent.ActualWidth / 2;
            var result = dialog.ShowDialog();
            if (result == true)
            {
                var newPassword = dialog.GetNewPassword();
                var parser = new TrackerParserData();
                var converter = new StructureBinaryConverter();
                var data = new List<byte>();
                data.Add((int)TrackerTypeData.TypePacketData.Request);
                data.Add(0); // size packet L byte
                data.Add(0); // size packet H byte
                data.Add((int)TrackerTypeData.TypeMessage.SettignsWrite);
                data.Add(0); // Param count
                /* security structure */
                var settingsSecurity = new TrackerSecuritySettings();
                settingsSecurity.Password = Encoding.Default.GetBytes(newPassword);
                data.AddRange(parser.addParam(new DataItemParam { Key = TrackerTypeData.KeyParameter.Security, Type = typeof(byte[]), Data = converter.Serialize(settingsSecurity) }));

                /* finalize packet, len crc and another */
                data[(int)TrackerTypeData.PacketField.PacketLenByteL] = (byte)(data.Count & 0xFF);
                data[(int)TrackerTypeData.PacketField.PacketLenByteH] = (byte)((data.Count & 0xFF00) >> 8);
                data[(int)TrackerTypeData.PacketField.PacketParamCountIndex] = 1;
                var crc = CrcCalc.Crc16(data.ToArray());
                var crcArray = BitConverter.GetBytes(crc);
                data.AddRange(crcArray);
            }
        }
    }
}
