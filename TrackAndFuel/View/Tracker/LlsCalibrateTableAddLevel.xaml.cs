using System.Windows;
using TrackAndFuel.ViewModel;

namespace TrackAndFuel.Tracker
{
    /// <summary>
    /// Interaction logic for LlsCalibrateTableAddLevel.xaml
    /// </summary>
    public partial class LlsCalibrateTableAddLevel : Window
    {
        private LlsCalibrateTableAddLevelViewModel _viewModel;
        public LlsCalibrateTableAddLevel()
        {
            InitializeComponent();

            _viewModel = new LlsCalibrateTableAddLevelViewModel();
            DataContext = _viewModel;
        }

        public int GetValue() 
        {
            return _viewModel.Value;
        }
        public int GetLevel()
        {
            return _viewModel.Level;
        }
        private void SaveButtonClicked(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void CloseButtonClicked(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
