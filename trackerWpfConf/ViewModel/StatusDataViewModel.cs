using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using trackerWpfConf.Instrumentals;

namespace trackerWpfConf.ViewModel
{
    class StatusDataViewModel : ViewModelBase
    {
        private Boolean _crystal8Mhz;
        private Boolean _crystal16KHz;
        private ObservableCollection<LogItem> _log = new ObservableCollection<LogItem>();

        public class LogItem
        {
            public TrackerTypeData.KeyParameter Type { get; set; }
            public string Message { get; set; }
        }

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

        public ObservableCollection<LogItem> Log 
        {
            get => _log;
            set 
            {
                _log = value;
                OnPropertyChanged();
            }
        }
    }
}
