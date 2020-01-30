using System.Windows;
using TrackAndFuel.ViewModel;

namespace TrackAndFuel.Tracker
{
    /// <summary>
    /// Interaction logic for LlsChangeLevelDialog.xaml
    /// </summary>
    public partial class LlsChangeLevelDialog : Window
    {
        private LlsChangeLevelMinMaxViewModel _viewModel;
        public LlsChangeLevelDialog(int minLevel, int maxLevel)
        {
            InitializeComponent();
            _viewModel = new LlsChangeLevelMinMaxViewModel();
            _viewModel.MinLevel = minLevel;
            _viewModel.MaxLevel = maxLevel;
            DataContext = _viewModel;
        }

        public int GetMaxLevel() 
        {
            return _viewModel.MaxLevel;
        }

        public int GetMinLevel()
        {
            return _viewModel.MinLevel;
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
