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

        private ObservableCollection<string> _operatorsList;

        private string _apn = "gdata";
        private string _apnLogin = "login";
        private string _apnPassword = "default";
        private string _apnPinCode = "";
        private string _ipDnsAddress = "";

        private ObservableCollection<string> _serversConnection;

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
    }
}
