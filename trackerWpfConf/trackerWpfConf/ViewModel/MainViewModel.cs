using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using testViewModel;

namespace trackerWpfConf.ViewModel
{
    class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            _settingsConnection = new ObservableCollection<SettingsConnectionViewModel>() ;
            _settingsConnection.Add(new SettingsConnectionViewModel() { Header = "Primary server" });
            _settingsConnection.Add(new SettingsConnectionViewModel() { Header = "Reserve server" });
        }

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
    }
}
