using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace trackerWpfConf.ViewModel
{
    class SettingsCommunicationViewModel : ViewModelBase
    {
        private string apnName = "defaultName";
        private string apnPassword = "defaultPassword";
        private string apnPdp = "defaultPdp";

        //public SettingsCommunicationViewModel()
        //{
        //    _settingsConnection = new ObservableCollection<SettingsConnectionViewModel>();
        //    _settingsConnection.Add(new SettingsConnectionViewModel() { Header = "Primary server", Address = "192.168.15.74" });
        //    _settingsConnection.Add(new SettingsConnectionViewModel() { Header = "Reserve server" });
        //}

        ObservableCollection<SettingsConnectionViewModel> _settingsConnection;
        public ObservableCollection<SettingsConnectionViewModel> SettingsConnection
        {
            get { return _settingsConnection; }
            set
            {
                _settingsConnection = value;
                OnPropertyChanged();
            }
        }

        public string ApnName
        {
            get { return this.apnName; }
            set
            {
                this.apnName = value;
                OnPropertyChanged();
            }
        }

        public string ApnPassword
        {
            get { 
                return apnPassword; 
            }
            set {
                apnPassword = value;
                OnPropertyChanged();
            }
        }

        public string ApnDdp
        {
            get
            {
                return apnPdp;
            }
            set
            {
                apnPdp = value;
                OnPropertyChanged();
            }
        }
    }
}