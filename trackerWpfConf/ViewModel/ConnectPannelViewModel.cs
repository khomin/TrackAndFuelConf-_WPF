using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace trackerWpfConf.ViewModel
{
    class ConnectPannelViewModel : ViewModelBase
    {
        private ObservableCollection<string> _portList;

        private bool _isConnected;
        private Brush _colorStatus;

        public ConnectPannelViewModel()
        {
            PortsList = new ObservableCollection<string> { "COM1", "COM2" };
            IsConnected = false;
            _colorStatus = Brushes.Red;
        }

        public ObservableCollection<string> PortsList {
            get {
                return _portList;
            }
            set {
                _portList = value;
                OnPropertyChanged();
            }
        }

        public bool IsConnected {
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
                }
                else
                {
                    ColorStatus = Brushes.Red;
                }

                OnPropertyChanged();
            }
        }

        public void ResearchPorts()
        {
            _portList = new ObservableCollection<string> { "COM1", "COM2" };
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

        private bool _showLoadSpinner = false;

        public bool ShowLoadSpinner
        {
            get
            {
                return _showLoadSpinner;
            }

            set
            {
                _showLoadSpinner = value;
                OnPropertyChanged();
            }
        }
    }
}
