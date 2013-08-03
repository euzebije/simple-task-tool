using System.Windows;
using System.Windows.Controls;
using STT.Model.Entity;
using STT.UI.Desktop.ViewModel.Security;
using Localization = STT.UI.Common.Localization;

namespace STT.UI.Desktop.View.Security
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView
    {
        private readonly LoginViewModel _viewModel;

        public UserAccount UserAccount { get; private set; }

        public LoginView(LoginViewModel viewModel)
        {
            InitializeComponent();

            _viewModel = viewModel;

            Title = Localization.ApplicationName;
            DataContext = viewModel;

            viewModel.LoginEnded += ViewModelOnLoginEnded;

            UsernameText.Focus();
        }

        private void ViewModelOnLoginEnded(bool success, UserAccount userAccount)
        {
            DialogResult = success;
            UserAccount = userAccount;

            Close();
        }

        private void OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            var passwordBox = sender as PasswordBox;
            if (passwordBox != null)
                _viewModel.Password = passwordBox.Password;
        }
    }
}
