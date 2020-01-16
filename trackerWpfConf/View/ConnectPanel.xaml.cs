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
using System.Windows.Shapes;

namespace trackerWpfConf.View
{
    /// <summary>
    /// Interaction logic for ConnectPanel.xaml
    /// </summary>
    public partial class ConnectPanel : UserControl
    {
        public event EventHandler ConnectEvent;

        public ConnectPanel()
        {
            InitializeComponent();
        }

        private void ConnectButton_Click(object sender, RoutedEventArgs e)
        {
            if(ConnectEvent != null)
            {
                ConnectEvent(this, EventArgs.Empty);
            }
        }
    }
}
