
using MetroDemo.Core;
using System.Windows.Controls;

namespace TrackAndFuel.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private SettingsViewModel _settingsViewModel;
        private RightPannelViewModel _rightPannelViewModel;
        private ConnectPanelViewModel _connectPanelViewModel;
        private Page _content;

        public MainViewModel()
        {
            _rightPannelViewModel = new RightPannelViewModel();
            _connectPanelViewModel = new ConnectPanelViewModel();
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

        public RightPannelViewModel RightPanelModel
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
                return _connectPanelViewModel;
            }
            set
            {
                _connectPanelViewModel = value;
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
