using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace trackerWpfConf.ViewModel
{
    class RightPannelViewModel : ViewModelBase
    {
        private readonly CurrentDataViewModel _currentDataViewModel = new CurrentDataViewModel();
        private readonly StatusDataViewModel _statusDataViewModel = new StatusDataViewModel();

        public CurrentDataViewModel Model
        {
            get { return _currentDataViewModel; }
        }

        public StatusDataViewModel StatusDataViewModel
        {
            get { return _statusDataViewModel; }
        }
    }
}
