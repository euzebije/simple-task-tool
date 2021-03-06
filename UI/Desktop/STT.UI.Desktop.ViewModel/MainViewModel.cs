﻿using STT.Data;
using STT.Model.Entity;

namespace STT.UI.Desktop.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private UserAccount _loggedInUser;
        public UserAccount LoggedInUser
        {
            get { return _loggedInUser; }
            set
            {
                if (Equals(value, _loggedInUser)) return;
                _loggedInUser = value;
                RaisePropertyChanged(() => LoggedInUser);
                RaisePropertyChanged(() => IsAdmin);
            }
        }

        public bool IsAdmin
        {
            get
            {
                return LoggedInUser != null && LoggedInUser.IsPowerUser;
            }
        }

        public WorkItemTypeListViewModel WorkItemTypeList { get; private set; }

        public MainViewModel(IRepositoryFactory repositoryFactory)
        {
            WorkItemTypeList = new WorkItemTypeListViewModel(repositoryFactory);
        }
    }
}
