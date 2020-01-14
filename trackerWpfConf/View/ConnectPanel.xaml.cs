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
        //private readonly ConnectPannelViewModel viewModel;

        private MainViewModel viewModel;

        //private TrackerSerialPort _serialPortHandler;

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
            //    //_connectPannelViewModel.ResearchPorts();
            //    //PortComBox.SelectedIndex = 0;
        }

        private void connectReconnect(object sender, RoutedEventArgs e)
        {
            //    //_serialPortHandler = new TrackerSerialPort(
            //    //    PortComBox.Text,
            //    //    115200,
            //    //Parity.None,
            //    //8,
            //    //StopBits.One,
            //    //(data) =>
            //    //    {
            //    //        _connectPannelViewModel.IsConnected = true;
            //    //        //ShowLoadSpinner = Visibility.Hidden;
            //    //        Trace.WriteLine(data);
            //    //    },
            //    //() =>
            //    //{
            //    //    this.Dispatcher.Invoke(() =>
            //    //    {
            //    //        _connectPannelViewModel.IsConnected = false;
            //    //        ShowLoadSpinner = Visibility.Hidden;
            //    //        MessageBoxResult result = MessageBox.Show("Connection lost",
            //    //                              "Warning",
            //    //                              MessageBoxButton.OK,
            //    //                              MessageBoxImage.Warning);
            //    //    });
            //    //}
            //    //);
            //    //_serialPortHandler.Open();
            //    //ShowLoadSpinner = Visibility.Visible;
        }


        //public Visibility ShowLoadSpinner
        //{
        //    get { return (Visibility)GetValue(ShowLoadSpinnerProperty); }
        //    set { SetValue(ShowLoadSpinnerProperty, value); }
        //}

        //public static readonly DependencyProperty ShowLoadSpinnerProperty =
        //    DependencyProperty.Register("ShowLoadSpinner", typeof(Visibility), typeof(ConnectPannel), new UIPropertyMetadata(Visibility.Hidden, OnMyPropertyChanged));

        //public static void OnMyPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        //{
        //    // code to be executed when property changed
        //}

        //public MainViewModel ViewModel
        //{
        //    get { return (MainViewModel)GetValue(ViewModelProperty); }
        //    set { SetValue(ViewModelProperty, value); }
        //}
        //public static DependencyProperty ViewModelProperty = DependencyProperty.Register("ViewModel", typeof(string), typeof(ConnectPannel), new PropertyMetadata(null));

        //public string Header
        //{
        //    get { return (string)GetValue(HeaderProperty); }
        //    set { SetValue(HeaderProperty, value); }
        //}
        //public static DependencyProperty HeaderProperty =
        //    DependencyProperty.Register("Header", typeof(string), typeof(ConnectPannel), new PropertyMetadata(null));

        //public static readonly DependencyProperty MyRealDataContextProperty =
        //DependencyProperty.Register(
        //    "MyRealDataContext",
        //    typeof(MainViewModel),
        //    typeof(ConnectPannel),
        //    new UIPropertyMetadata());
        //public MainViewModel MyRealDataContext
        //{
        //    get { return (MainViewModel)GetValue(MyRealDataContextProperty); }
        //    set { SetValue(MyRealDataContextProperty, value); }
        //}

        //public static readonly DependencyProperty MyRealDataContextProperty = DependencyProperty.Register("ViewModel",
        //    typeof(MainViewModel),
        //    typeof(MainViewModel),
        //    new PropertyMetadata(default(ConnectPannelViewModel))
        //);

        //public MainViewModel MyRealDataContext
        //{
        //    get { return (MainViewModel)GetValue(MyRealDataContextProperty); }
        //    set { SetValue(MyRealDataContextProperty, value); }
        //}

        //public static readonly DependencyProperty MyDataContextProperty = 
        //    DependencyProperty.Register(null, "MyDataContext", typeof(object), typeof(ConnectPannel), new PropertyMetadata(MyDataContextChangedCallback));

        //private PropertyMetadata MyDataContextChangedCallback;

        //private void MyDataContextChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e) 
        //{
        //}

        //// create binding in constructor or initialization.
        //Binding binding = new Binding("DataContext");

        //BindingOperations.SetBinding(this, MyDataContextProperty, binding);

        //BindingOperations.SetBinding(this, MyDataContextProperty, binding);
    }
}
