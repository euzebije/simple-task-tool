using System.Windows;
using STT.UI.Desktop.ViewModel;

namespace STT.UI.Desktop.View
{
    /// <summary>
    /// Interaction logic for ChangeUserAccountPassword.xaml
    /// </summary>
    public partial class ChangeUserAccountPassword
    {
        private readonly UserAccountViewModel _viewModel;

        public ChangeUserAccountPassword(UserAccountViewModel viewModel, Window owner)
        {
            InitializeComponent();

            _viewModel = viewModel;

            DataContext = viewModel;
            Owner = owner;

            Title = Common.Localization.ChangePassword;

            OldPasswordText.Focus();
        }

        private void SaveClick(object sender, RoutedEventArgs e)
        {
            _viewModel.ChangePassword();

            if (!_viewModel.IsPasswordChangeError)
                Close();
        }

        private void CancelClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
