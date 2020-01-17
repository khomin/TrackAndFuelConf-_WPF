using MahApps.Metro.Controls;
using System;
using System.Windows;
using System.Windows.Navigation;
using trackerWpfConf.View.Tracker;
using trackerWpfConf.ViewModel;

namespace trackerWpfConf.View
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

            viewModel.NavigateContent = new TrackerMainPanel(viewModel); // StartPanel(viewModel); // 
        }
    }
}
