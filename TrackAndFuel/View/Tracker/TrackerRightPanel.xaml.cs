using System.Windows;
using System.Windows.Controls;
using TrackAndFuel.ViewModel;

namespace TrackAndFuel.Tracker
{
    /// <summary>
    /// Interaction logic for RightPanel.xaml
    /// </summary>
    public partial class TrackerRightPanel : UserControl
    {
        private MainViewModel viewModel;

        public TrackerRightPanel()
        {
            InitializeComponent();

            this.DataContextChanged += (object sender, DependencyPropertyChangedEventArgs e) => {
                viewModel = this.DataContext as MainViewModel;
                DataContext = viewModel;
            };
        }
    }
}
