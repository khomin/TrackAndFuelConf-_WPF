using MahApps.Metro.Controls;
using TrackAndFuel.ViewModel;

namespace TrackAndFuel.Tracker
{
    /// <summary>
    /// Interaction logic for Main.xaml
    /// </summary>
    public partial class Main : MetroWindow
    {
        private MainViewModel _viewModel;
        public Main()
        {
            InitializeComponent();
            _viewModel = new MainViewModel();
            DataContext = _viewModel;

            _viewModel.NavigateContent = new StartPanel(_viewModel);

            _viewModel.RightPannelModel.CurrentData.Ain2Value = 10.20f;

            //_viewModel.NavigateContent = new TrackerMainPanel(_viewModel);
        }
    }
}
