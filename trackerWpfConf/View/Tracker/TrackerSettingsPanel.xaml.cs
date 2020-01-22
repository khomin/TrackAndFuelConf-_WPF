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

namespace trackerWpfConf
{
    /// <summary>
    /// Interaction logic for SettingsPannel.xaml
    /// </summary>
    public partial class SettingsPannel : UserControl
    {
        private MainViewModel viewModel;

        public SettingsPannel()
        {
            InitializeComponent();
            this.DataContextChanged += (object sender, DependencyPropertyChangedEventArgs e) =>
            {
                viewModel = this.DataContext as MainViewModel;
            };
        }
    }
}
