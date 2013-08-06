using System.Windows;
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

            UsernameText.Focus();
        }

        private void SaveClick(object sender, RoutedEventArgs e)
        {
            _viewModel.Save();
            Close();
        }

        private void CancelClick(object sender, RoutedEventArgs e)
        {
            _viewModel.Cancel();
            Close();
        }
    }
}
