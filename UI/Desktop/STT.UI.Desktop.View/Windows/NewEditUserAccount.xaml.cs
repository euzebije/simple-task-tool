using System.Windows;
using System.Windows.Controls;
using STT.Data;
using STT.UI.Desktop.ViewModel;

namespace STT.UI.Desktop.View
{
    /// <summary>
    /// Interaction logic for NewEditUserAccount.xaml
    /// </summary>
    public partial class NewEditUserAccount
    {
        private readonly UserAccountViewModel _viewModel;

        public NewEditUserAccount(UserAccountViewModel viewModel, DataCommand dataCommand, Window owner)
        {
            InitializeComponent();

            _viewModel = viewModel;

            DataContext = viewModel;
            Owner = owner;

            Title = dataCommand == DataCommand.New
                        ? Common.Localization.NewUserAccount
                        : string.Format(Common.Localization.EditUserAccount, viewModel.Username);
            if (dataCommand == DataCommand.Edit)
            {
                PasswordBlock.IsEnabled = false;
                PasswordText.IsEnabled = false;
            }

            UsernameText.Focus();
        }

        private void OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            var passwordBox = sender as PasswordBox;
            if (passwordBox != null)
                _viewModel.Password = passwordBox.Password;
        }

        private void SaveClick(object sender, RoutedEventArgs e)
        {
            _viewModel.Save();
            if (_viewModel.IsValid)
                Close();
        }

        private void CancelClick(object sender, RoutedEventArgs e)
        {
            _viewModel.Cancel();
            Close();
        }
    }
}
