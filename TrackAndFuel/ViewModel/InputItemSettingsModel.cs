using MetroDemo.Core;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text.RegularExpressions;
using static TrackAndFuel.ViewModel.SettingsViewModel;

namespace TrackAndFuel.ViewModel
{
    public class InputItemSettingsModel : ViewModelBase, IDataErrorInfo, IDisposable
    {
        private Action<bool> _settingsIsChangedCallbackNotify;
        private ObservableCollection<string> _portRoleList;
        private PortRole _portRoleIndex;
        private readonly string _portHeader = "";
        private string _portName = "Something pin name";
        private bool _portNameIsValid;
        private Action<bool> _useIngtionUpdatedNotify;
        private bool _usePinAsIgnitionDetection = false;
        private int _signalAnalysysTime = 250;
        private int _thresholdUpper = 0;
        private int _thresholdLower = 0;
        private int _thresholdLevelOfFixationFrequency = 0;
        private bool _useFiltrating = false;
        private int _averagingWindowValue = 1;
        private int _levelOfFiltrationValue = 0;

        public enum PortRole
        {
            notUsed = 0,
            discrete = 1,
            frequencySensor = 2,
            voltageMeasurement = 3
        };

        public InputItemSettingsModel(string portHeader, string portName, Action<bool> useIngtionUpdatedNotify, 
            Action<bool> settingsIsChangedCallbackNotify)
        {
            _settingsIsChangedCallbackNotify = settingsIsChangedCallbackNotify;
            _useIngtionUpdatedNotify = useIngtionUpdatedNotify;
            PortRoleIndex = 0;
            _portRoleList = new ObservableCollection<string>();
            PortRoleList.Add("Not used");
            PortRoleList.Add("Discrete input");
            //PortRoleList.Add("Pulse counter");
            PortRoleList.Add("Frequency sensor");
            PortRoleList.Add("Voltage measurement");
            //PortRoleList.Add("Tachometer");
            _portName = portName;
            _portHeader = portHeader;
        }

        public int PortRoleIndex
        {
            get => (int)_portRoleIndex; set
            {
                _portRoleIndex = (PortRole)value;
                OnPropertyChanged();
                NofifySettingsIsChanged();
            }
        }

        public ObservableCollection<string> PortRoleList { get => _portRoleList; }
        public string PortLineName
        {
            get => _portName;
            set => this.Set(ref this._portName, value);
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

        public string Error => string.Empty;

        public string PortHeader { get => _portHeader; }
        public bool UsePinAsIgnitionDetection
        {
            get => _usePinAsIgnitionDetection;
            set
            {
                _usePinAsIgnitionDetection = value;
                _useIngtionUpdatedNotify.Invoke(value);
                OnPropertyChanged();
            }
        }

        /**/
        /* validation */
        /**/
        Regex regexPortName = new Regex("^[\\d-a-z-A-Z]{1,16}$");

        public string this[string columnName]
        {
            get
            {
                string resultMessage = "";
                if (columnName == nameof(PortLineName))
                {
                    _portNameIsValid = regexPortName.IsMatch(this.PortLineName);
                    if (!_portNameIsValid)
                    {
                        resultMessage = "Value is not valid!";
                    }
                }
                NofifySettingsIsChanged();
                return resultMessage.Length == 0 ? null : resultMessage;
            }
        }
        private void NofifySettingsIsChanged()
        {
            _settingsIsChangedCallbackNotify.Invoke(_portRoleIndex != PortRole.notUsed ? _portNameIsValid : true);
        }
        public void Dispose() { }
    }
}
