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
    public class SettingsConnectionViewModel : ViewModelBase, IDataErrorInfo, IDisposable
    {
        private Action<bool> _settingsIsChangedCallbackNotify;
        private bool _settingsIsValid = false;

        private string _ipDnsAddress = "www.lkshuuuyaayooo.ru";
        private bool _ipDnsAddressIsValid = false;

        private string _port = "1000";
        private bool _portIsValid = false;


        private ObservableCollection<string> _protocolType; // combox
        private int _protocolTypeIndex = 0;

        private ObservableCollection<string>  _periodOfPingShortMessage; // combox
        private int _periodOfPingShortMessageIndex = 0;

        private int _delayBeforeNextAttempConnecting = 30; // slider
        private int _sendingPeriodDuringParking = 30; // slider
        private int _sendingPeriodInSleepMode = 30; // slider
        private bool _serverIsEnabled = false;

        public SettingsConnectionViewModel(Action<bool> settingsIsChangedCallbackNotify) 
        {
            _settingsIsChangedCallbackNotify = settingsIsChangedCallbackNotify;
            _protocolType = new ObservableCollection<string>();
            _protocolType.Add("Wialon");

            _periodOfPingShortMessage = new ObservableCollection<string>();
            _periodOfPingShortMessage.Add("15 sec");
            _periodOfPingShortMessage.Add("60 sec");
            _periodOfPingShortMessage.Add("3 min");
            NofifySettingsIsChanged();
        }

        public string IpDnsAddress
        {
            get => _ipDnsAddress; 
            set => this.Set(ref this._ipDnsAddress, value);
        }

        public int DelayBeforeNextAttempConnecting
        {
            get => _delayBeforeNextAttempConnecting;
            set
            {
                _delayBeforeNextAttempConnecting = value;
                OnPropertyChanged();
            }
        }

        public string Port
        {
            get => _port; 
            set => this.Set(ref this._port, value);
        }

        public ObservableCollection<string> ProtocolType => _protocolType;

        public ObservableCollection<string> PeriodOfPingShortMessage
        {
            get => _periodOfPingShortMessage;
            set
            {
                _periodOfPingShortMessage = value;
                OnPropertyChanged();
            }
        }
        public int SendingPeriodDuringParking
        {
            get => _sendingPeriodDuringParking;
            set
            {
                _sendingPeriodDuringParking = value;
                OnPropertyChanged();
            }
        }

        public bool ServerIsEnabled
        {
            get => ServerIsEnabled1;
            set
            {
                ServerIsEnabled1 = value;
                OnPropertyChanged();
            }
        }
        public int SendingPeriodInSleepMode
        {
            get => _sendingPeriodInSleepMode;
            set
            {
                _sendingPeriodInSleepMode = value;
                OnPropertyChanged();
            }
        }
        public bool ServerIsEnabled1
        {
            get => _serverIsEnabled;
            set
            {
                _serverIsEnabled = value;
                OnPropertyChanged();
            }
        }

        public int ProtocolTypeIndex
        {
            get => _protocolTypeIndex; 
            set
            {
                _protocolTypeIndex = value;
                OnPropertyChanged();
            }
        }

        public int PeriodOfPingShortMessageIndex
        {
            get => _periodOfPingShortMessageIndex;
            set
            {
                _periodOfPingShortMessageIndex = value;
                OnPropertyChanged();
            }
        }

        /**/
        /* validation */
        /**/
        Regex regexDns = new Regex("(https?:\\/\\/(?:www\\.|(?!www))[a-zA-Z0-9][a-zA-Z0-9-]+[a-zA-Z0-9]\\.[^\\s]{2,}|www\\.[a-zA-Z0-9][a-zA-Z0-9-]+[a-zA-Z0-9]\\.[^\\s]{2,}|https?:\\/\\/(?:www\\.|(?!www))[a-zA-Z0-9]+\\.[^\\s]{2,}|www\\.[a-zA-Z0-9]+\\.[^\\s]{2,})");
        Regex regexPort = new Regex("^([0-9]|[1-8][0-9]|9[0-9]|[1-8][0-9]{2}|9[0-8][0-9]|99[0-9]|[1-8][0-9]{3}|9[0-8][0-9]{2}|99[0-8][0-9]|999[0-9]|[1-5][0-9]{4}|6[0-4][0-9]{3}|65[0-4][0-9]{2}|655[0-2][0-9]|6553[0-5])$");

        public string this[string columnName]
        {
            get
            {
                string resultMessage = "";
                if (columnName == nameof(IpDnsAddress))
                {
                    _ipDnsAddressIsValid = regexDns.IsMatch(this.IpDnsAddress);
                    if (!_ipDnsAddressIsValid)
                    {
                        resultMessage = "Value is not valid!";
                    }
                }

                if (columnName == nameof(Port))
                {
                    _portIsValid = regexPort.IsMatch(this.Port);
                    if (!_portIsValid)
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
            _settingsIsChangedCallbackNotify.Invoke(_ipDnsAddressIsValid && _portIsValid);
        }

        public void Dispose() { }
        public string Error => string.Empty;

        public bool SettingsIsValid
        {
            get => _settingsIsValid; set
            {
                _settingsIsValid = value;
                OnPropertyChanged();
            }
        }
    }
}
