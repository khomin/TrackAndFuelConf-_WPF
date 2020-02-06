using MetroDemo.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TrackAndFuel.ViewModel
{
    public class OneWireItemModel : ViewModelBase, IDataErrorInfo, IDisposable
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
        /**/
        /* validation */
        /**/
        Regex regexOneWireName = new Regex("^([a-fA-Z]|[0-9]){16}$");
        Regex regexSensorName = new Regex("^[0-9-A-Z-a-z]{1,16}$");

        public string this[string columnName]
        {
            get
            {
                if (columnName == nameof(HexCode))
                {
                    if (!regexOneWireName.IsMatch(this.HexCode))
                    {
                        return "Value is not valid!";
                    }
                }

                if (columnName == nameof(SensorName))
                {
                    if (!regexSensorName.IsMatch(this.SensorName))
                    {
                        return "Value is not valid!";
                    }
                }
                return null;
            }
        }
        public void Dispose() { }
        public string Error => string.Empty;
    }
}
