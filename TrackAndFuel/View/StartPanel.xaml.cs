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
        private MainViewModel viewModel;
        public StartPanel(MainViewModel viewModel)
        {
            InitializeComponent();

            this.viewModel = viewModel;
        }

        private void connectPannel_ConnectEvent(object sender, EventArgs e)
        {
            /* create the new viewmodel for connection */
            viewModel.ConnectViewModel = new ConnectPannelViewModel();
            
            /* create dialog selecting com ports */
            ConnectDialog connectDialog = new ConnectDialog(viewModel);
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
                viewModel.NavigateContent = new TrackerMainPanel(viewModel);
            }
        }
    }
}
