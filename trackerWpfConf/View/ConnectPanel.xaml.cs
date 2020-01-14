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
                    viewModel.ConnectViewModel.IsConnected = true;
                    viewModel.ConnectViewModel.LoadingViewIsShow = Visibility.Hidden;
                    viewModel.ConnectViewModel.StatusConnect = "Connected";

                    Trace.WriteLine(data);
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
        }
    }
}
