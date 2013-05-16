using STT.Model.Entity;

namespace STT.Data.Memory
{
    public class RepositoryFactory : IRepositoryFactory
    {
        private IUserAccountRepository _userAccountRepository;
        private IWorkItemRepository _workItemRepository;
        private IWorkItemTypeRepository _workItemTypeRepository;
        private IProjectRepository _projectRepository;

        public IRepositoryBase<TModel> GetRepository<TModel>() where TModel : EntityBase
        {
            object repo = null;

            if (typeof(UserAccount) == typeof(TModel))
                repo = GetUserAccountRepository();
            else if (typeof(WorkItem) == typeof(TModel))
                repo = GetWorkItemRepository();
            else if (typeof(WorkItemType) == typeof(TModel))
                repo = GetWorkItemTypeRepository();
            else if (typeof (Project) == typeof (TModel))
                repo = GetProjectRepository();

            return repo as IRepositoryBase<TModel>;
        }

        public IUserAccountRepository GetUserAccountRepository()
        {
            return _userAccountRepository ?? (_userAccountRepository = new UserAccountRepository());
        }

        public IWorkItemRepository GetWorkItemRepository()
        {
            return _workItemRepository ?? (_workItemRepository = new WorkItemRepository());
        }

        public IWorkItemTypeRepository GetWorkItemTypeRepository()
        {
            return _workItemTypeRepository ?? (_workItemTypeRepository = new WorkItemTypeRepository());
        }

        public IProjectRepository GetProjectRepository()
        {
            return _projectRepository ?? (_projectRepository = new ProjectRepository());
        }
    }
}
