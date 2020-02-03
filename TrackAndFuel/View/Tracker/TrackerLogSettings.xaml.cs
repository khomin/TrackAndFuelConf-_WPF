using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TrackAndFuel.ViewModel;

namespace TrackAndFuel.Tracker
{
    /// <summary>
    /// Interaction logic for TrackerLogSettings.xaml
    /// </summary>
    public partial class TrackerLogSettings : UserControl
    {
        private MainViewModel viewModel;
        public TrackerLogSettings()
        {
            InitializeComponent();

            this.DataContextChanged += (object sender, DependencyPropertyChangedEventArgs e) => {
                viewModel = this.DataContext as MainViewModel;
                DataContext = viewModel;
            };
        }

        private void StartCancelLogRead_Click(object sender, RoutedEventArgs e)
        {
            if (viewModel.ConnectViewModel.IsLogReading)
            {
                viewModel.ConnectViewModel.IsLogReading = false;
            }
            else 
            {
                viewModel.ConnectViewModel.IsLogReading = true;
            }
        }

        private void ClearLogClick(object sender, RoutedEventArgs e) 
        {
        
        }
    }
}
