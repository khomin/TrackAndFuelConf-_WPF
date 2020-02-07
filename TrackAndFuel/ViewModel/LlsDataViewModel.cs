using MetroDemo.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TrackAndFuel.ViewModel
{
    public class LlsDataViewModel : ViewModelBase, IDataErrorInfo, IDisposable
    {
        private Action<bool> _settingsIsChangedCallbackNotify;
        private readonly ObservableCollection<string> _tempCompensationMode;
        private TemperatureCompenstationModeType _tempCompenstationModeIndex = TemperatureCompenstationModeType.Off;
        enum TemperatureCompenstationModeType
        {
            Off,
            Petrol95,
            Petrol92,
            Petrol80_0_25,
            Petrol80_min25_25,
            Diesel_0_25,
            Diesel_min25_25,
            UserMode
        };

        private string _tempCompensationCoef1 = "12.34";
        private bool _tempCompensationCoef1IsValid;

        private string _tempCompensationCoef2 = "01.98";
        private bool _tempCompensationCoef2IsValid;

        private string _minLevel = "100";
        private bool _minLevelIsValid;

        private string _maxLevel = "200";
        private bool _maxLevelIsValid;

        private bool _typeOutMessageIsRelativeLevel = false;
        private readonly ObservableCollection<string> _typeOfInterpolation;
        private InterpolationType _typeOfInterpolationIndex = InterpolationType.Lineally;
        enum InterpolationType
        {
            Lineally,
            Quadratic,
            Spline
        };

        private readonly ObservableCollection<string> _typeOfFiltration;
        private FiltrationType _typeOfFiltrationIndex = FiltrationType.Off;
        enum FiltrationType
        {
            Off,
            Average,
            Median,
            Adaptive
        };

        private string _filtrationAveragingTime = "10";
        private bool _filtrationAveragingTimeIsValid;

        private string _filtrationLenghtOfMedian = "2";
        private bool _filtrationLenghtOfMedianIsValid;

        private string _filtrationProcessNoiseCovarianceQ = "3";
        private bool _filtrationProcessNoiseCovarianceQIsValid;

        private string _filtrationMeasurementNoiseCovarianceR = "4";
        private bool _filtrationMeasurementNoiseCovarianceRIsValid;

        private bool _errorMinLimitLessThan10Percent = false;
        private bool _errorMaxLimitIsExceededBy10Percent = false;
        private bool _measuringOscillatorFrequency0Hz = false;

        private bool _sensorIsOffline = true;
        private string _sensorCntValue = "444";
        private string _sensorLevelVolume = "333";
        private string _sensorFrequency = "555";
        private string _sensorLevelInPercent = "80";

        public class CalibrateTable : ViewModelBase, IDataErrorInfo, IDisposable
        {
            private int _value;
            private int _level;

            public CalibrateTable(int value, int level) 
            {
                _value = value;
                _level  = level;
            }

            public string this[string columnName] 
            {
                get => null;
            }

            public int Value
            {
                get => _value;
                set => Set(ref _value, value);
            }
            public int Level
            {
                get => _level;
                set => Set(ref _level, value);
            }

            public string Error => string.Empty;

            public void Dispose()
            {
                
            }
        }

        private readonly ObservableCollection<CalibrateTable> _calibrateTables;
        private int _calibrateTablesIndex = 0;

        public LlsDataViewModel(Action<bool> settingsIsChangedCallbackNotify)
        {
            _settingsIsChangedCallbackNotify = settingsIsChangedCallbackNotify;
            _tempCompensationMode = new ObservableCollection<string>();
            _tempCompensationMode.Add("Off");
            _tempCompensationMode.Add("Petrol-95");
            _tempCompensationMode.Add("Petrol-92");
            _tempCompensationMode.Add("Petrol-80(0°...+25°)");
            _tempCompensationMode.Add("Petrol-80(-25°...+25°)");
            _tempCompensationMode.Add("Diesel(0°...+25°)");
            _tempCompensationMode.Add("Diesel(-25°...+25°)");
            _tempCompensationMode.Add("User mode");

            _typeOfInterpolation = new ObservableCollection<string>();
            _typeOfInterpolation.Add("Lineally");
            _typeOfInterpolation.Add("Quadratic");
            _typeOfInterpolation.Add("Spline");

            _typeOfFiltration = new ObservableCollection<string>();
            _typeOfFiltration.Add("Off");
            _typeOfFiltration.Add("Average");
            _typeOfFiltration.Add("Median");
            _typeOfFiltration.Add("Adaptive");

            _calibrateTables = new ObservableCollection<CalibrateTable>();
            _calibrateTables.Add(new CalibrateTable(50, 10));
            _calibrateTables.Add(new CalibrateTable(100, 200));
        }
        public ObservableCollection<string> TempCompensationMode { get => _tempCompensationMode; }
        public int TempCompenstationModeIndex
        {
            get => (int)_tempCompenstationModeIndex; 
            set
            {
                _tempCompenstationModeIndex = (TemperatureCompenstationModeType)value;
                OnPropertyChanged();
            }
        }
        public string TempCompensationCoef1
        {
            get => _tempCompensationCoef1;
            set => this.Set(ref this._tempCompensationCoef1, value);
        }
        public string TempCompensationCoef2
        {
            get => _tempCompensationCoef2;
            set => Set(ref _tempCompensationCoef2, value);
        }
        public string MinLevel
        {
            get => _minLevel;
            set => Set(ref _minLevel, value);
        }
        public string MaxLevel
        {
            get => _maxLevel;
            set => Set(ref _maxLevel, value);
        }
        public bool TypeOutMessageIsRelativeLevel
        {
            get => _typeOutMessageIsRelativeLevel;
            set
            {
                _typeOutMessageIsRelativeLevel = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<string> TypeOfInterpolation { get => _typeOfInterpolation; }
        public int TypeOfInterpolationIndex
        {
            get => (int)_typeOfInterpolationIndex;
            set
            {
                _typeOfInterpolationIndex = (InterpolationType)value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<string> TypeOfFiltration { get => _typeOfFiltration; }
        public int TypeOfFiltrationIndex
        {
            get => (int)_typeOfFiltrationIndex;
            set
            {
                _typeOfFiltrationIndex = (FiltrationType)value;
                OnPropertyChanged();
            }
        }
        public string FiltrationAveragingTime
        {
            get => _filtrationAveragingTime;
            set => Set(ref _filtrationAveragingTime, value);
        }
        public string FiltrationLenghtOfMedian
        {
            get => _filtrationLenghtOfMedian;
            set => Set(ref _filtrationLenghtOfMedian, value);
        }
        public string FiltrationProcessNoiseCovarianceQ
        {
            get => _filtrationProcessNoiseCovarianceQ;
            set => Set(ref _filtrationProcessNoiseCovarianceQ, value);
        }
        public string FiltrationMeasurementNoiseCovarianceR
        {
            get => _filtrationMeasurementNoiseCovarianceR;
            set => Set(ref _filtrationMeasurementNoiseCovarianceR, value);
        }

        public bool ErrorMinLimitLessThan10Percent
        {
            get => _errorMinLimitLessThan10Percent;
            set
            {
                _errorMinLimitLessThan10Percent = value;
                OnPropertyChanged();
            }
        }
        public bool ErrorMaxLimitIsExceededBy10Percent
        {
            get => _errorMaxLimitIsExceededBy10Percent;
            set
            {
                _errorMaxLimitIsExceededBy10Percent = value;
                OnPropertyChanged();
            }
        }
        public bool MeasuringOscillatorFrequency0Hz
        {
            get => _measuringOscillatorFrequency0Hz;
            set
            {
                _measuringOscillatorFrequency0Hz = value;
                OnPropertyChanged();
            }
        }

        public bool SensorIsOffline
        {
            get => _sensorIsOffline;
            set
            {
                _sensorIsOffline = value;
                OnPropertyChanged();
            }
        }
        public string SensorCntValue
        {
            get => _sensorCntValue;
            set => Set(ref _sensorCntValue, value);
        }
        public string SensorLevelVolume
        {
            get => _sensorLevelVolume;
            set
            {
                _sensorLevelVolume = value;
                OnPropertyChanged();
            }
        }
        public string SensorFrequency
        {
            get => _sensorFrequency;
            set
            {
                _sensorFrequency = value;
                OnPropertyChanged();
            }
        }
        public string SensorLevelInPercent
        {
            get => _sensorLevelInPercent;
            set
            {
                _sensorLevelInPercent = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<CalibrateTable> CalibrateTables => _calibrateTables;

        public int CalibrateTablesIndex
        {
            get => _calibrateTablesIndex; 
            set
            {
                _calibrateTablesIndex = value;
                OnPropertyChanged();
            }
        }

        public string Error => string.Empty;

        /**/
        /* validation */
        /**/
        Regex regexFloat = new Regex("^[0-9]*(?:\\.[0-9]*)?$");
        Regex regexMinLelel = new Regex("^([0-9]|[1-8][0-9]|9[0-9]|[1-8][0-9]{2}|9[0-8][0-9]|99[0-9]|10[01][0-9]|102[0-3])$");
        Regex regexMaxLevel = new Regex("^([0-9]|[1-8][0-9]|9[0-9]|[1-8][0-9]{2}|9[0-8][0-9]|99[0-9]|[1-3][0-9]{3}|40[0-8][0-9]|409[0-5])$");
        Regex regexAveragingTime = new Regex("^([0-9]|1[0-9]|2[01])$");
        Regex regexFiltrationAveragingTime = new Regex("^([0-7])$");
        
        public string this[string columnName]
        {
            get
            {
                string resultMessage = "";
                if (columnName == nameof(TempCompensationCoef1))
                {
                    _tempCompensationCoef1IsValid = regexFloat.IsMatch(this.TempCompensationCoef1) && (this.TempCompensationCoef1.Length != 0);
                    if (!_tempCompensationCoef1IsValid)
                    {
                        resultMessage = "Value is not valid!";
                    }
                }
                if (columnName == nameof(TempCompensationCoef2))
                {
                    _tempCompensationCoef2IsValid = regexFloat.IsMatch(this.TempCompensationCoef2) && (this.TempCompensationCoef2.Length != 0);
                    if (!_tempCompensationCoef2IsValid)
                    {
                        resultMessage = "Value is not valid!";
                    }
                }
                if (columnName == nameof(MinLevel))
                {
                    _minLevelIsValid = regexMinLelel.IsMatch(this.MinLevel);
                    if (!_minLevelIsValid)
                    {
                        resultMessage = "Value is not valid!";
                    }
                }
                if (columnName == nameof(MaxLevel))
                {
                    _maxLevelIsValid = regexMaxLevel.IsMatch(this.MaxLevel);
                    if (!_maxLevelIsValid)
                    {
                        resultMessage = "Value is not valid!";
                    }
                }
                if (columnName == nameof(FiltrationAveragingTime))
                {
                    _filtrationAveragingTimeIsValid = regexAveragingTime.IsMatch(this.FiltrationAveragingTime);
                    if (!_filtrationAveragingTimeIsValid)
                    {
                        resultMessage = "Value is not valid!";
                    }
                }
                if (columnName == nameof(FiltrationLenghtOfMedian))
                {
                    _filtrationLenghtOfMedianIsValid = regexFiltrationAveragingTime.IsMatch(this.FiltrationLenghtOfMedian);
                    if (!_filtrationLenghtOfMedianIsValid)
                    {
                        resultMessage = "Value is not valid!";
                    }
                }
                if (columnName == nameof(FiltrationProcessNoiseCovarianceQ))
                {
                    _filtrationProcessNoiseCovarianceQIsValid = regexFloat.IsMatch(this.FiltrationProcessNoiseCovarianceQ) && (this.FiltrationProcessNoiseCovarianceQ.Length != 0);
                    if (!_filtrationProcessNoiseCovarianceQIsValid)
                    {
                        resultMessage = "Value is not valid!";
                    }
                }
                if (columnName == nameof(FiltrationMeasurementNoiseCovarianceR))
                {
                    _filtrationMeasurementNoiseCovarianceRIsValid = regexFloat.IsMatch(this.FiltrationMeasurementNoiseCovarianceR) && (this.FiltrationMeasurementNoiseCovarianceR.Length != 0);
                    if (!_filtrationMeasurementNoiseCovarianceRIsValid)
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
            _settingsIsChangedCallbackNotify.Invoke(_tempCompensationCoef1IsValid && _tempCompensationCoef2IsValid 
                && _minLevelIsValid && _maxLevelIsValid && _filtrationAveragingTimeIsValid && _filtrationLenghtOfMedianIsValid 
                && _filtrationProcessNoiseCovarianceQIsValid && _filtrationMeasurementNoiseCovarianceRIsValid);
        }
        public void Dispose() { }
    }
}
