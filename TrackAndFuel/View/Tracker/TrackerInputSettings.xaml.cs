using System.Windows;
using System.Windows.Controls;
using TrackAndFuel.ViewModel;

namespace TrackAndFuel.Tracker
{
    /// <summary>
    /// Interaction logic for TrackerInputSettings.xaml
    /// </summary>
    public partial class TrackerInputSettings : UserControl
    {
        private MainViewModel viewModel;
        public TrackerInputSettings()
        {
            InitializeComponent();

            this.DataContextChanged += (object sender, DependencyPropertyChangedEventArgs e) => {
                viewModel = this.DataContext as MainViewModel;
            };
        }
    }
}
