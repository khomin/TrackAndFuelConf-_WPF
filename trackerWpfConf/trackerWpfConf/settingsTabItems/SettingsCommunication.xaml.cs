using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using trackerWpfConf.ViewModel;

namespace trackerWpfConf.settingsTabItems
{
    /// <summary>
    /// Interaction logic for SettingsCommunication.xaml
    /// </summary>
    public partial class SettingsCommunication : UserControl
    {
        private SettingsCommunicationViewModel settingsCommunicationViewModel;

        public SettingsCommunication()
        {
            InitializeComponent();

            settingsCommunicationViewModel = new SettingsCommunicationViewModel();
            DataContext = settingsCommunicationViewModel;

            //var dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            //dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            //dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            //dispatcherTimer.Start();
        }

        //private int counter = 0;
        //private void dispatcherTimer_Tick(object sender, EventArgs e)
        //{
        //    settingsCommunicationViewModel.ApnName = counter.ToString();
        //    counter += 1;
        //}
    }
}
