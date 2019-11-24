using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace trackerWpfConf.settingsTabItems
{
    class SettingsCommunicationViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string userId = "TestUserId";

        public string UserId
        {
            get { return userId; }
            set
            {
                userId = value;
                OnPropertyChange("UserId");
            }
        }


        protected void OnPropertyChange(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
