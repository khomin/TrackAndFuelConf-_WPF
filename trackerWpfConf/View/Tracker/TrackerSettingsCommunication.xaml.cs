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

namespace trackerWpfConf.settings
{
    /// <summary>
    /// Interaction logic for SettingsCommunication.xaml
    /// </summary>
    public partial class SettingsCommunication : UserControl
    {
        private MainViewModel viewModel;

        public SettingsCommunication()
        {
            InitializeComponent();

            this.DataContextChanged += (object sender, DependencyPropertyChangedEventArgs e) =>
            {
                viewModel = this.DataContext as MainViewModel;

                if(viewModel.SettingsModel.ServersConnectionModel != null)
                {
                    ServerItem.DataContext = viewModel.SettingsModel.ServersConnectionModel[0];
                }
            };

            ServerCurrent.SelectionChanged += (a, b) => {
                var combox = a as ComboBox;
                ServerItem.DataContext = viewModel.SettingsModel.ServersConnectionModel[combox.SelectedIndex];
            };
        }
    }
}
