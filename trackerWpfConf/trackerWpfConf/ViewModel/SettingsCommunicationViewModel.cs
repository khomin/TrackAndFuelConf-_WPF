using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace trackerWpfConf.ViewModel
{
    class SettingsCommunicationViewModel : PropertyChangedBase
    {
        private string apnName;

        public string ApnName
        {
            get { return this.apnName; }
            set
            {
                if (value == this.apnName)
                {
                    return;
                }
                this.apnName = value;
                this.NotifyOfPropertyChange(() => this.ApnName);
            }
        }

        private string simPin;

        public string SimPin
        {
            get { 
                return simPin; 
            }
            set { 
                if(simPin == value)
                {
                    return;
                }
                simPin = value;
                this.NotifyOfPropertyChange(() => this.SimPin);
            }
        }


    }
}