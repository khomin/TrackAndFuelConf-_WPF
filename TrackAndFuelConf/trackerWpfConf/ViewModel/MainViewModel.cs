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
        private bool _showLoadSpinner = false;

        public bool ShowLoadSpinner 
        {
            //get => _showLoadSpinner;
            set 
            {
                _showLoadSpinner = value;
                OnPropertyChanged();
            }
        }
    }
}
