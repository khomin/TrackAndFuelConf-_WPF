using MahApps.Metro.Controls;
using TrackAndFuel.ViewModel;

namespace TrackAndFuel.Tracker
{
    /// <summary>
    /// Interaction logic for Main.xaml
    /// </summary>
    public partial class Main : MetroWindow
    {
        private MainViewModel viewModel;
        public Main()
        {
            InitializeComponent();
            viewModel = new MainViewModel();
            DataContext = viewModel;

            viewModel.NavigateContent = new TrackerMainPanel(viewModel); //  StartPanel(viewModel); //
        }
    }
}
