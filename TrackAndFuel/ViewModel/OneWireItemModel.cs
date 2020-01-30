using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackAndFuel.ViewModel
{
    public class OneWireItemModel : ViewModelBase
    {
        private bool _isEnable = false;
        private string _hexCode = "";
        private string _sensorName = "";
        private int lowerAlarmZone = 0;
        private int upperAlarmZone = 0;

        public bool IsEnable
        {
            get => _isEnable;
            set
            {
                _isEnable = value;
                OnPropertyChanged();
            }
        }
        public string HexCode
        {
            get => _hexCode;
            set
            {
                _hexCode = value;
                OnPropertyChanged();
            }
        }
        public string SensorName
        {
            get => _sensorName;
            set
            {
                _sensorName = value;
                OnPropertyChanged();
            }
        }

        public int LowerAlarmZone { get => lowerAlarmZone;
            set
            {
                lowerAlarmZone = value;
                OnPropertyChanged();
            }
        }
        public int UpperAlarmZone { get => upperAlarmZone;
            set
            {
                upperAlarmZone = value;
                OnPropertyChanged();
            }
        }
    }
}
