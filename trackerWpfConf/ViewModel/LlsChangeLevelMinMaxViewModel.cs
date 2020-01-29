using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace trackerWpfConf.ViewModel
{
    public class LlsChangeLevelMinMaxViewModel : ViewModelBase
    {
        private int _minLevel;
        private int _maxLevel;

        public LlsChangeLevelMinMaxViewModel() 
        {
            _minLevel = 0;
            _maxLevel = 0;
        }

        public int MinLevel
        {
            get => _minLevel; 
            set
            {
                _minLevel = value;
                OnPropertyChanged();
            }
        }
        public int MaxLevel
        {
            get => _maxLevel; set
            {
                _maxLevel = value;
                OnPropertyChanged();
            }
        }
    }
}
