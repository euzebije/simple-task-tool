using System;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using STT.Data;
using STT.Model.Entity;

namespace STT.UI.Desktop.ViewModel
{
    public class UserAccountListViewModel : ListViewModel<UserAccount, UserAccountViewModel>
    {
        private readonly IUserAccountRepository _repository;

        private DelegateCommand _changePasswordCommand;

        public UserAccountListViewModel(IRepositoryFactory repositoryFactory)
            : base(repositoryFactory)
        {
            _repository = repositoryFactory.GetUserAccountRepository();

            foreach (var item in _repository.Get())
            {
                Items.Add(new UserAccountViewModel(item, repositoryFactory));
            }
        }

        public ICommand ChangePasswordCommand
        {
            get
            {
                return _changePasswordCommand ??
                       (_changePasswordCommand = new DelegateCommand(ChangePasswordHandler, CanChangePasswordHandler));
            }
        }
        private void ChangePasswordHandler()
        {
            RaiseChangePasswordStarted(SelectedItem);
        }
        private bool CanChangePasswordHandler()
        {
            return SelectedItem != null;
        }

        public event Action<UserAccountViewModel> ChangePasswordStarted;

        protected override void NewHandler()
        {
            var model = new UserAccount();
            var viewModel = new UserAccountViewModel(model, RepositoryFactory);
            viewModel.ModelSaved += ViewModelOnModelSaved;

            RaiseNewOrEditStarted(viewModel, DataCommand.New);
        }

        private void ViewModelOnModelSaved(ViewModelBase<UserAccount> viewModelBase)
        {
            var viewModel = viewModelBase as UserAccountViewModel;
            if (viewModel != null)
            {
                viewModel.ModelSaved -= ViewModelOnModelSaved;
                Items.Add(viewModel);
            }
        }

        protected override void OnSelectedItemChanged()
        {
            ((DelegateCommand)ChangePasswordCommand).RaiseCanExecuteChanged();
        }

        protected void RaiseChangePasswordStarted(UserAccountViewModel viewModel)
        {
            var handler = ChangePasswordStarted;
            if (handler != null)
                handler(viewModel);
        }
    }
}
