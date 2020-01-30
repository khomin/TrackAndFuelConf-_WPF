using System;
using System.Windows;
using System.Windows.Controls;

namespace TrackAndFuel.Tracker
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
