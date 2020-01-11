using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace trackerWpfConf.ViewModel
{
    class StatusDataViewModel : ViewModelBase
    {
        private Boolean _crystal8Mhz;
        private Boolean _crystal16KHz;

        public Boolean Crystal8MHz
        {
            get { return _crystal8Mhz; }
            set
            {
                _crystal8Mhz = value;
                OnPropertyChanged();
            }
        }

        public Boolean Crystal16KHz
        {
            get { return _crystal16KHz; }
            set
            {
                _crystal16KHz = value;
                OnPropertyChanged();
            }
        }
    }
}
