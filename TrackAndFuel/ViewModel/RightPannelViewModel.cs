using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackAndFuel.ViewModel
{
    public class RightPannelViewModel : ViewModelBase
    {
        private CurrentDataViewModel _currentDataViewModel = new CurrentDataViewModel();
        private StatusDataViewModel _statusDataViewModel = new StatusDataViewModel();

        public CurrentDataViewModel CurrentData
        {
            get { return _currentDataViewModel; }
        }

        public StatusDataViewModel StatusInfo
        {
            get { return _statusDataViewModel; }
        }
    }
}
