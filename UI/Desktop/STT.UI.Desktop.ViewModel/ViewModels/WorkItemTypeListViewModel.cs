using STT.Data;
using STT.Model.Entity;

namespace STT.UI.Desktop.ViewModel
{
    public class WorkItemTypeListViewModel : ListViewModel<WorkItemType, WorkItemTypeViewModel>
    {
        private readonly IWorkItemTypeRepository _repository;

        public WorkItemTypeListViewModel(IRepositoryFactory repositoryFactory)
            : base(repositoryFactory)
        {
            _repository = repositoryFactory.GetWorkItemTypeRepository();

            foreach (var item in _repository.Get())
            {
                Items.Add(new WorkItemTypeViewModel(item, repositoryFactory));
            }
        }

        protected override void NewHandler()
        {
            var model = new WorkItemType();
            var viewModel = new WorkItemTypeViewModel(model, RepositoryFactory);
            viewModel.ModelSaved += ViewModelOnModelSaved;

            RaiseNewOrEditStarted(viewModel, DataCommand.New);
        }

        private void ViewModelOnModelSaved(ViewModelBase<WorkItemType> viewModelBase)
        {
            var viewModel = viewModelBase as WorkItemTypeViewModel;
            if (viewModel != null)
            {
                viewModel.ModelSaved -= ViewModelOnModelSaved;
                Items.Add(viewModel);
            }
        }
    }
}
