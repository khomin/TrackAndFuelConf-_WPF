using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using testViewModel;

namespace trackerWpfConf.ViewModel
{
    class SettingsConnectionViewModel : ViewModelBase
    {
        string _header;
        public string Header
        {
            get { return _header; }
            set
            {
                _header = value;
                OnPropertyChanged();
            }
        }
    }
}
