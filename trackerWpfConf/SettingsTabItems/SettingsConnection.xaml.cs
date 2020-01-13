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

namespace trackerWpfConf.settingsTabItems
{
    /// <summary>
    /// Interaction logic for SettingsConnection.xaml
    /// </summary>
    public partial class SettingsConnection : UserControl
    {
        public SettingsConnection()
        {
            InitializeComponent();
        }

        public string Header
        {
            get { return (string)GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }
        public static DependencyProperty HeaderProperty =
            DependencyProperty.Register("Header", typeof(string), typeof(SettingsConnection), new PropertyMetadata(null));

        public string Address
        {
            get { return (string)GetValue(AddressProperty); }
            set { SetValue(AddressProperty, value); }
        }

        public static DependencyProperty AddressProperty { get => addressProperty; set => addressProperty = value; }

        private static DependencyProperty addressProperty =
            DependencyProperty.Register("Address", typeof(string), typeof(SettingsConnection), new PropertyMetadata(null));
    }
}
