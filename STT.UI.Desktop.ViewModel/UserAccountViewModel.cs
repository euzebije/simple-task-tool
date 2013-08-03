using System;
using STT.Model.Entity;

namespace STT.UI.Desktop.ViewModel
{
    public class UserAccountViewModel : ViewModelBase<UserAccount>
    {
        public UserAccountViewModel(UserAccount model)
            : base(model)
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
        }
        public bool IsPowerUser
        {
            get { return Model.IsPowerUser; }
        }
    }
}
