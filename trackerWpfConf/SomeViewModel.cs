using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFRssFeedReader.ViewModel;

namespace trackerWpfConf
{
    public class SomeViewModel : BaseViewModel
    {
        private string _firstNane = "default value";

        public SomeViewModel()
        {
        }

        public string FirstName { 
            get { 
                return _firstNane;  
            }
            set 
            {
                _firstNane = value;
                OnPropertyChanged(this, "FirstName");
            }
        }
    }
}
