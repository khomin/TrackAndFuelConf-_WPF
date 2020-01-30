using System.Windows;
using System.Windows.Controls;
using TrackAndFuel.ViewModel;

namespace TrackAndFuel.Tracker
{
    /// <summary>
    /// Interaction logic for TrackerOneWireSettings.xaml
    /// </summary>
    public partial class TrackerOneWireSettings : UserControl
    {
        private MainViewModel viewModel;
        public TrackerOneWireSettings()
        {
            InitializeComponent();
            this.DataContextChanged += (object sender, DependencyPropertyChangedEventArgs e) =>
            {
                viewModel = this.DataContext as MainViewModel;
            };
        }
    }
}
