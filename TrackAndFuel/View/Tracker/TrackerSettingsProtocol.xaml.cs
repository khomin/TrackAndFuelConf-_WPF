using System.Windows;
using System.Windows.Controls;
using TrackAndFuel.ViewModel;

namespace TrackAndFuel.Tracker
{
    /// <summary>
    /// Interaction logic for TrackerSettingsProtocol.xaml
    /// </summary>
    public partial class TrackerSettingsProtocol : UserControl
    {
        private MainViewModel viewModel;
        public TrackerSettingsProtocol()
        {
            InitializeComponent();

            this.DataContextChanged += (object sender, DependencyPropertyChangedEventArgs e) =>
            {
                viewModel = this.DataContext as MainViewModel;
            };
        }
    }
}
