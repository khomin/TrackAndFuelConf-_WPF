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
        private int _operatorListIndex = 0;
        enum OperatorType
        {
            MTS = 0,
            Megafon = 1,
            Beeline = 2,
            Custom = 3
        };

        private string _apn = "gdata";
        private string _apnLogin = "login";
        private string _apnPassword = "default";
        private string _apnPinCode = "";
        private bool _apnIsEditable = false;
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
        private bool _protocolMsgTypeLatitude = false;
        private bool _protocolMsgTypeLongitude = false;
        private bool _protocolMsgTypeFix = false;
        private bool _protocolMsgTypeAltitude = false;
        private bool _protocolMsgTypeHeading = false;
        private bool _protocolMsgTypeSpeed = false;
        private bool _protocolMsgTypeHdop = false;
        private bool _protocolMsgTypeSatsCount = false;
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

        private int _maxDistanceBetweenTwoPoints = 0;
        private int _maxDistanceAngeOfRotationBetweenTwoPoints = 0;
        private int _accelerationThresholdDetermMotion = 0;
        private int _minSpeedForDetectionMotion = 0;
        private int _maxSpeedForDetectionParking = 0;

        private bool _enableSmsSubscribing_0 = false;
        private bool _enableSmsSubscribing_1 = false;
        private bool _allowSmsControl_0 = false;
        private bool _allowSmsControl_1 = false;
        private readonly ObservableCollection<String> _sensorActivation_0_phone_0;
        private int _sensorActivation_0_phone_0_index = 0;
        private readonly ObservableCollection<String> _sensorActivation_0_phone_1;
        private int _sensorActivation_0_phone_1_index = 0;
        private readonly ObservableCollection<String> _sensorActivation_1_phone_0;
        private int _sensorActivation_1_phone_0_index = 0;
        private readonly ObservableCollection<String> _sensorActivation_1_phone_1;
        private int _sensorActivation_1_phone_1_index = 0;
        private readonly ObservableCollection<String> _temperatureDecreasePhone_0;
        private int _temperatureDecreasePhone_0_index = 0;
        private readonly ObservableCollection<String> _temperatureDecreasePhone_1;
        private int _temperatureDecreasePhone_1_index = 0;
        private readonly ObservableCollection<String> _temperatureIncreasePhone_0;
        private int _temperatureIncreasePhone_0_index = 0;
        private readonly ObservableCollection<String> _temperatureIncreasePhone_1;
        private int _temperatureIncreasePhone_1_index = 0;
        private readonly ObservableCollection<String> _temperatureRecoveryPhone_0;
        private int _temperatureRecoveryPhone_0_index = 0;
        private readonly ObservableCollection<String> _temperatureRecoveryPhone_1;
        private int _temperatureRecoveryPhone_1_index = 0;
        public enum SubscribitionEvent
        {
            Off = 0,
            SentSms = 1,
        };

        private readonly ObservableCollection<OneWireItemModel> _oneWireSettingsModelList;
        private readonly ObservableCollection<InputItemSettingsModel> _inputsSettingsModelList;

        public SettingsViewModel()
        {
            _operatorsList = new ObservableCollection<string>();

            var operators = System.Enum.GetNames(typeof(OperatorType));
            foreach (var op in operators)
            {
                _operatorsList.Add(op);
            }
            OperatorListIndex = 0;

            _serversConnection = new ObservableCollection<string>();
            _serversConnection.Add("Primary");
            _serversConnection.Add("Reserv");

            _serversConnectionModel = new ObservableCollection<SettingsConnectionViewModel>();
            _serversConnectionModel.Add(new SettingsConnectionViewModel());
            _serversConnectionModel.Add(new SettingsConnectionViewModel());
            _serversConnectionModel[0].IpDnsAddress = "лкщюыё1.ру";
            _serversConnectionModel[1].IpDnsAddress = "лкщюыё2.ру";

            _transmitProtocol = new ObservableCollection<string>();
            _transmitProtocol.Add("Wialon");

            TypeOfIgnitionDetection = new ObservableCollection<string>();
            TypeOfIgnitionDetection.Add("Disable");
            TypeOfIgnitionDetection.Add("Virtual (by power voltage");
            TypeOfIgnitionDetection.Add("IN3");

            _oneWireSettingsModelList = new ObservableCollection<OneWireItemModel>();
            _oneWireSettingsModelList.Add(new OneWireItemModel());
            _oneWireSettingsModelList.Add(new OneWireItemModel());
            _oneWireSettingsModelList.Add(new OneWireItemModel());
            _oneWireSettingsModelList.Add(new OneWireItemModel());
            _oneWireSettingsModelList[0].HexCode = "AABBCC1";
            _oneWireSettingsModelList[1].HexCode = "AABBCC2";
            _oneWireSettingsModelList[2].HexCode = "AABBCC3";
            _oneWireSettingsModelList[3].HexCode = "AABBCC4";
            _oneWireSettingsModelList[0].SensorName = "Temp1";
            _oneWireSettingsModelList[1].SensorName = "Temp2";
            _oneWireSettingsModelList[2].SensorName = "Temp3";
            _oneWireSettingsModelList[3].SensorName = "Temp4";

            _inputsSettingsModelList = new ObservableCollection<InputItemSettingsModel>();
            _inputsSettingsModelList.Add(new InputItemSettingsModel("Line IN1", false));
            _inputsSettingsModelList.Add(new InputItemSettingsModel("Line IN2", false));
            _inputsSettingsModelList.Add(new InputItemSettingsModel("Line IN3", true));

            _sensorActivation_0_phone_0 = new ObservableCollection<string>();
            _sensorActivation_0_phone_1 = new ObservableCollection<string>();
            _sensorActivation_1_phone_0 = new ObservableCollection<string>();
            _sensorActivation_1_phone_1 = new ObservableCollection<string>();
            _temperatureDecreasePhone_0 = new ObservableCollection<string>();
            _temperatureDecreasePhone_1 = new ObservableCollection<string>();
            _temperatureIncreasePhone_0 = new ObservableCollection<string>();
            _temperatureIncreasePhone_1 = new ObservableCollection<string>();
            _temperatureRecoveryPhone_0 = new ObservableCollection<string>();
            _temperatureRecoveryPhone_1 = new ObservableCollection<string>();
            _sensorActivation_0_phone_0.Add("Off");
            _sensorActivation_0_phone_0.Add("Sent SMS");
            
            _sensorActivation_0_phone_1.Add("Off");
            _sensorActivation_0_phone_1.Add("Sent SMS");
            
            _sensorActivation_1_phone_0.Add("Off");
            _sensorActivation_1_phone_0.Add("Sent SMS");

            _sensorActivation_1_phone_1.Add("Off");
            _sensorActivation_1_phone_1.Add("Sent SMS");

            _temperatureDecreasePhone_0.Add("Off");
            _temperatureDecreasePhone_0.Add("Sent SMS");

            _temperatureDecreasePhone_1.Add("Off");
            _temperatureDecreasePhone_1.Add("Sent SMS");

            _temperatureIncreasePhone_0.Add("Off");
            _temperatureIncreasePhone_0.Add("Sent SMS");

            _temperatureIncreasePhone_1.Add("Off");
            _temperatureIncreasePhone_1.Add("Sent SMS");

            _temperatureRecoveryPhone_0.Add("Off");
            _temperatureRecoveryPhone_0.Add("Sent SMS");

            _temperatureRecoveryPhone_1.Add("Off");
            _temperatureRecoveryPhone_1.Add("Sent SMS");
        }

        public ObservableCollection<string> OperatorsList { get => _operatorsList; }

        public int OperatorListIndex
        {
            get => _operatorListIndex;
            set
            {
                _operatorListIndex = value;
                OperatorType op = (OperatorType)value;
                switch (op)
                {
                    case OperatorType.MTS:
                        Apn = "internet.mts.ru";
                        ApnLogin = "mts";
                        ApnPassword = "mts";
                        ApnIsEditable = false;
                        break;
                    case OperatorType.Megafon:
                        Apn = "internet.beeline.ru";
                        ApnLogin = "beeline";
                        ApnPassword = "beeline";
                        ApnIsEditable = false;
                        break;
                    case OperatorType.Beeline:
                        Apn = "megafon";
                        ApnLogin = "gdata";
                        ApnPassword = "gdata";
                        ApnIsEditable = false;
                        break;
                    case OperatorType.Custom:
                        Apn = "";
                        ApnLogin = "";
                        ApnPassword = "";
                        ApnIsEditable = true;
                        break;
                }
                OnPropertyChanged();
            }
        }

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

        public bool ApnIsEditable
        {
            get => _apnIsEditable;
            set
            {
                _apnIsEditable = value;
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
            get => _protocolMsgTypeLatitude;
            set
            {
                _protocolMsgTypeLatitude = value;
                OnPropertyChanged();
            }
        }
        public bool ProtocolMsgTypeLongitude1
        {
            get => _protocolMsgTypeLongitude;
            set
            {
                _protocolMsgTypeLongitude = value;
                OnPropertyChanged();
            }
        }
        public bool ProtocolMsgTypeFix1
        {
            get => _protocolMsgTypeFix;
            set
            {
                _protocolMsgTypeFix = value;
                OnPropertyChanged();
            }
        }
        public bool ProtocolMsgTypeAltitude1
        {
            get => _protocolMsgTypeAltitude;
            set
            {
                _protocolMsgTypeAltitude = value;
                OnPropertyChanged();
            }
        }
        public bool ProtocolMsgTypeHeading1
        {
            get => _protocolMsgTypeHeading;
            set
            {
                _protocolMsgTypeHeading = value;
                OnPropertyChanged();
            }
        }
        public bool ProtocolMsgTypeSpeed1
        {
            get => _protocolMsgTypeSpeed;
            set
            {
                _protocolMsgTypeSpeed = value;
                OnPropertyChanged();
            }
        }
        public bool ProtocolMsgTypeHdop1
        {
            get => _protocolMsgTypeHdop;
            set
            {
                _protocolMsgTypeHdop = value;
                OnPropertyChanged();
            }
        }
        public bool ProtocolMsgTypeSatsCount1
        {
            get => _protocolMsgTypeSatsCount;
            set
            {
                _protocolMsgTypeSatsCount = value;
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
                _typeOfIgnitionDetectionIndex = (IgnitionDetectionType)value;
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

        public int MaxDistanceBetweenTwoPoints
        {
            get => _maxDistanceBetweenTwoPoints; set
            {
                _maxDistanceBetweenTwoPoints = value;
                OnPropertyChanged();
            }
        }
        public int MaxDistanceAngeOfRotationBetweenTwoPoints
        {
            get => _maxDistanceAngeOfRotationBetweenTwoPoints;
            set
            {
                _maxDistanceAngeOfRotationBetweenTwoPoints = value;
                OnPropertyChanged();
            }
        }
        public int AccelerationThresholdDetermMotion
        {
            get => _accelerationThresholdDetermMotion;
            set
            {
                _accelerationThresholdDetermMotion = value;
                OnPropertyChanged();
            }
        }
        public int MinSpeedForDetectionMotion
        {
            get => _minSpeedForDetectionMotion;
            set
            {
                _minSpeedForDetectionMotion = value;
                OnPropertyChanged();
            }
        }
        public int MaxSpeedForDetectionParking
        {
            get => _maxSpeedForDetectionParking;
            set
            {
                _maxSpeedForDetectionParking = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<OneWireItemModel> OneWireSettingsModelList => _oneWireSettingsModelList;

        public ObservableCollection<InputItemSettingsModel> InputsSettingsModelList => _inputsSettingsModelList;

        public bool EnableSmsSubscribing_0
        {
            get => _enableSmsSubscribing_0;
            set
            {
                _enableSmsSubscribing_0 = value;
                OnPropertyChanged();
            }
        }
        public bool EnableSmsSubscribing_1
        {
            get => _enableSmsSubscribing_1;
            set
            {
                _enableSmsSubscribing_1 = value;
                OnPropertyChanged();
            }
        }
        public bool AllowSmsControl_0
        {
            get => _allowSmsControl_0;
            set
            {
                _allowSmsControl_0 = value;
                OnPropertyChanged();
            }
        }
        public bool AllowSmsControl_1
        {
            get => _allowSmsControl_1;
            set
            {
                _allowSmsControl_1 = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<string> SensorActivation_0_phone_0 => _sensorActivation_0_phone_0;

        public int SensorActivation_0_phone_0_index
        {
            get => _sensorActivation_0_phone_0_index;
            set
            {
                _sensorActivation_0_phone_0_index = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<string> SensorActivation_0_phone_1 => _sensorActivation_0_phone_1;

        public int SensorActivation_0_phone_1_index
        {
            get => _sensorActivation_0_phone_1_index;
            set
            {
                _sensorActivation_0_phone_1_index = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<string> SensorActivation_1_phone_0 => _sensorActivation_1_phone_0;

        public int SensorActivation_1_phone_0_index
        {
            get => _sensorActivation_1_phone_0_index;
            set
            {
                _sensorActivation_1_phone_0_index = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<string> SensorActivation_1_phone_1 => _sensorActivation_1_phone_1;

        public int SensorActivation_1_phone_1_index
        {
            get => _sensorActivation_1_phone_1_index;
            set
            {
                _sensorActivation_1_phone_1_index = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<string> TemperatureDecreasePhone_0 => _temperatureDecreasePhone_0;

        public int TemperatureDecreasePhone_0_index
        {
            get => _temperatureDecreasePhone_0_index;
            set
            {
                _temperatureDecreasePhone_0_index = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<string> TemperatureDecreasePhone_1 => _temperatureDecreasePhone_1;

        public int TemperatureDecreasePhone_1_index
        {
            get => _temperatureDecreasePhone_1_index;
            set
            {
                _temperatureDecreasePhone_1_index = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<string> TemperatureIncreasePhone_0 => _temperatureIncreasePhone_0;

        public int TemperatureIncreasePhone_0_index
        {
            get => _temperatureIncreasePhone_0_index;
            set
            {
                _temperatureIncreasePhone_0_index = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<string> TemperatureIncreasePhone_1 => _temperatureIncreasePhone_1;

        public int TemperatureIncreasePhone_1_index
        {
            get => _temperatureIncreasePhone_1_index;
            set
            {
                _temperatureIncreasePhone_1_index = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<string> TemperatureRecoveryPhone_0 => _temperatureRecoveryPhone_0;

        public int TemperatureRecoveryPhone_0_index
        {
            get => _temperatureRecoveryPhone_0_index;
            set
            {
                _temperatureRecoveryPhone_0_index = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<string> TemperatureRecoveryPhone_1 => _temperatureRecoveryPhone_1;

        public int TemperatureRecoveryPhone_1_index
        {
            get => _temperatureRecoveryPhone_1_index;
            set
            {
                _temperatureRecoveryPhone_1_index = value;
                OnPropertyChanged();
            }
        }
    }
}
