using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelBase;

namespace trackerWpfConf.ViewModel
{
    class RightPannelViewModel : ViewModelBase.ViewModelBase
    {
            private string address;

            public string Address
            {
                get { return address; }
                set
                {
                    address = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}
