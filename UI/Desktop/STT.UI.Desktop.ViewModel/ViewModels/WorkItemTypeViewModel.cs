using STT.Data;
using STT.Model.Entity;

namespace STT.UI.Desktop.ViewModel
{
    public class WorkItemTypeViewModel : ViewModelBase<WorkItemType>
    {
        private string _backupName;
        private string _backupDescription;

        public WorkItemTypeViewModel(WorkItemType model, IRepositoryFactory repositoryFactory)
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

        protected override void StartEditMode()
        {
            _backupName = Name;
            _backupDescription = Description;
        }

        protected override void SubmitEdit()
        {
            _backupName = null;
            _backupDescription = null;
        }

        protected override void RevertEdit()
        {
            Name = _backupName;
            Description = _backupDescription;
        }
    }
}
