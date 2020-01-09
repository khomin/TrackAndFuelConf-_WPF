using MahApps.Metro.Controls;
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

namespace trackerWpfConf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = viewModel;
        }


        private SomeViewModel viewModel = new SomeViewModel();
        public SomeViewModel ViewModel
        {
            get { return viewModel; }
        }

        //private string _myValue = "some text";
        //public string MyValue
        //{
        //    get  { return _myValue;  }
        //    set 
        //    { 
        //        _myValue = value;
        //        OnPropertyChanged(_myValue);
        //    }        
        //}
    }
}
