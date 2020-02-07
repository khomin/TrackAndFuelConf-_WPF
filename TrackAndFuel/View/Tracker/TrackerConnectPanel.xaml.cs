using System;
using System.Windows;
using System.Windows.Controls;
using TrackAndFuel.ViewModel;

namespace TrackAndFuel.Tracker
{
    /// <summary>
    /// Interaction logic for TrackerConnectPannel.xaml
    /// </summary>
    public partial class TrackerConnectPannel : UserControl
    {
        private MainViewModel viewModel = null;
        public event EventHandler disconnectEvent;
        public event EventHandler saveConfigEvent;
        public event EventHandler loadConfigEvent;
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

        private void LoadConfig_Click(object sender, RoutedEventArgs e)
        {
            loadConfigEvent(this, EventArgs.Empty);
        }
        private void SaveConfig_Click(object sender, RoutedEventArgs e)
        {
            saveConfigEvent(this, EventArgs.Empty);
        }

        private void toolButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
