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

        private ObservableCollection<SettingsConnectionViewModel> _serversConnectionModel;

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

        public string ApnPassword { 
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

        public string Apn { 
            get => _apn;
            set
            {
                _apn = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<SettingsConnectionViewModel> ServersConnectionModel { get => _serversConnectionModel; }
    }
}
