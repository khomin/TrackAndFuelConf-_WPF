
using System.Windows.Controls;

namespace trackerWpfConf.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private RightPannelViewModel _rightPannelViewModel;
        
        private ConnectPannelViewModel _connectPannelViewModel;

        private Page _content;

        public MainViewModel() 
        {
            _rightPannelViewModel = new RightPannelViewModel();
            _connectPannelViewModel = new ConnectPannelViewModel();
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
