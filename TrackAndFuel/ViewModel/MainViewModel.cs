
using System.Windows.Controls;

namespace TrackAndFuel.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private SettingsViewModel _settingsViewModel;
        private RightPannelViewModel _rightPannelViewModel;
        private ConnectPanelViewModel _connectPannelViewModel;
        private Page _content;

        public MainViewModel()
        {
            _rightPannelViewModel = new RightPannelViewModel();
            _connectPannelViewModel = new ConnectPanelViewModel();
            _settingsViewModel = new SettingsViewModel();
        }

        public Page NavigateContent
        {
            get => _content;
            set
            {
                _content = value;
                OnPropertyChanged();
            }
        }

        public RightPannelViewModel RightPannelModel
        {
            get
            {
                return _rightPannelViewModel;
            }
        }

        public ConnectPanelViewModel ConnectViewModel
        {
            get
            {
                return _connectPannelViewModel;
            }
            set
            {
                _connectPannelViewModel = value;
                OnPropertyChanged();
            }
        }

        public SettingsViewModel SettingsModel
        {
            get => _settingsViewModel;
            set
            {
                _settingsViewModel = value;
                OnPropertyChanged();
            }
        }
    }
}
