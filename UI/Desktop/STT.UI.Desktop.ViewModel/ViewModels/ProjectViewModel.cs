using System;
using System.Collections.Generic;
using STT.Data;
using STT.Model.Entity;

namespace STT.UI.Desktop.ViewModel
{
    public class ProjectViewModel : ViewModelBase<Project>
    {
        private string _backupName;
        private string _backupDescription;
        private UserAccount _backupOwner;

        public ProjectViewModel(Project model, IRepositoryFactory repositoryFactory)
            : base(model, repositoryFactory)
        {
        }

        public string Name
        {
            get { return Model.Name; }
            set
            {
                if (Model.Name != value)
                {
                    Model.Name = value;
                    RaisePropertyChanged(() => Name);
                }
            }
        }
        public string Description
        {
            get { return Model.Description; }
            set
            {
                if (Model.Description != value)
                {
                    Model.Description = value;
                    RaisePropertyChanged(() => Description);
                }
            }
        }
        public DateTime CreatedOn
        {
            get { return Model.CreatedOn; }
        }
        public UserAccount Owner
        {
            get { return Model.Owner; }
            set
            {
                if (Model.Owner != value)
                {
                    Model.Owner = value;
                    RaisePropertyChanged(() => Owner);
                }
            }
        }
        public ICollection<WorkItem> WorkItems
        {
            get { return Model.WorkItems; }
        }

        public IEnumerable<UserAccount> OwnersList
        {
            get { return RepositoryFactory.GetUserAccountRepository().Get(); }
        }

        protected override void StartEditMode()
        {
            _backupName = Name;
            _backupDescription = Description;
            _backupOwner = Owner;
        }

        protected override void SubmitEdit()
        {
            _backupName = null;
            _backupDescription = null;
            _backupOwner = null;
        }

        protected override void RevertEdit()
        {
            Name = _backupName;
            Description = _backupDescription;
            Owner = _backupOwner;
        }
    }
}
