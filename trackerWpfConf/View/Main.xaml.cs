using MahApps.Metro.Controls;
using System.Windows;

namespace trackerWpfConf.View
{
    /// <summary>
    /// Interaction logic for Main.xaml
    /// </summary>
    public partial class Main : MetroWindow
    {
        public Main()
        {
            InitializeComponent();
        }

        private void connectPannel_ConnectEvent(object sender, System.EventArgs e)
        {
            ConnectDialog connectDialog = new ConnectDialog();
            connectDialog.WindowStartupLocation = WindowStartupLocation.Manual;
            connectDialog.WindowStyle = WindowStyle.None;
            connectDialog.ShowInTaskbar = false;
            connectDialog.ResizeMode = ResizeMode.NoResize;
            connectDialog.AllowsTransparency = true;
            var ownerContent = (FrameworkElement)Content;
            var contentPoints = ownerContent.PointToScreen(new Point(0, 0));
            connectDialog.Top = ownerContent.ActualHeight / 2;
            connectDialog.Left = ownerContent.ActualWidth / 2;
            var dialog = connectDialog.ShowDialog();
        }
    }
}
