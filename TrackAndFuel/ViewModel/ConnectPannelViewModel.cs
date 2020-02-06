using MetroDemo.Core;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO.Ports;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace TrackAndFuel.ViewModel
{
    public class ConnectPanelViewModel : ViewModelBase
    {
        private ObservableCollection<string> _portList;

        private bool _isConnected;
        private Brush _colorStatus;
        private Visibility _loadingViewIsShow;
        private bool _mainFormIsAvailableForInteracting;
        private string _statusConnect = "Disconnected";
        private bool _isReadyReadWriteSettings = false;
        private bool _isLogReading = false;
        private readonly List<CommandData> _commandDataBuf;
        private bool _isWaitsForLogClear = false;
        public class CommandData
        {
            public string key;
            public byte[] data;
            public CommandData(string key, byte[] data) 
            {
                this.key = key;
                this.data = data;
            }
        }

        public ConnectPanelViewModel()
        {
            _portList = new ObservableCollection<string>(SerialPort.GetPortNames().ToList());
            _portList.Insert(0, "Demo");
            _isConnected = false;
            _colorStatus = Brushes.Red;
            LoadingViewIsShow = Visibility.Hidden;
            _mainFormIsAvailableForInteracting = false;
            _commandDataBuf = new List<CommandData>();
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
                if (_isConnected == value)
                {
                    return;
                }
                _isConnected = value;
                if (value)
                {
                    ColorStatus = Brushes.Green;
                    MainFormIsAvailableForInteracting = true;
                    IsReadyReadWriteSettings = true;
                }
                else
                {
                    ColorStatus = Brushes.Red;
                    MainFormIsAvailableForInteracting = false;
                    IsReadyReadWriteSettings = false;
                }

                OnPropertyChanged();
            }
        }

        public void ResearchPorts()
        {
            _portList = new ObservableCollection<string>(SerialPort.GetPortNames().ToList());
            _portList.Insert(0, "Demo");
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

        public bool IsReadyReadWriteSettings
        {
            get => _isReadyReadWriteSettings;
            set
            {
                _isReadyReadWriteSettings = value;
                OnPropertyChanged();
            }
        }

        public bool IsLogReading
        {
            get => _isLogReading;
            set
            {
                if (value == true)
                {
                    IsReadyReadWriteSettings = false;
                }
                else
                {
                    IsReadyReadWriteSettings = true;
                }

                _isLogReading = value;
                OnPropertyChanged();
            }
        }

        public List<CommandData> CommandDataBuf => _commandDataBuf;

        public bool IsWaitsForLogClear
        {
            get => _isWaitsForLogClear;
            set
            {
                _isWaitsForLogClear = value;
                OnPropertyChanged();
            }
        }
    }
}
