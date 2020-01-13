using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace trackerWpfConf.ViewModel
{
    class SettingsConnectionViewModel : ViewModelBase
    {
        string header;
        
        public string Header
        {
            get { return header; }
            set
            {
                header = value;
                OnPropertyChanged();
            }
        }

        private string address;

        public string Address
        {
            get { return address; }
            set { 
                address = value;
                OnPropertyChanged();
            }
        }
    }
}
