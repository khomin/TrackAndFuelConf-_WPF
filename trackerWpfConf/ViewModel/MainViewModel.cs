using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace trackerWpfConf.ViewModel
{
    class MainViewModel : ViewModelBase
    {
        private readonly RightPannelViewModel _rightPannelViewModel;
        
        private readonly ConnectPannelViewModel _connectPannelViewModel;

        public MainViewModel() 
        {
            _rightPannelViewModel = new RightPannelViewModel();
            _connectPannelViewModel = new ConnectPannelViewModel();
        }

        public RightPannelViewModel RightPannelViewModel
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

        private string _statusConnect = "na";

        public string StatusConnect 
        {
            get => _statusConnect;
        }
    }
}
