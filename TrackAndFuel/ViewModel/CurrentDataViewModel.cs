using MetroDemo.Core;
using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackAndFuel.ViewModel
{
    public class CurrentDataViewModel : ViewModelBase
    {
        private string _imeiModemValue = "";
        private float _ain1Value = 0.96f;
        private float _ain2Value = 0.0f;
        private float _ain3Value = 0.0f;
        private float _gmsSignalStrenghtPercentValue = 0;
        private int _gnssSatFixValue = 0;
        private float _gnssLatValue = 0.0f;
        private float _gnssLonValue = 0.0f;
        private string _gnssPositionAsString = "";
        private float _powerBatteryValue = 0.0f;
        private float _powerExternalValue = 0.0f;
        private Boolean _inDiscret1Value = false;
        private Boolean _inDiscret2Value = false;
        private Boolean _inDiscret3Value = false;
        private Boolean _inDiscret4Value = false;
        private Boolean _outDiscret1Value = false;
        private Boolean _outDiscret2Value = false;
        private readonly ObservableCollection<LogPoint> _logPositionList;
        private int _logPositionListIndex = 0;
        
        public class LogPoint
        {
            public UInt32 Id { get; set; }
            public double Lon { get; set; }
            public double Lat { get; set; }
            public DateTime Datetime { get; set; }
            public LogPoint(UInt32 id, double lon, double lat, DateTime dateTime)
            {
                this.Id = id;
                this.Lon = lon;
                this.Lat = lat;
                this.Datetime = dateTime;
            }
        }
        private Map map;

        public CurrentDataViewModel()
        {
            _logPositionList = new ObservableCollection<LogPoint>();
        }

        public string ImeiModemValue
        {
            get => _imeiModemValue;
            set
            {
                _imeiModemValue = value;
                OnPropertyChanged();
            }
        }

        public float Ain1Value
        {
            get => _ain1Value;
            set
            {
                _ain1Value = value;
                OnPropertyChanged();
            }
        }

        public float Ain2Value
        {
            get => _ain2Value;
            set
            {
                _ain2Value = value;
                OnPropertyChanged();
            }
        }

        public float Ain3Value
        {
            get => _ain3Value;
            set
            {
                _ain3Value = value;
                OnPropertyChanged();
            }
        }

        public float GmsSignalStrenghtPercentValue
        {
            get => _gmsSignalStrenghtPercentValue;
            set
            {
                _gmsSignalStrenghtPercentValue = value;
                OnPropertyChanged();
            }
        }

        public int GnssSatFixValue
        {
            get => _gnssSatFixValue;
            set
            {
                _gnssSatFixValue = value;
                OnPropertyChanged();
                GnssPositionAsString = value.ToString() + "," + _gnssLatValue.ToString();
            }
        }

        public float GnssLatValue
        {
            get => _gnssLatValue;
            set
            {
                _gnssLatValue = value;
                OnPropertyChanged();
                GnssPositionAsString = value.ToString() + "," + _gnssLonValue.ToString();
            }
        }

        public float GnssLonValue
        {
            get => _gnssLonValue;
            set
            {
                _gnssLonValue = value;
                OnPropertyChanged();
            }
        }

        public float PowerBatteryValue
        {
            get => _powerBatteryValue;
            set
            {
                _powerBatteryValue = value;
                OnPropertyChanged();
            }
        }

        public float PowerExternalValue
        {
            get => _powerExternalValue;
            set
            {
                _powerExternalValue = value;
                OnPropertyChanged();
            }
        }

        public bool InDiscret1Value
        {
            get => _inDiscret1Value;
            set
            {
                _inDiscret1Value = value;
                OnPropertyChanged();
            }
        }

        public bool InDiscret2Value
        {
            get => _inDiscret2Value;
            set
            {
                _inDiscret2Value = value;
                OnPropertyChanged();
            }
        }

        public bool InDiscret3Value
        {
            get => _inDiscret3Value;
            set
            {
                _inDiscret3Value = value;
                OnPropertyChanged();
            }
        }


        public bool InDiscret4Value
        {
            get => _inDiscret4Value;
            set
            {
                _inDiscret4Value = value;
                OnPropertyChanged();
            }
        }

        public bool OutDiscret1Value
        {
            get => _outDiscret1Value;
            set
            {
                _outDiscret1Value = value;
                OnPropertyChanged();
            }
        }

        public bool OutDiscret2Value
        {
            get => _outDiscret2Value;
            set
            {
                _outDiscret2Value = value;
                OnPropertyChanged();
            }
        }

        public string GnssPositionAsString
        {
            get => _gnssPositionAsString;
            set
            {
                _gnssPositionAsString = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<LogPoint> LogPositionList => _logPositionList;

        public int LogPositionListIndex
        {
            get => _logPositionListIndex;
            set
            {
                _logPositionListIndex = value;
                OnPropertyChanged();
            }
        }

        public Map Map { get => map; set => map = value; }
    }
}
