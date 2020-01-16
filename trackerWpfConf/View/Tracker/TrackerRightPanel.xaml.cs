using System.Windows;
using System.Windows.Controls;
using trackerWpfConf.ViewModel;

namespace trackerWpfConf
{
    /// <summary>
    /// Interaction logic for RightPanel.xaml
    /// </summary>
    public partial class RightPanel : UserControl
    {
        private MainViewModel viewModel;

        public RightPanel()
        {
            InitializeComponent();

            this.DataContextChanged += (object sender, DependencyPropertyChangedEventArgs e) => {
                viewModel = this.DataContext as MainViewModel;
            };
        }
    }
}
