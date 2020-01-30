using System.Windows;
using System.Windows.Controls;
using TrackAndFuel.ViewModel;

namespace TrackAndFuel.Tracker
{
    /// <summary>
    /// Interaction logic for TrackerTrackSettings.xaml
    /// </summary>
    public partial class TrackerTrackSettings : UserControl
    {
        private MainViewModel viewModel;
        public TrackerTrackSettings()
        {
            InitializeComponent();

            this.DataContextChanged += (object sender, DependencyPropertyChangedEventArgs e) =>
            {
                viewModel = this.DataContext as MainViewModel;
            };
        }
    }
}
