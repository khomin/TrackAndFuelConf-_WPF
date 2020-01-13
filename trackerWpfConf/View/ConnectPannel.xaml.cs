using System;
using System.Collections.Generic;
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
        private readonly ConnectPannelViewModel _connectPannelViewModel;

        private TrackerSerialPortt _serialPortHandler;

        public ConnectPannel()
        {
            InitializeComponent();

            _connectPannelViewModel = new ConnectPannelViewModel();
            DataContext = _connectPannelViewModel;
        }

        private void refreshButton_Click(object sender, RoutedEventArgs e)
        {
            _connectPannelViewModel.ResearchPorts();
            PortComBox.SelectedIndex = 0;
        }

        private void connectReconnect(object sender, RoutedEventArgs e)
        {
            _serialPortHandler = new TrackerSerialPortt(
                PortComBox.Text,
                115200,
            Parity.None,
            8,
            StopBits.One,
            (data) =>
                {
                    _connectPannelViewModel.IsConnected = true;
                    _connectPannelViewModel.ShowLoadSpinner = false;
                    //ShowSpinnerLoading = true;
                },
            () =>
            {
                this.Dispatcher.Invoke(() =>
                {
                    _connectPannelViewModel.IsConnected = false;
                    _connectPannelViewModel.ShowLoadSpinner = false;
                    MessageBoxResult result = MessageBox.Show("Connection lost",
                                          "Warning",
                                          MessageBoxButton.OK,
                                          MessageBoxImage.Warning);
                });
            }
            );
            _serialPortHandler.open();
            _connectPannelViewModel.ShowLoadSpinner = true;
        }


        public bool ShowLoadSpinner
        {
            get { return (bool)GetValue(ShowLoadSpinnerProperty); }
            set { SetValue(ShowLoadSpinnerProperty, value); }
        }

        public static readonly DependencyProperty ShowLoadSpinnerProperty =
            DependencyProperty.Register("ShowLoadSpinner", typeof(bool), typeof(ConnectPannel), new UIPropertyMetadata(UserNameChanged));

        static void UserNameChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var ele = obj as ConnectPannel;
            ele.ShowLoadSpinner = true;//e.NewValue as bool;
            ////do things with the object and it's properties
            //var newVal = (string)e.NewValue;
        }
    }
}
