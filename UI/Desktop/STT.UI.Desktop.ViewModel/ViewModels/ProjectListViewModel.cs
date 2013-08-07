using STT.Data;
using STT.Model.Entity;

namespace STT.UI.Desktop.ViewModel
{
    public class ProjectListViewModel : ListViewModel<Project, ProjectViewModel>
    {
        private readonly IProjectRepository _repository;

        public ProjectListViewModel(IRepositoryFactory repositoryFactory)
            : base(repositoryFactory)
        {
            _repository = repositoryFactory.GetProjectRepository();

            foreach (var item in _repository.Get())
            {
                Items.Add(new ProjectViewModel(item, repositoryFactory));
            }
        }

        protected override void NewHandler()
        {
            var model = new Project();
            var viewModel = new ProjectViewModel(model, RepositoryFactory);
            viewModel.ModelSaved += ViewModelOnModelSaved;

            RaiseNewOrEditStarted(viewModel, DataCommand.New);
        }

        private void ViewModelOnModelSaved(ViewModelBase<Project> viewModelBase)
        {
            var viewModel = viewModelBase as ProjectViewModel;
            if (viewModel != null)
            {
                viewModel.ModelSaved -= ViewModelOnModelSaved;
                Items.Add(viewModel);
            }
        }
    }
}
