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
using trackerWpfConf.ViewModel;

namespace trackerWpfConf.View.Tracker
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
            this.DataContextChanged += (object sender, DependencyPropertyChangedEventArgs e) =>
            {
                viewModel = this.DataContext as LlsDataViewModel;
            };

        }

        private void ChangeLLsLevelMinMax(object sender, RoutedEventArgs e)
        {
            LlsChangeLevelDialog dialog = new LlsChangeLevelDialog(viewModel.MinLevel, viewModel.MaxLevel);
            var ownerContent = (FrameworkElement)Content;
            var contentPoints = ownerContent.PointToScreen(new Point(0, 0));
            dialog.Top = ownerContent.ActualHeight / 2;
            dialog.Left = ownerContent.ActualWidth / 2;
            var result = dialog.ShowDialog();
            if (result == true)
            {
                viewModel.MaxLevel = dialog.GetMaxLevel();
                viewModel.MinLevel = dialog.GetMinLevel();
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
                viewModel.CalibrateTables.Insert(viewModel.CalibrateTablesIndex, new LlsDataViewModel.CalibrateTable(dialog.GetLevel(), dialog.GetValue()));
                viewModel.CalibrateTablesIndex = viewModel.CalibrateTables.Count - 1;
            }
        }
        private void EnterCurrentVolumeCalibrateTable(object sender, RoutedEventArgs e)
        {
            viewModel.CalibrateTables.Add(new LlsDataViewModel.CalibrateTable(100, 200));
            viewModel.CalibrateTablesIndex = viewModel.CalibrateTables.Count-1;
        }
    }
}
