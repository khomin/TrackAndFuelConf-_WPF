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
    }
}
