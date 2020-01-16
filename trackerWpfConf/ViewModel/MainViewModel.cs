
using System.Windows.Controls;

namespace trackerWpfConf.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private RightPannelViewModel _rightPannelViewModel;
        
        private ConnectPannelViewModel _connectPannelViewModel;

        private Page _content;

        private int _switchView;

        public int SwitchView
        {
            get => _switchView;
            set 
            {
                _switchView = value;
                OnPropertyChanged();
            }
        }
        public MainViewModel() 
        {
            _rightPannelViewModel = new RightPannelViewModel();
            _connectPannelViewModel = new ConnectPannelViewModel();
            SwitchView = 0;
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

        public ConnectPannelViewModel ConnectViewModel
        {
            get
            {
                return _connectPannelViewModel;
            }
        }
    }
}
