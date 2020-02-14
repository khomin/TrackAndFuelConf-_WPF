using System.Windows;
using System.Windows.Controls;
using TrackAndFuel.ViewModel;

namespace TrackAndFuel.Tracker
{
    /// <summary>
    /// Interaction logic for SettingsPannel.xaml
    /// </summary>
    public partial class SettingsPannel : UserControl
    {
        private MainViewModel viewModel;

        public SettingsPannel()
        {
            InitializeComponent();
            this.DataContextChanged += (object sender, DependencyPropertyChangedEventArgs e) => {
                viewModel = this.DataContext as MainViewModel;
            };
        }
    }
}
