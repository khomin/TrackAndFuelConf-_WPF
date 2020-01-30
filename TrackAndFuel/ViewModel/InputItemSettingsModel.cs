using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackAndFuel.ViewModel
{
    public class InputItemSettingsModel : ViewModelBase
    {
        private ObservableCollection<string> _portRoleList;
        private PortRole _portRoleIndex;
        private string _portLineName = "";
        private bool _availableAsIgnitionSensor = false;
        private int _signalAnalysysTime = 250;
        private int _thresholdUpper = 0;
        private int _thresholdLower = 0;
        private int _thresholdLevelOfFixationFrequency = 0;
        private bool _useFiltrating = false;
        private int _averagingWindowValue = 1;
        private int _levelOfFiltrationValue = 0;

        enum PortRole
        {
            notUsed = 0,
            discrete_NO_minus = 1,
            discrete_NC_minus = 2,
            discrete_NO_plus = 3,
            discrete_NC_plus = 4,
            pulseCounter = 5,
            frequencySensor = 6,
            voltageMeasurement = 7,
            tachometer = 8
        };

        public InputItemSettingsModel(string name, bool itCanUseAsIgnitionDetectPort)
        {
            AvailableAsIgnitionSensor = itCanUseAsIgnitionDetectPort;
            PortRoleIndex = 0;
            _portRoleList = new ObservableCollection<string>();
            PortRoleList.Add("Not used");
            PortRoleList.Add("Discrete input");
            //PortRoleList.Add("Pulse counter");
            PortRoleList.Add("Frequency sensor");
            PortRoleList.Add("Voltage measurement");
            //PortRoleList.Add("Tachometer");
            _portLineName = name;
        }

        public int PortRoleIndex
        {
            get => (int)_portRoleIndex; set
            {
                _portRoleIndex = (PortRole)value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<string> PortRoleList { get => _portRoleList; }
        public string PortLineName
        {
            get => _portLineName; set
            {
                _portLineName = value;
                OnPropertyChanged();
            }
        }

        public bool AvailableAsIgnitionSensor
        {
            get => _availableAsIgnitionSensor;
            set
            {
                _availableAsIgnitionSensor = value;
                OnPropertyChanged();
            }
        }

        public int SignalAnalysysTime
        {
            get => _signalAnalysysTime;
            set
            {
                _signalAnalysysTime = value;
                OnPropertyChanged();
            }
        }

        public int ThresholdUpper
        {
            get => _thresholdUpper;
            set
            {
                _thresholdUpper = value;
                OnPropertyChanged();
            }
        }
        public int ThresholdLower
        {
            get => _thresholdLower;
            set
            {
                _thresholdLower = value;
                OnPropertyChanged();
            }
        }
        public int ThresholdLevelOfFixationFrequency
        {
            get => _thresholdLevelOfFixationFrequency;
            set
            {
                _thresholdLevelOfFixationFrequency = value;
                OnPropertyChanged();
            }
        }

        public bool UseFiltrating
        {
            get => _useFiltrating;
            set
            {
                _useFiltrating = value;
                OnPropertyChanged();
            }
        }
        public int AveragingWindowValue
        {
            get => _averagingWindowValue;
            set
            {
                _averagingWindowValue = value;
                OnPropertyChanged();
            }
        }
        public int LevelOfFiltrationValue
        {
            get => _levelOfFiltrationValue;
            set
            {
                _levelOfFiltrationValue = value;
                OnPropertyChanged();
            }
        }
    }
}
