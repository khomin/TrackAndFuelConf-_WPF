using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using trackerWpfConf.Instrumentals;
using trackerWpfConf.ViewModel;

namespace trackerWpfConf
{
    /// <summary>
    /// Interaction logic for ConnectPannel.xaml
    /// </summary>
    public partial class ConnectPannel : UserControl
    {
        private MainViewModel viewModel;

        private TrackerSerialPort _serialPortHandler;

        public ConnectPannel()
        {
            InitializeComponent();

            this.DataContextChanged += (object sender, DependencyPropertyChangedEventArgs e) => {
                viewModel = this.DataContext as MainViewModel;
            };
        }

        private void refreshButton_Click(object sender, RoutedEventArgs e)
        {
            viewModel.ConnectViewModel.IsConnected = true;
            viewModel.ConnectViewModel.ResearchPorts();
            PortComBox.SelectedIndex = 0;
        }

        private void connectReconnect(object sender, RoutedEventArgs e)
        {
            _serialPortHandler = new TrackerSerialPort(
                PortComBox.Text,
                115200,
            Parity.None,
            8,
            StopBits.One,
            (data) =>
                {
                    DataReceiveHandle(data);
                },
            () =>
            {
                this.Dispatcher.Invoke(() =>
                {
                    viewModel.ConnectViewModel.IsConnected = false;
                    viewModel.ConnectViewModel.LoadingViewIsShow = Visibility.Hidden;
                    viewModel.ConnectViewModel.StatusConnect = "Disconnected";
                    MessageBoxResult result = MessageBox.Show("Connection lost",
                                          "Warning",
                                          MessageBoxButton.OK,
                                          MessageBoxImage.Warning);
                });
            }
            );
            _serialPortHandler.Open();
            viewModel.ConnectViewModel.LoadingViewIsShow = Visibility.Visible;
            viewModel.ConnectViewModel.StatusConnect = "Connecting";

            /* TEST ONLY !!! */
            System.Timers.Timer testTimer = new System.Timers.Timer(3000);
            testTimer.AutoReset = true;
            testTimer.Enabled = true;
            testTimer.Elapsed += (sourse, evendt) =>
            {
                DataReceiveHandle(new List<int>(10));
            };
        }

        private void DataReceiveHandle(List<int> data)
        {
            viewModel.ConnectViewModel.IsConnected = true;
            viewModel.ConnectViewModel.LoadingViewIsShow = Visibility.Hidden;
            viewModel.ConnectViewModel.StatusConnect = "Connected";

            byte[] test = { 0x24, 0x00, 0x02, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0x02, 0x16, 0x00, 0x44, 0x65, 0x66, 0x61, 0x75, 0x6C, 0x74, 0x20, 0x74, 0x61, 0x73, 0x6B, 0x20, 0x77, 0x6F, 0x72, 0x6B, 0x69, 0x6E, 0x67, 0x0D, 0x0A, 0xB3 };
            data.Clear();
            for (int i = 0; i < test.Length; i++)
            {
                data.Add(test[i]);
            }

            TrackerParserData parserData = new TrackerParserData();
            var result = parserData.Parse(test);
            Trace.WriteLine(data);

            //viewModel.RightPannelViewModel.CurrentData.Ain1Value = 
        }
    }
}
