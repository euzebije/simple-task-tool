using System;
using System.Linq;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using STT.Data;
using STT.Model.Entity;

namespace STT.UI.Desktop.ViewModel.Security
{
    public class LoginViewModel : ViewModelBase
    {
        private readonly IUserAccountRepository _repository;

        private string _username;
        private string _password;

        private DelegateCommand _loginCommand;
        private DelegateCommand _cancelCommand;

        public LoginViewModel(IRepositoryFactory repositoryFactory)
        {
            _repository = repositoryFactory.GetUserAccountRepository();
        }

        public string Username
        {
            get { return _username; }
            set
            {
                if (value == _username) return;
                _username = value;
                RaisePropertyChanged(() => Username);
            }
        }
        public string Password
        {
            get { return _password; }
            set
            {
                if (value == _password) return;
                _password = value;
                RaisePropertyChanged(() => Password);
            }
        }
        
        public ICommand LoginCommand
        {
            get
            {
                return _loginCommand ??
                       (_loginCommand = new DelegateCommand(LoginHandler));
            }
        }

        private void LoginHandler()
        {
            if (!string.IsNullOrWhiteSpace(Username) && !string.IsNullOrWhiteSpace(Password))
            {
                var userAccount = _repository.Get().SingleOrDefault(x => x.Username.Equals(Username));
                if (userAccount != null)
                {
                    var success = userAccount.IsActive && userAccount.IsPasswordValid(Password);
                    if (success)
                        RaiseLoginEnded(true, userAccount);
                }
            }
        }

        public ICommand CancelCommand
        {
            get { return _cancelCommand ?? (_cancelCommand = new DelegateCommand(CancelHandler)); }
        }
        private void CancelHandler()
        {
            RaiseLoginEnded(false, null);
        }

        public event Action<bool, UserAccount> LoginEnded;

        private void RaiseLoginEnded(bool success, UserAccount userAccount)
        {
            var handler = LoginEnded;
            if (handler != null)
                handler(success, userAccount);
        }
    }
}
