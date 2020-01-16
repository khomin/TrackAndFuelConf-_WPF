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

namespace trackerWpfConf.View.Tracker
{
    /// <summary>
    /// Interaction logic for TrackerMainPanel.xaml
    /// </summary>
    public partial class TrackerMainPanel : Page
    {
        private MainViewModel viewModel;
        public TrackerMainPanel(MainViewModel viewModel)
        {
            InitializeComponent();

            this.viewModel = viewModel;
        }

        //public MainViewModel viewModel
        //{
        //    get { return (MainViewModel)GetValue(ViewModelProperty); }
        //    set { SetValue(ViewModelProperty, value); }
        //}

        //public static DependencyProperty ViewModelProperty { get => addressProperty; set => addressProperty = value; }

        //private static DependencyProperty addressProperty =
        //    DependencyProperty.Register("Address", typeof(MainViewModel), typeof(Main), new PropertyMetadata(null));
    }
}
