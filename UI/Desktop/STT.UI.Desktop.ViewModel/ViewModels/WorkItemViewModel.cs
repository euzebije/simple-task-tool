using System;
using System.Collections.Generic;
using STT.Data;
using STT.Model.Entity;
using STT.UI.Common;
using STT.UI.Desktop.Common;

namespace STT.UI.Desktop.ViewModel
{
    public class WorkItemViewModel : ViewModelBase<WorkItem>
    {
        private string _backupTitle;
        private string _backupDescription;
        private UserAccount _backupAssignedTo;
        private Priority _backupPriority;
        private WorkItemType _backupType;
        private Project _backupProject;
        private bool _backupIsFinished;

        public WorkItemViewModel(WorkItem model, IRepositoryFactory repositoryFactory)
            : base(model, repositoryFactory)
        {
        }

        public string Title
        {
            get { return Model.Title; }
            set
            {
                if (Model.Title != value)
                {
                    Model.Title = value;
                    RaisePropertyChanged(() => Title);
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
        public UserAccount CreatedBy
        {
            get { return Model.CreatedBy; }
        }
        public DateTime CreatedOn
        {
            get { return Model.CreatedOn; }
        }
        public UserAccount AssignedTo
        {
            get { return Model.AssignedTo; }
            set
            {
                if (!Equals(Model.AssignedTo, value))
                {
                    Model.AssignedTo = value;
                    RaisePropertyChanged(() => AssignedTo);
                }
            }
        }
        public DateTime LastUpdate
        {
            get { return Model.LastUpdate; }
        }
        public Priority Priority
        {
            get { return Model.Priority; }
            set
            {
                if (Model.Priority != value)
                {
                    Model.Priority = value;
                    RaisePropertyChanged(() => Priority);
                }
            }
        }
        public WorkItemType Type
        {
            get { return Model.Type; }
            set
            {
                if (!Equals(Model.Type, value))
                {
                    Model.Type = value;
                    RaisePropertyChanged(() => Type);
                }
            }
        }
        public Project Project
        {
            get { return Model.Project; }
            set
            {
                if (!Equals(Model.Project, value))
                {
                    Model.Project = value;
                    RaisePropertyChanged(() => Project);
                }
            }
        }
        public bool IsFinished
        {
            get { return Model.IsFinished; }
            set
            {
                if (Model.IsFinished != value)
                {
                    Model.IsFinished = value;
                    RaisePropertyChanged(() => IsFinished);
                }
            }
        }

        public IEnumerable<UserAccount> AssignedToList
        {
            get { return RepositoryFactory.GetUserAccountRepository().Get(); }
        }
        public IEnumerable<Priority> PriorityList
        {
            get { return new[] {Priority.Low, Priority.Normal, Priority.High}; }
        }
        public IEnumerable<WorkItemType> TypeList
        {
            get { return RepositoryFactory.GetWorkItemTypeRepository().Get(); }
        }
        public IEnumerable<Project> ProjectList
        {
            get { return RepositoryFactory.GetProjectRepository().Get(); }
        }

        public override void Save()
        {
            Model.LastUpdate = DateTime.Now;
            if (Model.CreatedBy == null)
                Model.CreatedBy = AppSession.LoggedInUser;

            base.Save();
        }

        protected override void StartEditMode()
        {
            _backupTitle = Title;
            _backupDescription = Description;
            _backupAssignedTo = AssignedTo;
            _backupPriority = Priority;
            _backupType = Type;
            _backupProject = Project;
            _backupIsFinished = IsFinished;
        }

        protected override void SubmitEdit()
        {
            _backupTitle = null;
            _backupDescription = null;
            _backupAssignedTo = null;
            _backupPriority = Priority.Low;
            _backupType = null;
            _backupProject = null;
            _backupIsFinished = false;
        }

        protected override void RevertEdit()
        {
            Title = _backupTitle;
            Description = _backupDescription;
            AssignedTo = _backupAssignedTo;
            Priority = _backupPriority;
            Type = _backupType;
            Project = _backupProject;
            IsFinished = _backupIsFinished;
        }

        protected override bool Validate()
        {
            if (string.IsNullOrWhiteSpace(Title))
            {
                ValidationMessage = string.Format(Localization.ValidationRequired, Localization.Title);
                return false;
            }
            
            if (Type == null)
            {
                ValidationMessage = string.Format(Localization.ValidationRequired, Localization.Type);
                return false;
            }

            if (Project == null)
            {
                ValidationMessage = string.Format(Localization.ValidationRequired, Localization.Project);
                return false;
            }

            ValidationMessage = null;
            return true;
        }
    }
}
