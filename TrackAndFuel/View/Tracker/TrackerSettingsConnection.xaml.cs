using System.Windows;
using System.Windows.Controls;
using TrackAndFuel.ViewModel;

namespace TrackAndFuel.Tracker
{
    /// <summary>
    /// Interaction logic for SettingsConnection.xaml
    /// </summary>
    public partial class SettingsConnection : UserControl
    {
        private SettingsConnectionViewModel viewModel;

        public SettingsConnection()
        {
            InitializeComponent();

            this.DataContextChanged += (object sender, DependencyPropertyChangedEventArgs e) => {
                viewModel = this.DataContext as SettingsConnectionViewModel;
                DataContext = viewModel;
            };
        }
    }
}
