using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace trackerWpfConf.ViewModel
{
    public class LlsDataViewModel : ViewModelBase
    {
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

        private float _tempCompensationCoef1 = 12.34f;
        private float _tempCompensationCoef2 = 01.98f;
        private int _minLevel = 100;
        private int _maxLevel = 200;
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

        private int _filtrationAveragingTime = 10;
        private int _filtrationLenghtOfMedian = 2;
        private int _filtrationProcessNoiseCovarianceQ = 3;
        private int _filtrationMeasurementNoiseCovarianceR = 4;

        private bool _errorMinLimitLessThan10Percent = false;
        private bool _errorMaxLimitIsExceededBy10Percent = false;
        private bool _measuringOscillatorFrequency0Hz = false;

        private bool _sensorIsOffline = true;
        private int _sensorCntValue = 444;
        private int _sensorLevelVolume = 333;
        private int _sensorFrequency = 555;
        private int _sensorLevelInPercent = 80;

        public class CalibrateTable : ViewModelBase
        {
            private int _value;
            private int _level;

            public CalibrateTable(int value, int level) 
            {
                _value = value;
                _level  = level;
            }

            public int Value
            {
                get => _value; set
                {
                    _value = value;
                    OnPropertyChanged();
                }
            }
            public int Level
            {
                get => _level;
                set
                {
                    _level = value;
                    OnPropertyChanged();
                }
            }
        }

        private readonly ObservableCollection<CalibrateTable> _calibrateTables;
        private int _calibrateTablesIndex = 0;

        public LlsDataViewModel()
        {
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
            get => (int)_tempCompenstationModeIndex; set
            {
                _tempCompenstationModeIndex = (TemperatureCompenstationModeType)value;
                OnPropertyChanged();
            }
        }
        public float TempCompensationCoef1
        {
            get => _tempCompensationCoef1;
            set
            {
                _tempCompensationCoef1 = value;
                OnPropertyChanged();
            }
        }
        public float TempCompensationCoef2
        {
            get => _tempCompensationCoef2;
            set
            {
                _tempCompensationCoef2 = value;
                OnPropertyChanged();
            }
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
            get => _maxLevel;
            set
            {
                _maxLevel = value;
                OnPropertyChanged();
            }
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
        public int FiltrationAveragingTime
        {
            get => _filtrationAveragingTime;
            set
            {
                _filtrationAveragingTime = value;
                OnPropertyChanged();
            }
        }
        public int FiltrationLenghtOfMedian
        {
            get => _filtrationLenghtOfMedian;
            set
            {
                _filtrationLenghtOfMedian = value;
                OnPropertyChanged();
            }
        }
        public int FiltrationProcessNoiseCovarianceQ
        {
            get => _filtrationProcessNoiseCovarianceQ;
            set
            {
                _filtrationProcessNoiseCovarianceQ = value;
                OnPropertyChanged();
            }
        }
        public int FiltrationMeasurementNoiseCovarianceR
        {
            get => _filtrationMeasurementNoiseCovarianceR;
            set
            {
                _filtrationMeasurementNoiseCovarianceR = value;
                OnPropertyChanged();
            }
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
        public int SensorCntValue
        {
            get => _sensorCntValue;
            set
            {
                _sensorCntValue = value;
                OnPropertyChanged();
            }
        }
        public int SensorLevelVolume
        {
            get => _sensorLevelVolume;
            set
            {
                _sensorLevelVolume = value;
                OnPropertyChanged();
            }
        }
        public int SensorFrequency
        {
            get => _sensorFrequency;
            set
            {
                _sensorFrequency = value;
                OnPropertyChanged();
            }
        }
        public int SensorLevelInPercent
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
    }
}
