using System.Windows;
using System.Windows.Controls;
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

                if(viewModel.SettingsModel.ServersConnectionModel != null)
                {
                    ServerItem.DataContext = viewModel.SettingsModel.ServersConnectionModel[0];
                }
            };
            
            ServerCurrent.SelectionChanged += (a, b) => {
                var combox = a as ComboBox;
                ServerItem.DataContext = viewModel.SettingsModel.ServersConnectionModel[combox.SelectedIndex];
            };
        }
    }
}
