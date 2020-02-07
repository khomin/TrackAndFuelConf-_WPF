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
        private Action<bool> _settingsIsChangedCallbackNotify;
        private bool _isEnable = false;
        private string _hexCode = "";
        private bool _hexCodeIsValid = false;

        private string _sensorName = "";
        private bool _sensorNameIsValid = false;

        private int lowerAlarmZone = 0;
        private int upperAlarmZone = 0;

        public OneWireItemModel(Action<bool> settingsIsChangedCallbackNotify) 
        {
            _settingsIsChangedCallbackNotify = settingsIsChangedCallbackNotify;
        }

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
            set => this.Set(ref this._hexCode, value);
        }
        public string SensorName
        {
            get => _sensorName;
            set => this.Set(ref this._sensorName, value);
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
                string resultMessage = "";
                if (columnName == nameof(HexCode))
                {
                    _hexCodeIsValid = regexOneWireName.IsMatch(this.HexCode);
                    if (!_hexCodeIsValid)
                    {
                        resultMessage = "Value is not valid!";
                    }
                }

                if (columnName == nameof(SensorName))
                {
                    _sensorNameIsValid = regexSensorName.IsMatch(this.SensorName);
                    if (!_sensorNameIsValid)
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
            _settingsIsChangedCallbackNotify.Invoke(_hexCodeIsValid && _sensorNameIsValid);
        }

        public void Dispose() { }
        public string Error => string.Empty;
    }
}
