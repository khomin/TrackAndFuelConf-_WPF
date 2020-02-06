using System.Windows;
using System.Windows.Controls;
using TrackAndFuel.ViewModel;

namespace TrackAndFuel.Tracker
{
    /// <summary>
    /// Interaction logic for TrackerLlsItem.xaml
    /// </summary>
    public partial class TrackerLlsItem : UserControl
    {
        private LlsDataViewModel viewModel;
        public TrackerLlsItem()
        {
            InitializeComponent();
            this.DataContextChanged += (object sender, DependencyPropertyChangedEventArgs e) => {
                viewModel = this.DataContext as LlsDataViewModel;
                DataContext = viewModel;
            };
        }

        private void ChangeLLsLevelMinMax(object sender, RoutedEventArgs e)
        {
            LlsChangeLevelDialog dialog = new LlsChangeLevelDialog(int.Parse(viewModel.MinLevel), int.Parse(viewModel.MaxLevel));
            var ownerContent = (FrameworkElement)Content;
            var contentPoints = ownerContent.PointToScreen(new Point(0, 0));
            dialog.Top = ownerContent.ActualHeight / 2;
            dialog.Left = ownerContent.ActualWidth / 2;
            var result = dialog.ShowDialog();
            if (result == true)
            {
                viewModel.MaxLevel = dialog.GetMaxLevel().ToString();
                viewModel.MinLevel = dialog.GetMinLevel().ToString();
            }
        }

        private void ChangeMinLevelClicked(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Set current level as minimum?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
            {
                //do no stuff
            }
            else
            {
                //do yes stuff
            }
        }

        private void ChangeMaxLevelClicked(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Set current level as maximum?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
            {
                //do no stuff
            }
            else
            {
                //do yes stuff
            }
        }

        private void ClearCalibrateTable (object sender, RoutedEventArgs e)
        {
            viewModel.CalibrateTables.Clear();
            viewModel.CalibrateTablesIndex = 0;
        }

        private void RemoveCalibrateTable(object sender, RoutedEventArgs e)
        {
            var index = 0;
            if (viewModel.CalibrateTablesIndex > 0) {
                index = viewModel.CalibrateTablesIndex - 1;
            }
            viewModel.CalibrateTables.RemoveAt(viewModel.CalibrateTablesIndex);
            viewModel.CalibrateTablesIndex = viewModel.CalibrateTables.Count - 1;
        }

        private void AddCalibrateTable(object sender, RoutedEventArgs e)
        {
            LlsCalibrateTableAddLevel dialog = new LlsCalibrateTableAddLevel();
            var ownerContent = (FrameworkElement)Content;
            var contentPoints = ownerContent.PointToScreen(new Point(0, 0));
            dialog.Top = ownerContent.ActualHeight / 2;
            dialog.Left = ownerContent.ActualWidth / 2;
            var result = dialog.ShowDialog();
            if (result == true)
            {
                if (viewModel.CalibrateTables.Count != 0)
                {
                    viewModel.CalibrateTables.Insert(viewModel.CalibrateTablesIndex + 1, new LlsDataViewModel.CalibrateTable(dialog.GetLevel(), dialog.GetValue()));
                }
                else 
                {
                    viewModel.CalibrateTables.Add(new LlsDataViewModel.CalibrateTable(dialog.GetLevel(), dialog.GetValue()));
                }
                viewModel.CalibrateTablesIndex = viewModel.CalibrateTables.Count - 1;
            }
        }
        private void EnterCurrentVolumeCalibrateTable(object sender, RoutedEventArgs e)
        {
            if (viewModel.CalibrateTables.Count != 0)
            {
                viewModel.CalibrateTables.Insert(viewModel.CalibrateTablesIndex + 1, new LlsDataViewModel.CalibrateTable(0, 0));
            }
            else
            {
                viewModel.CalibrateTables.Add(new LlsDataViewModel.CalibrateTable(0, 0));
            }
            viewModel.CalibrateTablesIndex = viewModel.CalibrateTables.Count - 1;
        }

        private void GenerateTableClicked(object sender, RoutedEventArgs e)
        {
            LlsCalibrationGenerateTableDialog dialog = new LlsCalibrationGenerateTableDialog();
            var ownerContent = (FrameworkElement)Content;
            var contentPoints = ownerContent.PointToScreen(new Point(0, 0));
            dialog.Top = ownerContent.ActualHeight / 2;
            dialog.Left = ownerContent.ActualWidth / 2;
            var result = dialog.ShowDialog();
            if (result == true)
            {
                int literCounter = 0;
                viewModel.CalibrateTables.Clear();
                for (int i = 0; i < dialog.GetSteps(); i++) 
                {
                    viewModel.CalibrateTables.Add(new LlsDataViewModel.CalibrateTable(literCounter, dialog.GetLiters()));
                    literCounter += dialog.GetLiters();
                }
            }
        }

        private void ReadTableFromSensorClicked(object sender, RoutedEventArgs e)
        {

        }

        private void SaveTableToSensorClicked(object sender, RoutedEventArgs e)
        {

        }

        private void SaveAsCsvClicked(object sender, RoutedEventArgs e)
        {

        }
    }
}
