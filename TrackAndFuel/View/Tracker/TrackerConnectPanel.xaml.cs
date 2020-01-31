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
        public event EventHandler disconnectEvent;
        private MainViewModel viewModel = null;

        public TrackerConnectPannel() 
        {
            InitializeComponent();

            this.DataContextChanged += (object sender, DependencyPropertyChangedEventArgs e) => {
                viewModel = this.DataContext as MainViewModel;
                DataContext = viewModel;
            };
        }

        private void Disconnect_Click(object sender, RoutedEventArgs e)
        {
            disconnectEvent(this, EventArgs.Empty);
            viewModel.NavigateContent.NavigationService.GoBack();
        }
    }
}
