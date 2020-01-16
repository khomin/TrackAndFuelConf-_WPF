using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Ports;
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
using trackerWpfConf.Instrumentals;
using trackerWpfConf.ViewModel;

namespace trackerWpfConf
{
    /// <summary>
    /// Interaction logic for TrackerConnectPannel.xaml
    /// </summary>
    public partial class TrackerConnectPannel : UserControl
    {
        public event EventHandler disconnectEvent;
        private MainViewModel viewModel = null;

        public TrackerConnectPannel() 
        {
            InitializeComponent();

            this.DataContextChanged += (object sender, DependencyPropertyChangedEventArgs e) => {
                viewModel = this.DataContext as MainViewModel;
            };
        }

        private void Disconnect_Click(object sender, RoutedEventArgs e)
        {
            disconnectEvent(this, EventArgs.Empty);
            viewModel.NavigateContent.NavigationService.GoBack();
        }
    }
}
