using System.Windows;
using System.Windows.Controls;
using TrackAndFuel.ViewModel;

namespace TrackAndFuel.Tracker
{
    /// <summary>
    /// Interaction logic for TrackerInputSettingsItem.xaml
    /// </summary>
    public partial class TrackerInputSettingsItem : UserControl
    {
        private InputItemSettingsModel viewModel;
        public TrackerInputSettingsItem()
        {
            InitializeComponent();

            this.DataContextChanged += (object sender, DependencyPropertyChangedEventArgs e) => {
                viewModel = this.DataContext as InputItemSettingsModel;
            };
        }
    }
}
