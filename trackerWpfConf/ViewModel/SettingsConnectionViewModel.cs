using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace trackerWpfConf.ViewModel
{
    public class SettingsConnectionViewModel : ViewModelBase
    {
        private string _ipDnsAddress = "ya.ru";

        public string IpDnsAddress
        {
            get => _ipDnsAddress; 
            set
            {
                _ipDnsAddress = value;
                OnPropertyChanged();
            }
        }
    }
}
