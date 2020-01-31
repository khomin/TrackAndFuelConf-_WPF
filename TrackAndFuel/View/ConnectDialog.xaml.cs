using MahApps.Metro.Controls;
using TrackAndFuel.ViewModel;

namespace TrackAndFuel.Tracker
{
    /// <summary>
    /// Interaction logic for ConnectDialog.xaml
    /// </summary>
    public partial class ConnectDialog : MetroWindow
    {
        private MainViewModel _viewModel;
        public ConnectDialog(MainViewModel viewModel)
        {
            InitializeComponent();

            _viewModel = viewModel;
        }

        private void connectButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void refreshButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            _viewModel.ConnectViewModel.ResearchPorts();
            PortComBox.SelectedIndex = 0;
        }

        private void closeButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
        public string getSelectedPortName() 
        {
            return PortComBox.SelectedItem.ToString();
        }
    }
}
