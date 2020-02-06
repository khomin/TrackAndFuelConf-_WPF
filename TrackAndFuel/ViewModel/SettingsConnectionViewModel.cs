﻿using MetroDemo.Core;
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
        private string _ipDnsAddress = "lkshuuuyaayooo.ru";
        private int _port = 1000;
        
        private ObservableCollection<string> _protocolType; // combox
        private int _protocolTypeIndex = 0;

        private ObservableCollection<string>  _periodOfPingShortMessage; // combox
        private int _periodOfPingShortMessageIndex = 0;

        private int _delayBeforeNextAttempConnecting = 30; // slider
        private int _sendingPeriodDuringParking = 30; // slider
        private int _sendingPeriodInSleepMode = 30; // slider
        private bool _serverIsEnabled = false;

        public SettingsConnectionViewModel() 
        {
            _protocolType = new ObservableCollection<string>();
            _protocolType.Add("Wialon");

            _periodOfPingShortMessage = new ObservableCollection<string>();
            _periodOfPingShortMessage.Add("15 sec");
            _periodOfPingShortMessage.Add("60 sec");
            _periodOfPingShortMessage.Add("3 min");
        }

        public string IpDnsAddress
        {
            get => _ipDnsAddress; 
            set
            {
                _ipDnsAddress = value;
                OnPropertyChanged();
            }
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

        public int Port
        {
            get => _port; 
            set
            {
                _port = value;
                OnPropertyChanged();
            }
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
        Regex regexApn = new Regex("^[a-z0-9!#$%&'()*+,.\\/:;<=>?@\\[\\] ^_`{|}~-]{1,32}$");

        public string this[string columnName]
        {
            get
            {
                if (columnName == nameof(IpDnsAddress))
                {
                    if (!regexApn.IsMatch(this.IpDnsAddress))
                    {
                        return "Value is not valid!";
                    }
                }

                if (columnName == nameof(Port))
                {
                    if (this.Port == 0)
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
