using STT.Data;
using STT.Model.Entity;

namespace STT.UI.Desktop.ViewModel
{
    public class WorkItemTypeViewModel : ViewModelBase<WorkItemType>
    {
        private readonly IWorkItemTypeRepository _repository;

        public WorkItemTypeViewModel(WorkItemType model, IRepositoryFactory repositoryFactory)
            : base(model, repositoryFactory)
        {
            _repository = repositoryFactory.GetWorkItemTypeRepository();
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
    }
}
