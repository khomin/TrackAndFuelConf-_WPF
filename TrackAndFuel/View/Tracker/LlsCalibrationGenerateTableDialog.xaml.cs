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
using System.Windows.Shapes;
using TrackAndFuel.ViewModel;

namespace TrackAndFuel.Tracker
{
    /// <summary>
    /// Interaction logic for LlsCalibrationGenerateTableDialog.xaml
    /// </summary>
    public partial class LlsCalibrationGenerateTableDialog : Window
    {
        private LlsCalibrateTableGenerateViewModel _viewModel;
        public LlsCalibrationGenerateTableDialog()
        {
            InitializeComponent();

            _viewModel = new LlsCalibrateTableGenerateViewModel();
            DataContext = _viewModel;
        }
        public int GetLiters()
        {
            return _viewModel.Liters;
        }
        public int GetSteps()
        {
            return _viewModel.Steps;
        }

        private void GenerateButtonClicked(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void CloseButtonClicked(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
