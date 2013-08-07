using System;
using STT.Data;
using STT.Model.Entity;
using STT.UI.Common;

namespace STT.UI.Desktop.ViewModel
{
    public class UserAccountViewModel : ViewModelBase<UserAccount>
    {
        private string _backupUsername;
        private bool _backupIsActive;

        private string _oldPassword;
        private string _newPassword;
        private string _newPasswordConfirm;
        private string _passwordChangeMessage;
        private bool _isPasswordChangeError;

        private string _password;

        public UserAccountViewModel(UserAccount model, IRepositoryFactory repositoryFactory)
            : base(model, repositoryFactory)
        {
        }

        public string Username
        {
            get { return Model.Username; }
            set {
                if (Model.Username != value)
                {
                    Model.Username = value;
                    RaisePropertyChanged(() => Username);
                }
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
        public DateTime CreatedOn
        {
            get { return Model.CreatedOn; }
        }
        public DateTime LastLogin
        {
            get { return Model.LastLogin; }
        }
        public bool IsActive
        {
            get { return Model.IsActive; }
            set
            {
                if (Model.IsActive != value)
                {
                    Model.IsActive = value;
                    RaisePropertyChanged(() => IsActive);
                }
            }
        }
        public bool IsPowerUser
        {
            get { return Model.IsPowerUser; }
        }

        public string OldPassword
        {
            get { return _oldPassword; }
            set
            {
                if (value == _oldPassword) return;
                _oldPassword = value;
                RaisePropertyChanged(() => OldPassword);
            }
        }
        public string NewPassword
        {
            get { return _newPassword; }
            set
            {
                if (value == _newPassword) return;
                _newPassword = value;
                RaisePropertyChanged(() => NewPassword);
            }
        }
        public string NewPasswordConfirm
        {
            get { return _newPasswordConfirm; }
            set
            {
                if (value == _newPasswordConfirm) return;
                _newPasswordConfirm = value;
                RaisePropertyChanged(() => NewPasswordConfirm);
            }
        }

        public bool IsPasswordChangeError
        {
            get { return _isPasswordChangeError; }
            set
            {
                if (value.Equals(_isPasswordChangeError)) return;
                _isPasswordChangeError = value;
                RaisePropertyChanged(() => IsPasswordChangeError);
            }
        }
        public string PasswordChangeMessage
        {
            get { return _passwordChangeMessage; }
            set
            {
                if (value == _passwordChangeMessage) return;
                _passwordChangeMessage = value;
                RaisePropertyChanged(() => PasswordChangeMessage);
            }
        }

        public override void Save()
        {
            if (Model.IsPasswordValid("temp") && !string.IsNullOrWhiteSpace(Password))
                Model.ChangePassword("temp", Password);

            base.Save();
        }

        protected override void StartEditMode()
        {
            _backupUsername = Username;
            _backupIsActive = IsActive;
        }

        protected override void SubmitEdit()
        {
            _backupUsername = null;
            _backupIsActive = false;
        }

        protected override void RevertEdit()
        {
            Username = _backupUsername;
            IsActive = _backupIsActive;
        }

        protected override bool Validate()
        {
            if (string.IsNullOrWhiteSpace(Username))
            {
                ValidationMessage = string.Format(Localization.ValidationRequired, Localization.Username);
                return false;
            }

            if (Model.IsPasswordValid("temp") && string.IsNullOrWhiteSpace(Password))
            {
                ValidationMessage = string.Format(Localization.ValidationRequired, Localization.Password);
                return false;
            }

            ValidationMessage = null;
            return true;
        }

        public void ChangePassword()
        {
            IsPasswordChangeError = false;

            if (string.IsNullOrWhiteSpace(OldPassword))
            {
                IsPasswordChangeError = true;
                PasswordChangeMessage = Localization.Password_OldPasswordNull;
            }
            else if (string.IsNullOrWhiteSpace(NewPassword))
            {
                IsPasswordChangeError = true;
                PasswordChangeMessage = Localization.Password_NewPasswordNull;
            }
            else if (!NewPassword.Equals(NewPasswordConfirm))
            {
                IsPasswordChangeError = true;
                PasswordChangeMessage = Localization.Password_PasswordDoesNotMatch;
            }
            else if (!Model.IsPasswordValid(OldPassword))
            {
                IsPasswordChangeError = true;
                PasswordChangeMessage = Localization.Password_OldPasswordInvalid;
            }

            if (!IsPasswordChangeError)
            {
                Model.ChangePassword(OldPassword, NewPassword);
                Save();
            }
        }
    }
}
