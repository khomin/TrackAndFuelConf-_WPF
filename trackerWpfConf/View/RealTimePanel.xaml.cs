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
using System.Windows.Navigation;
using System.Windows.Shapes;
using trackerWpfConf.ViewModel;

namespace trackerWpfConf 
{
    /// <summary>
    /// Interaction logic for RealTimePanel.xaml
    /// </summary>
    public partial class RealTimePanel : UserControl
    {
        private readonly RightPannelViewModel _rightPanelViewModel;
        
        public RealTimePanel()
        {
            InitializeComponent();

            _rightPanelViewModel = new RightPannelViewModel();
            DataContext = _rightPanelViewModel;
        }
    }
}
