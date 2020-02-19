using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TrackAndFuel.ViewModel;

namespace TrackAndFuel.Tracker
{
    /// <summary>
    /// Interaction logic for SettingsCommunication.xaml
    /// </summary>
    public partial class SettingsCommunication : UserControl
    {
        private MainViewModel viewModel;

        public SettingsCommunication()
        {
            InitializeComponent();

            this.DataContextChanged += (object sender, DependencyPropertyChangedEventArgs e) =>
            {
                viewModel = this.DataContext as MainViewModel;
            };
        }

        private void ChangePasswordClick(object sender, RoutedEventArgs e)
        {

        }
    }
}
