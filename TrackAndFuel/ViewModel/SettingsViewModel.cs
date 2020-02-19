using MetroDemo.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TrackAndFuel.ViewModel
{
    public class SettingsViewModel : ViewModelBase, IDataErrorInfo, IDisposable
    {
        private bool _settingsIsValid = false;
        private readonly ObservableCollection<string> _operatorsList;
        private int _operatorListIndex = 0;
        public enum OperatorType
        {
            MTS = 0,
            Megafon = 1,
            Beeline = 2,
            Tele2 = 3,
            Custom = 4
        };

        private string _apn = "gdata";
        private bool _apnIsValid = false;

        private string _apnLogin = "login";
        private bool _apnLoginIsValid = false;

        private string _apnPassword = "default";
        private bool _apnPasswordIsValid = false;

        private string _apnPinCode = "";
        private bool _apnPinCodeIsValid = false;

        private bool _apnIsEditable = false;
        private readonly ObservableCollection<string> _serversConnection;
        private int _serverConnectionListIndex = 0;
        private readonly SettingsConnectionViewModel _serversConnectionModel;
        private bool _serverConnectionIsValid = false;

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
            Input = 2
        };

        private float _manualIgnitionDetectionLowValue = 0;
        private float _manualIgnitionDetectionHightValue = 0;

        private int _maxDistanceBetweenTwoPoints = 0;
        private int _maxDistanceAngeOfRotationBetweenTwoPoints = 0;
        private int _accelerationThresholdDetermMotion = 0;
        private int _minSpeedForDetectionMotion = 0;
        private int _maxSpeedForDetectionParking = 0;
        private int _maxStendingTime = 0;
        private int _maxHeading = 0;

        private bool _enableSmsSubscribing_0 = false;
        private bool _enableSmsSubscribing_1 = false;
        private bool _allowSmsControl_0 = false;
        private bool _allowSmsControl_1 = false;

        private string _phoneNumber1 = "79032664165";
        private bool _phoneNumber1IsValid = false;

        private string _phoneNumber2 = "79032664165";
        private bool _phoneNumber2IsValid = false;

        private readonly ObservableCollection<String> _sensorActivation_0_phone_0;
        private int _sensorActivation_0_phone_0_index = 0;
        private readonly ObservableCollection<String> _sensorActivation_0_phone_1;
        private int _sensorActivation_0_phone_1_index = 0;
        private readonly ObservableCollection<String> _sensorActivation_1_phone_0;
        private int _sensorActivation_1_phone_0_index = 0;
        private readonly ObservableCollection<String> _sensorActivation_1_phone_1;
        private int _sensorActivation_1_phone_1_index = 0;
        private readonly ObservableCollection<String> _sensorActivation_2_phone_0;
        private int _sensorActivation_2_phone_0_index = 0;
        private readonly ObservableCollection<String> _sensorActivation_2_phone_1;
        private int _sensorActivation_2_phone_1_index = 0;
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

        private readonly ObservableCollection<String> _lls1DrainPhone_0;
        private int _lls1DrainPhone_0_index = 0;
        private readonly ObservableCollection<String> _lls1DrainPhone_1;
        private int _lls1DrainPhone_1_index = 0;
        private readonly ObservableCollection<String> _lls1FillUpPhone_0;
        private int _lls1FillUpPhone_0_index = 0;
        private readonly ObservableCollection<String> _lls1FillUpPhone_1;
        private int _lls1FillUpPhone_1_index = 0;

        public enum SubscribitionEvent
        {
            Off = 0,
            SentSms = 1,
        };

        private readonly ObservableCollection<OneWireItemModel> _oneWireSettingsModelList;
        private bool _oneWireSettingsIsValid0;
        private bool _oneWireSettingsIsValid1;
        private bool _oneWireSettingsIsValid2;
        private bool _oneWireSettingsIsValid3;

        private readonly ObservableCollection<InputItemSettingsModel> _inputsSettingsModelList;
        public class IgnitionPinDetectIsAvailable
        {
            public bool IgnitionPin1DetectIsUse = false;
            public bool IgnitionPin2DetectIsUse = false;
            public bool IgnitionPin3DetectIsUse = false;
        }
        private bool _inputSettingsIsValid0;
        private bool _inputSettingsIsValid1;
        private bool _inputSettingsIsValid2;

        private readonly ObservableCollection<LlsDataViewModel> _llsDataViewModelList;
        private bool _llsSettings1_IsValid;
        private bool _llsSettings2_IsValid;
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


            _serversConnectionModel = new SettingsConnectionViewModel((isValid) =>
            {
                _serverConnectionIsValid = isValid;
                ValidateSettings();
            });
            /* Those default - true, 
             * because the second change after focuse on */
            _serverConnectionIsValid = true;

            _transmitProtocol = new ObservableCollection<string>();
            _transmitProtocol.Add("Wialon");

            TypeOfIgnitionDetection = new ObservableCollection<string>();
            TypeOfIgnitionDetection.Add("Disable");
            TypeOfIgnitionDetection.Add("Virtual (by power voltage");
            TypeOfIgnitionDetection.Add("Discret input");

            _oneWireSettingsModelList = new ObservableCollection<OneWireItemModel>();
            _oneWireSettingsModelList.Add(new OneWireItemModel((IsValid) =>
            {
                _oneWireSettingsIsValid0 = IsValid;
                ValidateSettings();
            }));
            _oneWireSettingsModelList.Add(new OneWireItemModel((IsValid) =>
            {
                _oneWireSettingsIsValid1 = IsValid;
                ValidateSettings();
            }));
            _oneWireSettingsModelList.Add(new OneWireItemModel((IsValid) =>
            {
                _oneWireSettingsIsValid2 = IsValid;
                ValidateSettings();
            })); ;
            _oneWireSettingsModelList.Add(new OneWireItemModel((IsValid) =>
            {
                _oneWireSettingsIsValid3 = IsValid;
                ValidateSettings();
            }));
            /* Those default - true, 
             * because the second change after focuse on */
            _oneWireSettingsIsValid0 = true;
            _oneWireSettingsIsValid1 = true;
            _oneWireSettingsIsValid2 = true;
            _oneWireSettingsIsValid3 = true;

            _oneWireSettingsModelList[0].HexCode = "AABBCC1000000000";
            _oneWireSettingsModelList[1].HexCode = "AABBCC1000000001";
            _oneWireSettingsModelList[2].HexCode = "AABBCC1000000002";
            _oneWireSettingsModelList[3].HexCode = "AABBCC1000000003";
            _oneWireSettingsModelList[0].SensorName = "Temp1";
            _oneWireSettingsModelList[1].SensorName = "Temp2";
            _oneWireSettingsModelList[2].SensorName = "Temp3";
            _oneWireSettingsModelList[3].SensorName = "Temp4";

            _inputsSettingsModelList = new ObservableCollection<InputItemSettingsModel>();
            _inputsSettingsModelList.Add(new InputItemSettingsModel("Line IN1", "LineIN1", (isUsedAsIgntition) =>
            {
                if (isUsedAsIgntition)
                {
                    _inputsSettingsModelList[1].UsePinAsIgnitionDetection = false;
                    _inputsSettingsModelList[2].UsePinAsIgnitionDetection = false;
                }
            }, (isValid) =>
            {
                _inputSettingsIsValid0 = isValid;
                ValidateSettings();
            }));
            _inputsSettingsModelList.Add(new InputItemSettingsModel("Line IN2", "LineIN2", (isUsedAsIgntition) =>
            {
                if (isUsedAsIgntition)
                {
                    _inputsSettingsModelList[0].UsePinAsIgnitionDetection = false;
                    _inputsSettingsModelList[2].UsePinAsIgnitionDetection = false;
                }
            }, (isValid) =>
            {
                _inputSettingsIsValid1 = isValid;
                ValidateSettings();
            }));
            _inputsSettingsModelList.Add(new InputItemSettingsModel("Line IN3", "LineIN3", (isUsedAsIgntition) =>
            {
                if (isUsedAsIgntition)
                {
                    _inputsSettingsModelList[0].UsePinAsIgnitionDetection = false;
                    _inputsSettingsModelList[1].UsePinAsIgnitionDetection = false;
                }
            }, (isValid) =>
            {
                _inputSettingsIsValid2 = isValid;
                ValidateSettings();
            }));
            /* Those default - true, 
             * because the second change after focuse on */
            _inputSettingsIsValid0 = true;
            _inputSettingsIsValid1 = true;
            _inputSettingsIsValid2 = true;

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
            _sensorActivation_2_phone_0 = new ObservableCollection<string>();
            _sensorActivation_2_phone_1 = new ObservableCollection<string>();

            _sensorActivation_0_phone_0.Add("Off");
            _sensorActivation_0_phone_0.Add("Sent SMS");

            _sensorActivation_0_phone_1.Add("Off");
            _sensorActivation_0_phone_1.Add("Sent SMS");

            _sensorActivation_1_phone_0.Add("Off");
            _sensorActivation_1_phone_0.Add("Sent SMS");

            _sensorActivation_1_phone_1.Add("Off");
            _sensorActivation_1_phone_1.Add("Sent SMS");

            _sensorActivation_2_phone_1.Add("Off");
            _sensorActivation_2_phone_1.Add("Sent SMS");

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

            _llsDataViewModelList = new ObservableCollection<LlsDataViewModel>();
            _llsDataViewModelList.Add(new LlsDataViewModel((isValid) =>
            {
                _llsSettings1_IsValid = isValid;
                ValidateSettings();
            }));
            _llsDataViewModelList.Add(new LlsDataViewModel((isValid) =>
            {
                _llsSettings2_IsValid = isValid;
                ValidateSettings();
            }));
            /* Those default - true, 
            * because the second change after focuse on */
            _llsSettings1_IsValid = true;
            _llsSettings2_IsValid = true;
        }

        public ObservableCollection<string> OperatorsList { get => _operatorsList; }

        public int OperatorListIndex
        {
            get => _operatorListIndex;
            set
            {
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
                    case OperatorType.Tele2:
                        Apn = "internet.tele2.ru";
                        ApnLogin = "";
                        ApnPassword = "";
                        ApnIsEditable = false;
                        break;
                    case OperatorType.Custom:
                        //Apn = "";
                        //ApnLogin = "";
                        //ApnPassword = "";
                        ApnIsEditable = true;
                        break;
                }
                ValidateSettings();
                this.Set(ref this._operatorListIndex, value);
            }
        }

        public ObservableCollection<string> ServerConnectionList { get => _serversConnection; }
        public string ApnLogin
        {
            get => _apnLogin;
            set
            {
                this.Set(ref this._apnLogin, value);
            }
        }

        public string ApnPassword
        {
            get => _apnPassword;
            set
            {
                this.Set(ref this._apnPassword, value);
            }
        }
        public string ApnPinCode
        {
            get => _apnPinCode;
            set
            {
                this.Set(ref this._apnPinCode, value);
            }
        }

        public string Apn
        {
            get => _apn;
            set
            {
                this.Set(ref this._apn, value);
            }
        }

        public bool ApnIsEditable
        {
            get => _apnIsEditable;
            set
            {
                this.Set(ref this._apnIsEditable, value);
            }
        }
        public int ServerConnectionListIndex
        {
            get => _serverConnectionListIndex;
            set
            {
                this.Set(ref this._serverConnectionListIndex, value);
            }
        }

        public ObservableCollection<string> TransmitProtocol
        {
            get => _transmitProtocol;
            set
            {
                this.Set(ref this._transmitProtocol, value);
            }
        }
        public int TransmitProtocolIndex
        {
            get => _transmitProtocolIndex;
            set
            {
                this.Set(ref this._transmitProtocolIndex, value);
            }
        }
        public bool ProtocolMsgTypeEventType
        {
            get => _protocolMsgTypeEventType;
            set
            {
                this.Set(ref this._protocolMsgTypeEventType, value);
            }
        }
        public bool ProtocolMsgTypeOdometer
        {
            get => _protocolMsgTypeOdometer;
            set
            {
                this.Set(ref this._protocolMsgTypeOdometer, value);
            }
        }
        public bool ProtocolMsgTypeGsmStreng
        {
            get => _protocolMsgTypeGsmStreng;
            set
            {
                this.Set(ref this._protocolMsgTypeGsmStreng, value);
            }
        }
        public bool ProtocolMsgTypeTemperature
        {
            get => _protocolMsgTypeTemperature;
            set
            {
                this.Set(ref this._protocolMsgTypeTemperature, value);
            }
        }
        public bool ProtocolMsgTypeEngHours
        {
            get => _protocolMsgTypeEngHours;
            set
            {
                this.Set(ref this._protocolMsgTypeEngHours, value);
            }
        }
        public bool ProtocolMsgTypeIgnState
        {
            get => _protocolMsgTypeIgnState;
            set
            {
                this.Set(ref this._protocolMsgTypeIgnState, value);
            }
        }
        public bool ProtocolMsgTypeLatitude1
        {
            get => _protocolMsgTypeLatitude;
            set
            {
                this.Set(ref this._protocolMsgTypeLatitude, value);
            }
        }
        public bool ProtocolMsgTypeLongitude1
        {
            get => _protocolMsgTypeLongitude;
            set
            {
                this.Set(ref this._protocolMsgTypeLongitude, value);
            }
        }
        public bool ProtocolMsgTypeFix1
        {
            get => _protocolMsgTypeFix;
            set
            {
                this.Set(ref this._protocolMsgTypeFix, value);
            }
        }
        public bool ProtocolMsgTypeAltitude1
        {
            get => _protocolMsgTypeAltitude;
            set
            {
                this.Set(ref this._protocolMsgTypeAltitude, value);
            }
        }
        public bool ProtocolMsgTypeHeading1
        {
            get => _protocolMsgTypeHeading;
            set
            {
                this.Set(ref this._protocolMsgTypeHeading, value);
            }
        }
        public bool ProtocolMsgTypeSpeed1
        {
            get => _protocolMsgTypeSpeed;
            set
            {
                this.Set(ref this._protocolMsgTypeSpeed, value);
            }
        }
        public bool ProtocolMsgTypeHdop1
        {
            get => _protocolMsgTypeHdop;
            set
            {
                this.Set(ref this._protocolMsgTypeHdop, value);
            }
        }
        public bool ProtocolMsgTypeSatsCount1
        {
            get => _protocolMsgTypeSatsCount;
            set
            {
                this.Set(ref this._protocolMsgTypeSatsCount, value);
            }
        }
        public bool ProtocolMsgTypePowerInternal
        {
            get => _protocolMsgTypePowerInternal;
            set
            {
                this.Set(ref this._protocolMsgTypePowerInternal, value);
            }
        }
        public bool ProtocolMsgTypePowerExternal
        {
            get => _protocolMsgTypePowerExternal;
            set
            {
                this.Set(ref this._protocolMsgTypePowerExternal, value);
            }
        }
        public bool ProtocolMsgTypeDigitalOuts
        {
            get => _protocolMsgTypeDigitalOuts;
            set
            {
                this.Set(ref this._protocolMsgTypeDigitalOuts, value);
            }
        }
        public bool ProtocolMsgTypeInputs
        {
            get => _protocolMsgTypeInputs;
            set
            {
                this.Set(ref this._protocolMsgTypeInputs, value);
            }
        }
        public bool ProtocolMsgTypeLlsInternal
        {
            get => _protocolMsgTypeLlsInternal;
            set
            {
                this.Set(ref this._protocolMsgTypeLlsInternal, value);
            }
        }
        public bool ProtocolMsgTypeTemperature1Wire
        {
            get => _protocolMsgTypeTemperature1Wire; set
            {
                this.Set(ref this._protocolMsgTypeTemperature1Wire, value);
            }
        }

        public ObservableCollection<string> TypeOfIgnitionDetection
        {
            get => _typeOfIgnitionDetection;
            set
            {
                this.Set(ref this._typeOfIgnitionDetection, value);
            }
        }
        public int TypeOfIgnitionDetectionIndex
        {
            get => (int)_typeOfIgnitionDetectionIndex;
            set
            {
                this.Set(ref this._typeOfIgnitionDetectionIndex, (IgnitionDetectionType)value);
            }
        }

        public float ManualIgnitionDetectionLowValue
        {
            get => _manualIgnitionDetectionLowValue;
            set
            {
                this.Set(ref this._manualIgnitionDetectionLowValue, value);
            }
        }
        public float ManualIgnitionDetectionHightValue
        {
            get => _manualIgnitionDetectionHightValue;
            set
            {
                this.Set(ref this._manualIgnitionDetectionHightValue, value);
            }
        }

        public int MaxDistanceBetweenTwoPoints
        {
            get => _maxDistanceBetweenTwoPoints; set
            {
                this.Set(ref this._maxDistanceBetweenTwoPoints, value);
            }
        }
        public int MaxDistanceAngeOfRotationBetweenTwoPoints
        {
            get => _maxDistanceAngeOfRotationBetweenTwoPoints;
            set
            {
                this.Set(ref this._maxDistanceAngeOfRotationBetweenTwoPoints, value);
            }
        }
        public int AccelerationThresholdDetermMotion
        {
            get => _accelerationThresholdDetermMotion;
            set
            {
                this.Set(ref this._accelerationThresholdDetermMotion, value);
            }
        }
        public int MinSpeedForDetectionMotion
        {
            get => _minSpeedForDetectionMotion;
            set
            {
                this.Set(ref this._minSpeedForDetectionMotion, value);
            }
        }
        public int MaxSpeedForDetectionParking
        {
            get => _maxSpeedForDetectionParking;
            set
            {
                this.Set(ref this._maxSpeedForDetectionParking, value);
            }
        }

        public ObservableCollection<OneWireItemModel> OneWireSettingsModelList => _oneWireSettingsModelList;

        public ObservableCollection<InputItemSettingsModel> InputsSettingsModelList => _inputsSettingsModelList;

        public bool EnableSmsSubscribing_0
        {
            get => _enableSmsSubscribing_0;
            set
            {
                this.Set(ref this._enableSmsSubscribing_0, value);
                ValidateSettings();
            }
        }
        public bool EnableSmsSubscribing_1
        {
            get => _enableSmsSubscribing_1;
            set
            {
                this.Set(ref this._enableSmsSubscribing_1, value);
                ValidateSettings();
            }
        }
        public bool AllowSmsControl_0
        {
            get => _allowSmsControl_0;
            set
            {
                this.Set(ref this._allowSmsControl_0, value);
            }
        }
        public bool AllowSmsControl_1
        {
            get => _allowSmsControl_1;
            set
            {
                this.Set(ref this._allowSmsControl_1, value);
            }
        }
        public ObservableCollection<string> SensorActivation_0_phone_0 => _sensorActivation_0_phone_0;

        public int SensorActivation_0_phone_0_index
        {
            get => _sensorActivation_0_phone_0_index;
            set
            {
                this.Set(ref this._sensorActivation_0_phone_0_index, value);
            }
        }
        public ObservableCollection<string> SensorActivation_0_phone_1 => _sensorActivation_0_phone_1;

        public int SensorActivation_0_phone_1_index
        {
            get => _sensorActivation_0_phone_1_index;
            set
            {
                this.Set(ref this._sensorActivation_0_phone_1_index, value);
            }
        }
        public ObservableCollection<string> SensorActivation_1_phone_0 => _sensorActivation_1_phone_0;

        public int SensorActivation_1_phone_0_index
        {
            get => _sensorActivation_1_phone_0_index;
            set
            {
                this.Set(ref this._sensorActivation_1_phone_0_index, value);
            }
        }
        public ObservableCollection<string> SensorActivation_1_phone_1 => _sensorActivation_1_phone_1;

        public int SensorActivation_1_phone_1_index
        {
            get => _sensorActivation_1_phone_1_index;
            set
            {
                this.Set(ref this._sensorActivation_1_phone_1_index, value);
            }
        }
        public ObservableCollection<string> TemperatureDecreasePhone_0 => _temperatureDecreasePhone_0;

        public int TemperatureDecreasePhone_0_index
        {
            get => _temperatureDecreasePhone_0_index;
            set
            {
                this.Set(ref this._temperatureDecreasePhone_0_index, value);
            }
        }
        public ObservableCollection<string> TemperatureDecreasePhone_1 => _temperatureDecreasePhone_1;

        public int TemperatureDecreasePhone_1_index
        {
            get => _temperatureDecreasePhone_1_index;
            set
            {
                this.Set(ref this._temperatureDecreasePhone_1_index, value);
            }
        }
        public ObservableCollection<string> TemperatureIncreasePhone_0 => _temperatureIncreasePhone_0;

        public int TemperatureIncreasePhone_0_index
        {
            get => _temperatureIncreasePhone_0_index;
            set
            {
                this.Set(ref this._temperatureIncreasePhone_0_index, value);
            }
        }
        public ObservableCollection<string> TemperatureIncreasePhone_1 => _temperatureIncreasePhone_1;

        public int TemperatureIncreasePhone_1_index
        {
            get => _temperatureIncreasePhone_1_index;
            set
            {
                this.Set(ref this._temperatureIncreasePhone_1_index, value);
            }
        }
        public ObservableCollection<string> TemperatureRecoveryPhone_0 => _temperatureRecoveryPhone_0;

        public int TemperatureRecoveryPhone_0_index
        {
            get => _temperatureRecoveryPhone_0_index;
            set
            {
                this.Set(ref this._temperatureRecoveryPhone_0_index, value);
            }
        }
        public ObservableCollection<string> TemperatureRecoveryPhone_1 => _temperatureRecoveryPhone_1;

        public int TemperatureRecoveryPhone_1_index
        {
            get => _temperatureRecoveryPhone_1_index;
            set
            {
                this.Set(ref this._temperatureRecoveryPhone_1_index, value);
            }
        }

        public ObservableCollection<LlsDataViewModel> LlsDataViewModelList => _llsDataViewModelList;

        public string Error => string.Empty;

        public string PhoneNumber1 { get => _phoneNumber1; set => this.Set(ref this._phoneNumber1, value); }
        public string PhoneNumber2 { get => _phoneNumber2; set => this.Set(ref this._phoneNumber2, value); }
        public bool SettingsIsValid { get => _settingsIsValid; set => this.Set(ref this._settingsIsValid, value); }

        public ObservableCollection<string> SensorActivation_2_phone_0 => _sensorActivation_2_phone_0;

        public int SensorActivation_2_phone_0_index { get => _sensorActivation_2_phone_0_index; set => this.Set(ref this._sensorActivation_2_phone_0_index, value); }

        public ObservableCollection<string> SensorActivation_2_phone_1 => _sensorActivation_2_phone_1;

        public int SensorActivation_2_phone_1_index { get => _sensorActivation_2_phone_1_index; set => this.Set(ref this._sensorActivation_2_phone_1_index, value); }

        public ObservableCollection<string> Lls1DrainPhone_0 => _lls1DrainPhone_0;

        public int Lls1DrainPhone_0_index { get => _lls1DrainPhone_0_index; set => this.Set(ref this._lls1DrainPhone_0_index, value); }

        public ObservableCollection<string> Lls1DrainPhone_1 => _lls1DrainPhone_1;

        public int Lls1DrainPhone_1_index { get => _lls1DrainPhone_1_index; set => this.Set(ref this._lls1DrainPhone_1_index, value); }

        public ObservableCollection<string> Lls1FillUpPhone_0 => _lls1FillUpPhone_0;

        public int Lls1FillUpPhone_0_index { get => _lls1FillUpPhone_0_index; set => this.Set(ref this._lls1FillUpPhone_0_index, value); }

        public ObservableCollection<string> Lls1FillUpPhone_1 => _lls1FillUpPhone_1;

        public int Lls1FillUpPhone_1_index { get => _lls1FillUpPhone_1_index; set => this.Set(ref this._lls1FillUpPhone_1_index, value); }

        public SettingsConnectionViewModel ServersConnectionModel => _serversConnectionModel;

        public int MaxHeading
        {
            get => _maxHeading;
            set
            {
                _maxHeading = value;
                OnPropertyChanged();
            }
        }

        public int MaxStendingTime
        {
            get => _maxStendingTime;
            set
            {
                _maxStendingTime = value;
                OnPropertyChanged();
            }
        }

        /**/
        /* validation */
        /**/
        Regex regexApn = new Regex("^[a-z0-9!#$%&'()*+,.\\/:;<=>?@\\[\\] ^_`{|}~-]{1,32}$");
        Regex regexPhone = new Regex(@"\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})");
        Regex regexPinCode = new Regex("^([\\d]{4})?()$");

        public string this[string columnName]
        {
            get
            {
                string resultMessage = "";
                if (columnName == nameof(Apn))
                {
                    _apnIsValid = regexApn.IsMatch(this.Apn);
                    if (!_apnIsValid)
                    {
                        resultMessage = "Value is not valid!";
                    }
                }

                if (columnName == nameof(ApnLogin))
                {
                    if (this.ApnLogin.Length != 0)
                    {
                        _apnLoginIsValid = regexApn.IsMatch(this.ApnLogin);
                        if (!_apnLoginIsValid)
                        {
                            resultMessage = "Value is not valid!";
                        }
                    }
                    else
                    {
                        _apnLoginIsValid = true;
                    }
                }

                if (columnName == nameof(ApnPassword))
                {
                    if (this.ApnPassword.Length != 0)
                    {
                        _apnPasswordIsValid = regexApn.IsMatch(this.ApnPassword);
                        if (!_apnPasswordIsValid)
                        {
                            resultMessage = "Value is not valid!";
                        }
                    }
                    else
                    {
                        _apnPasswordIsValid = false;
                    }
                }

                if (columnName == nameof(ApnPinCode))
                {
                    _apnPinCodeIsValid = regexPinCode.IsMatch(this.ApnPinCode);
                    if (!_apnPinCodeIsValid)
                    {
                        resultMessage = "Value is not valid!";
                    }
                }

                if (columnName == nameof(PhoneNumber1))
                {
                    _phoneNumber1IsValid = regexPhone.IsMatch(this.PhoneNumber1);
                    if (!_phoneNumber1IsValid)
                    {
                        resultMessage = "Value is not valid!";
                    }
                }

                if (columnName == nameof(PhoneNumber2))
                {
                    _phoneNumber2IsValid = regexPhone.IsMatch(this.PhoneNumber2);
                    if (!_phoneNumber2IsValid)
                    {
                        resultMessage = "Value is not valid!";
                    }
                }

                ValidateSettings();
                return (resultMessage.Length == 0) ? null : resultMessage;
            }
        }
        private void ValidateSettings()
        {
            if (_enableSmsSubscribing_0 == false && _enableSmsSubscribing_1 == false)
            {
                var isValid = _apnIsValid && _apnLoginIsValid && _serverConnectionIsValid
                   && _oneWireSettingsIsValid0 && _oneWireSettingsIsValid1
                   && _oneWireSettingsIsValid2 && _oneWireSettingsIsValid3 && _inputSettingsIsValid0
                   && _inputSettingsIsValid1 && _inputSettingsIsValid2 && _llsSettings1_IsValid && _llsSettings2_IsValid;
                SettingsIsValid = isValid;
            }
            else if (_enableSmsSubscribing_0 == false)
            {
                var isValid = _apnIsValid && _apnLoginIsValid && _serverConnectionIsValid
                    && _oneWireSettingsIsValid0 && _oneWireSettingsIsValid1 && _oneWireSettingsIsValid2 && _oneWireSettingsIsValid3
                    && _inputSettingsIsValid0 && _inputSettingsIsValid1 && _inputSettingsIsValid2 && _phoneNumber2IsValid && _llsSettings1_IsValid && _llsSettings2_IsValid;
                SettingsIsValid = isValid;
            }
            else if (_enableSmsSubscribing_1 == false)
            {
                var isValid = _apnIsValid && _apnLoginIsValid && _serverConnectionIsValid
                   && _oneWireSettingsIsValid0 && _oneWireSettingsIsValid1
                   && _oneWireSettingsIsValid2 && _oneWireSettingsIsValid3 && _inputSettingsIsValid0
                   && _inputSettingsIsValid1 && _inputSettingsIsValid2 && _phoneNumber1IsValid && _llsSettings1_IsValid && _llsSettings2_IsValid;
                SettingsIsValid = isValid;
            }
        }


        public void Dispose() { }
    }
}
