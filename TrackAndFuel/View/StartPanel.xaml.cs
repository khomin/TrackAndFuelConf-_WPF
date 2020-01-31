using System;
using System.Windows;
using System.Windows.Controls;
using TrackAndFuel.ViewModel;

namespace TrackAndFuel.Tracker
{
    /// <summary>
    /// Interaction logic for StartPanel.xaml
    /// </summary>
    public partial class StartPanel : Page
    {
        private MainViewModel _viewModel;
        public StartPanel(MainViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
            _viewModel = viewModel;
        }

        private void connectPanel_ConnectEvent(object sender, EventArgs e)
        {
            /* create the new viewmodel for connection */
            _viewModel.ConnectViewModel = new ConnectPanelViewModel();
            
            /* create dialog selecting com ports */
            ConnectDialog connectDialog = new ConnectDialog(_viewModel);
            connectDialog.WindowStartupLocation = WindowStartupLocation.Manual;
            connectDialog.WindowStyle = WindowStyle.None;
            connectDialog.ShowInTaskbar = false;
            connectDialog.ResizeMode = ResizeMode.NoResize;
            connectDialog.AllowsTransparency = true;
            var ownerContent = (FrameworkElement)Content;
            var contentPoints = ownerContent.PointToScreen(new Point(0, 0));
            connectDialog.Top = ownerContent.ActualHeight / 2;
            connectDialog.Left = ownerContent.ActualWidth / 2;
            var result = connectDialog.ShowDialog();
            if (result == true) 
            {
                _viewModel.NavigateContent = new TrackerMainPanel(_viewModel, connectDialog.getSelectedPortName());
            }
        }
    }
}
