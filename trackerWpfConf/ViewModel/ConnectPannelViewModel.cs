using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace trackerWpfConf.ViewModel
{
    class ConnectPannelViewModel : ViewModelBase
    {
        private ObservableCollection<string> _portList;

        private bool _isConnected;
        private Brush _colorStatus;
        private Visibility _loadingViewIsShow;
        private bool _mainFormIsAvailableForInteracting;
        private string _statusConnect = "Disconnected";

        public ConnectPannelViewModel()
        {
            _portList = new ObservableCollection<string>(SerialPort.GetPortNames().ToList());
            _isConnected = false;
            _colorStatus = Brushes.Red;
            LoadingViewIsShow = Visibility.Hidden;
            _mainFormIsAvailableForInteracting = false;
        }

        public ObservableCollection<string> PortsList
        {
            get
            {
                return _portList;
            }
            set
            {
                _portList = value;
                OnPropertyChanged();
            }
        }

        public bool IsConnected
        {
            get
            {
                return _isConnected;
            }
            set
            {
                _isConnected = value;
                if (value)
                {
                    ColorStatus = Brushes.Green;
                    MainFormIsAvailableForInteracting = true;
                }
                else
                {
                    ColorStatus = Brushes.Red;
                    MainFormIsAvailableForInteracting = false;
                }

                OnPropertyChanged();
            }
        }

        public void ResearchPorts()
        {
            _portList = new ObservableCollection<string>(SerialPort.GetPortNames().ToList());
        }

        public Brush ColorStatus
        {
            get
            {
                return _colorStatus;
            }
            set
            {
                _colorStatus = value;
                OnPropertyChanged();
            }
        }

        public bool MainFormIsAvailableForInteracting
        {
            get => _mainFormIsAvailableForInteracting; 
            set
            {
                _mainFormIsAvailableForInteracting = value;
                OnPropertyChanged();
            }
        }

        public Visibility LoadingViewIsShow
        {
            get => _loadingViewIsShow;
            set
            {
                _loadingViewIsShow = value;
                OnPropertyChanged();
            }
        }

        public string StatusConnect
        {
            get => _statusConnect;
            set 
            {
                _statusConnect = value;
                OnPropertyChanged();
            }
        }
    }
}
