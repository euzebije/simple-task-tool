using STT.Data;
using STT.Model.Entity;

namespace STT.UI.Desktop.ViewModel
{
    public class WorkItemListViewModel : ListViewModel<WorkItem, WorkItemViewModel>
    {
        private readonly IWorkItemRepository _repository;

        public WorkItemListViewModel(IRepositoryFactory repositoryFactory)
            : base(repositoryFactory)
        {
            _repository = repositoryFactory.GetWorkItemRepository();

            foreach (var item in _repository.Get())
            {
                Items.Add(new WorkItemViewModel(item, repositoryFactory));
            }
        }

        protected override void NewHandler()
        {
            var model = new WorkItem();
            var viewModel = new WorkItemViewModel(model, RepositoryFactory);
            viewModel.ModelSaved += ViewModelOnModelSaved;

            RaiseNewOrEditStarted(viewModel, DataCommand.New);
        }

        private void ViewModelOnModelSaved(ViewModelBase<WorkItem> viewModelBase)
        {
            var viewModel = viewModelBase as WorkItemViewModel;
            if (viewModel != null)
            {
                viewModel.ModelSaved -= ViewModelOnModelSaved;
                Items.Add(viewModel);
            }
        }
    }
}
