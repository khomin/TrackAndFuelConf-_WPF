using MahApps.Metro.Controls;
using System.Windows;
using trackerWpfConf.ViewModel;

namespace trackerWpfConf.View
{
    /// <summary>
    /// Interaction logic for ConnectDialog.xaml
    /// </summary>
    public partial class ConnectDialog : MetroWindow
    {
        private MainViewModel viewModel;
        public ConnectDialog()
        {
            InitializeComponent();

            this.DataContextChanged += (object sender, DependencyPropertyChangedEventArgs e) => {
                viewModel = this.DataContext as MainViewModel;
            };
        }

        private void connectButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void refreshButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            viewModel.ConnectViewModel.ResearchPorts();
            PortComBox.SelectedIndex = 0;
        }

        private void closeButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
