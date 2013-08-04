using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using STT.Model.Entity;

namespace STT.UI.Desktop.ViewModel
{
    public class UserAccountListViewModel : ViewModelBase
    {
        private ObservableCollection<UserAccountViewModel> _userAccounts;

        private UserAccountViewModel _selectedUserAccount;

        private DelegateCommand _newCommand;
        private DelegateCommand _editCommand;
        //private DelegateCommand _

        public UserAccountListViewModel()
        {
            UserAccounts = new ObservableCollection<UserAccountViewModel>();
        }

        public ObservableCollection<UserAccountViewModel> UserAccounts
        {
            get { return _userAccounts; }
            set
            {
                if (Equals(value, _userAccounts)) return;
                _userAccounts = value;
                RaisePropertyChanged(() => UserAccounts);
            }
        }

        public UserAccountViewModel SelectedUserAccount
        {
            get { return _selectedUserAccount; }
            set
            {
                if (Equals(value, _selectedUserAccount)) return;
                _selectedUserAccount = value;
                RaisePropertyChanged(() => SelectedUserAccount);
            }
        }

        public ICommand NewCommand
        {
            get { return _newCommand ?? (_newCommand = new DelegateCommand(NewHandler)); }
        }
        private void NewHandler()
        {
            var userAccount = new UserAccount();
            var viewModel = new UserAccountViewModel(userAccount);

            RaiseNewOrEditStarted(viewModel);
        }

        public event Action<UserAccountViewModel> NewOrEditStarted;

        private void RaiseNewOrEditStarted(UserAccountViewModel viewModel)
        {
            var handler = NewOrEditStarted;
            if (handler != null)
                handler(viewModel);
        }
    }
}
