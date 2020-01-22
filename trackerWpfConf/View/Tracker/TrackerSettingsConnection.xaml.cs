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
    /// Interaction logic for SettingsConnection.xaml
    /// </summary>
    public partial class SettingsConnection : UserControl
    {
        private SettingsConnectionViewModel viewModel;

        public SettingsConnection()
        {
            InitializeComponent();

            this.DataContextChanged += (object sender, DependencyPropertyChangedEventArgs e) => {
                viewModel = this.DataContext as SettingsConnectionViewModel;
            };
        }
    }
}
