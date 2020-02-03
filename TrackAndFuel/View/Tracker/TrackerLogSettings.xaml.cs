using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Routing;
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
                viewModel.ConnectViewModel.CommandDataBuf.Add(new ConnectPanelViewModel.CommandData("stopTestLog", new byte[0]));
            }
            else 
            {
                viewModel.ConnectViewModel.IsLogReading = true;
                viewModel.ConnectViewModel.CommandDataBuf.Add(new ConnectPanelViewModel.CommandData("startTestLog", new byte[0]));
            }
        }

        private void ClearLogClick(object sender, RoutedEventArgs e) 
        {
            double[][] routePath = new double[3][];
            LocationCollection locs = new LocationCollection();

            routePath[0] = new double[2];
            routePath[1] = new double[2];
            routePath[2] = new double[2];

            routePath[0][0] = (double)30;
            routePath[0][1] = (double)50;
            routePath[1][0] = (double)31;
            routePath[1][1] = (double)50;
            routePath[2][0] = (double)32;
            routePath[2][1] = (double)50;

            for (int i = 0; i < routePath.Length; i++)
            {
                if (routePath[i].Length >= 2)
                {
                    locs.Add(new Microsoft.Maps.MapControl.WPF.Location(routePath[i][0], routePath[i][1]));
                }
            }

            MapPolyline routeLine = new MapPolyline()
            {
                Locations = locs,
                Stroke = new SolidColorBrush(Colors.Blue),
                StrokeThickness = 5
            };

            Map.Children.Add(routeLine);
        }
    }
}
