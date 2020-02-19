using System.Windows;
using TrackAndFuel.ViewModel;

namespace TrackAndFuel.Tracker
{
    /// <summary>
    /// Interaction logic for ChangePasswordDialog.xaml
    /// </summary>
    public partial class ChangePasswordDialog : Window
    {
        private MainViewModel _viewModel;
        private string _currentPassword = "";
        public ChangePasswordDialog(string currentPassword)
        {
            InitializeComponent();
            _viewModel = new MainViewModel();
            DataContext = _viewModel;

            _currentPassword = currentPassword;
        }

        private void ChangePassword(object sender, RoutedEventArgs e)
        {
            if (_currentPassword.Equals(NewPasswordUi.Text))
            {
                MessageBoxResult result = MessageBox.Show("Your new password is equal to the previous",
                                     "Warning",
                                     MessageBoxButton.OK,
                                     MessageBoxImage.Warning);
                this.DialogResult = false;
            }
            else 
            {
                this.DialogResult = true;
            }
        }

        private void CloseWithoutSaving(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        public string GetNewPassword() 
        {
            return NewPasswordUi.Text;
        }
    }
}
