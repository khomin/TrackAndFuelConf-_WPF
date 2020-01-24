using System.Windows;
using System.Windows.Controls;
using trackerWpfConf.ViewModel;

namespace trackerWpfConf.settings
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
