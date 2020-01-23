using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace trackerWpfConf.ViewModel
{
    public class SettingsViewModel : ViewModelBase
    {
        private readonly ObservableCollection<string> _operatorsList;
        private string _apn = "gdata";
        private string _apnLogin = "login";
        private string _apnPassword = "default";
        private string _apnPinCode = "";
        private ObservableCollection<string> _serversConnection;
        private int _serverConnectionListIndex = 0;
        private ObservableCollection<SettingsConnectionViewModel> _serversConnectionModel;

        private ObservableCollection<string> _transmitProtocol;  // TODO: it now wialon only
        private int _transmitProtocolIndex = 0;

        private bool _protocolMsgTypeEventType = false;
        private bool _protocolMsgTypeOdometer = false;
        private bool _protocolMsgTypeGsmStreng = false;
        private bool _protocolMsgTypeTemperature = false;
        private bool _protocolMsgTypeEngHours = false;
        private bool _protocolMsgTypeIgnState = false;
        private bool ProtocolMsgTypeLatitude = false;
        private bool ProtocolMsgTypeLongitude = false;
        private bool ProtocolMsgTypeFix = false;
        private bool ProtocolMsgTypeAltitude = false;
        private bool ProtocolMsgTypeHeading = false;
        private bool ProtocolMsgTypeSpeed = false;
        private bool ProtocolMsgTypeHdop = false;
        private bool ProtocolMsgTypeSatsCount = false;
        private bool _protocolMsgTypePowerInternal = false;
        private bool _protocolMsgTypePowerExternal = false;
        private bool _protocolMsgTypeDigitalOuts = false;
        private bool _protocolMsgTypeInputs = false;
        private bool _protocolMsgTypeLlsInternal = false;
        private bool _protocolMsgTypeTemperature1Wire = false;

        private ObservableCollection<string> _typeOfIgnitionDetection;
        private IgnitionDetectionType _typeOfIgnitionDetectionIndex = 0;
        public enum IgnitionDetectionType
        {
            Off = 0,
            AutoByPower = 1,
            Input3 = 2
        };

        private float _manualIgnitionDetectionLowValue = 0;
        private float _manualIgnitionDetectionHightValue = 0;

        public SettingsViewModel()
        {
            _operatorsList = new ObservableCollection<string>();
            _operatorsList.Add("MTS");
            _operatorsList.Add("Megafon");
            _operatorsList.Add("Beeline");
            _operatorsList.Add("Custom");

            _serversConnection = new ObservableCollection<string>();
            _serversConnection.Add("Primary");
            _serversConnection.Add("Reserv");

            _serversConnectionModel = new ObservableCollection<SettingsConnectionViewModel>();
            _serversConnectionModel.Add(new SettingsConnectionViewModel());
            _serversConnectionModel.Add(new SettingsConnectionViewModel());
            _serversConnectionModel[0].IpDnsAddress = "ya.ru1";
            _serversConnectionModel[1].IpDnsAddress = "ya.ru2";

            _transmitProtocol = new ObservableCollection<string>();
            _transmitProtocol.Add("Wialon");

            TypeOfIgnitionDetection = new ObservableCollection<string>();
            TypeOfIgnitionDetection.Add("Disable");
            TypeOfIgnitionDetection.Add("Virtual (by power voltage");
            TypeOfIgnitionDetection.Add("IN3");
        }

        public ObservableCollection<string> OperatorsList { get => _operatorsList; }

        public ObservableCollection<string> ServerConnectionList { get => _serversConnection; }
        public string ApnLogin
        {
            get => _apnLogin;
            set
            {
                _apnLogin = value;
                OnPropertyChanged();
            }
        }

        public string ApnPassword
        {
            get => _apnPassword;
            set
            {
                _apnPassword = value;
                OnPropertyChanged();
            }
        }
        public string ApnPinCode
        {
            get => _apnPinCode;
            set
            {
                _apnPinCode = value;
                OnPropertyChanged();
            }
        }

        public string Apn
        {
            get => _apn;
            set
            {
                _apn = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<SettingsConnectionViewModel> ServersConnectionModel { get => _serversConnectionModel; }
        public int ServerConnectionListIndex
        {
            get => _serverConnectionListIndex;
            set
            {
                _serverConnectionListIndex = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<string> TransmitProtocol
        {
            get => _transmitProtocol;
            set
            {
                _transmitProtocol = value;
                OnPropertyChanged();
            }
        }
        public int TransmitProtocolIndex
        {
            get => _transmitProtocolIndex;
            set
            {
                _transmitProtocolIndex = value;
                OnPropertyChanged();
            }
        }
        public bool ProtocolMsgTypeEventType
        {
            get => _protocolMsgTypeEventType;
            set
            {
                _protocolMsgTypeEventType = value;
                OnPropertyChanged();
            }
        }
        public bool ProtocolMsgTypeOdometer
        {
            get => _protocolMsgTypeOdometer;
            set
            {
                _protocolMsgTypeOdometer = value;
                OnPropertyChanged();
            }
        }
        public bool ProtocolMsgTypeGsmStreng
        {
            get => _protocolMsgTypeGsmStreng;
            set
            {
                _protocolMsgTypeGsmStreng = value;
                OnPropertyChanged();
            }
        }
        public bool ProtocolMsgTypeTemperature
        {
            get => _protocolMsgTypeTemperature;
            set
            {
                _protocolMsgTypeTemperature = value;
                OnPropertyChanged();
            }
        }
        public bool ProtocolMsgTypeEngHours
        {
            get => _protocolMsgTypeEngHours;
            set
            {
                _protocolMsgTypeEngHours = value;
                OnPropertyChanged();
            }
        }
        public bool ProtocolMsgTypeIgnState
        {
            get => _protocolMsgTypeIgnState;
            set
            {
                _protocolMsgTypeIgnState = value;
                OnPropertyChanged();
            }
        }
        public bool ProtocolMsgTypeLatitude1
        {
            get => ProtocolMsgTypeLatitude;
            set
            {
                ProtocolMsgTypeLatitude = value;
                OnPropertyChanged();
            }
        }
        public bool ProtocolMsgTypeLongitude1
        {
            get => ProtocolMsgTypeLongitude;
            set
            {
                ProtocolMsgTypeLongitude = value;
                OnPropertyChanged();
            }
        }
        public bool ProtocolMsgTypeFix1
        {
            get => ProtocolMsgTypeFix;
            set
            {
                ProtocolMsgTypeFix = value;
                OnPropertyChanged();
            }
        }
        public bool ProtocolMsgTypeAltitude1
        {
            get => ProtocolMsgTypeAltitude;
            set
            {
                ProtocolMsgTypeAltitude = value;
                OnPropertyChanged();
            }
        }
        public bool ProtocolMsgTypeHeading1
        {
            get => ProtocolMsgTypeHeading;
            set
            {
                ProtocolMsgTypeHeading = value;
                OnPropertyChanged();
            }
        }
        public bool ProtocolMsgTypeSpeed1
        {
            get => ProtocolMsgTypeSpeed;
            set
            {
                ProtocolMsgTypeSpeed = value;
                OnPropertyChanged();
            }
        }
        public bool ProtocolMsgTypeHdop1
        {
            get => ProtocolMsgTypeHdop;
            set
            {
                ProtocolMsgTypeHdop = value;
                OnPropertyChanged();
            }
        }
        public bool ProtocolMsgTypeSatsCount1
        {
            get => ProtocolMsgTypeSatsCount;
            set
            {
                ProtocolMsgTypeSatsCount = value;
                OnPropertyChanged();
            }
        }
        public bool ProtocolMsgTypePowerInternal
        {
            get => _protocolMsgTypePowerInternal;
            set
            {
                _protocolMsgTypePowerInternal = value;
                OnPropertyChanged();
            }
        }
        public bool ProtocolMsgTypePowerExternal
        {
            get => _protocolMsgTypePowerExternal;
            set
            {
                _protocolMsgTypePowerExternal = value;
                OnPropertyChanged();
            }
        }
        public bool ProtocolMsgTypeDigitalOuts
        {
            get => _protocolMsgTypeDigitalOuts;
            set
            {
                _protocolMsgTypeDigitalOuts = value;
                OnPropertyChanged();
            }
        }
        public bool ProtocolMsgTypeInputs
        {
            get => _protocolMsgTypeInputs;
            set
            {
                _protocolMsgTypeInputs = value;
                OnPropertyChanged();
            }
        }
        public bool ProtocolMsgTypeLlsInternal
        {
            get => _protocolMsgTypeLlsInternal;
            set
            {
                _protocolMsgTypeLlsInternal = value;
                OnPropertyChanged();
            }
        }
        public bool ProtocolMsgTypeTemperature1Wire
        {
            get => _protocolMsgTypeTemperature1Wire; set
            {
                _protocolMsgTypeTemperature1Wire = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<string> TypeOfIgnitionDetection
        {
            get => _typeOfIgnitionDetection;
            set
            {
                _typeOfIgnitionDetection = value;
                OnPropertyChanged();
            }
        }
        public int TypeOfIgnitionDetectionIndex
        { 
            get => (int)_typeOfIgnitionDetectionIndex;
            set
            {
                _typeOfIgnitionDetectionIndex = (IgnitionDetectionType)value ;
                OnPropertyChanged();
            }
        }

        public float ManualIgnitionDetectionLowValue
        {
            get => _manualIgnitionDetectionLowValue; 
            set
            {
                _manualIgnitionDetectionLowValue = value;
                OnPropertyChanged();
            }
        }
        public float ManualIgnitionDetectionHightValue
        {
            get => _manualIgnitionDetectionHightValue; 
            set
            {
                _manualIgnitionDetectionHightValue = value;
                OnPropertyChanged();
            }
        }
    }
}
