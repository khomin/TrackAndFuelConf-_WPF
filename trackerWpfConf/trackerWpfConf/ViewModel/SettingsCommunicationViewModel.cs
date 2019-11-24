using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using testViewModel;

namespace trackerWpfConf.ViewModel
{
    class SettingsCommunicationViewModel : ViewModelBase
    {
        private string apnName = "defaultName";
        private string apnPassword = "defaultPassword";
        private string apnPdp = "defaultPdp";

        //settingscommunicationviewmodel()
        //{
        //    //var _datetimer = new timer(_datetimer_tick, dispatcher, 0, 60000);
        //    var dispatchertimer = new system.windows.threading.dispatchertimer();
        //    dispatchertimer.tick += new eventhandler(dispatchertimer_tick);
        //    dispatchertimer.interval = new timespan(0, 0, 1);
        //    dispatchertimer.start();
        //}

        //private int counter = 0;
        //private void dispatchertimer_tick(object sender, eventargs e)
        //{
        //    apnname = counter.tostring();
        //    counter += 1;
        //    // updating the label which displays the current second
        //    //lblseconds.content = datetime.now.second;

        //    //// forcing the commandmanager to raise the requerysuggested event
        //    //commandmanager.invalidaterequerysuggested();
        //}

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