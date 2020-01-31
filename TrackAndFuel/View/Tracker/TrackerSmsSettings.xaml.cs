using System.Windows;
using System.Windows.Controls;
using TrackAndFuel.ViewModel;

namespace TrackAndFuel.Tracker
{
    /// <summary>
    /// Interaction logic for TrackerSmsSettings.xaml
    /// </summary>
    public partial class TrackerSmsSettings : UserControl
    {
        private MainViewModel viewModel;
        public TrackerSmsSettings()
        {
            InitializeComponent();
            this.DataContextChanged += (object sender, DependencyPropertyChangedEventArgs e) => {
                viewModel = this.DataContext as MainViewModel;
                DataContext = viewModel;
            };
        }
    }
}
