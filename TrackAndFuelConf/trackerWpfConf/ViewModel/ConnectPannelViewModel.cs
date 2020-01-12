using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace trackerWpfConf.ViewModel
{
    class ConnectPannelViewModel : ViewModelBase
    {
        private ObservableCollection<string> _portList;

        public ConnectPannelViewModel()
        {
            _portList = new ObservableCollection<string> { "COM1", "COM2" };
        }

        public ObservableCollection<string> PortsList {
            get {
                return _portList;
            }
        }

        public void ResearchPorts() 
        {
            _portList = new ObservableCollection<string> { "COM1", "COM2" };
        }
    }
}
