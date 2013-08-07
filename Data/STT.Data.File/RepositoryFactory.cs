using System;
using System.IO;
using STT.Model.Entity;

namespace STT.Data.File
{
    public class RepositoryFactory : IRepositoryFactory
    {
        private readonly string _dataFolder;

        private IUserAccountRepository _userAccountRepository;
        private IWorkItemRepository _workItemRepository;
        private IWorkItemTypeRepository _workItemTypeRepository;
        private IProjectRepository _projectRepository;

        public RepositoryFactory()
        {
            _dataFolder = Path.Combine(Environment.CurrentDirectory, "Data");
            if (!Directory.Exists(_dataFolder))
                Directory.CreateDirectory(_dataFolder);
        }

        public IRepositoryBase<TModel> GetRepository<TModel>() where TModel : EntityBase
        {
            object repo = null;

            if (typeof(UserAccount) == typeof(TModel))
                repo = GetUserAccountRepository();
            else if (typeof(WorkItem) == typeof(TModel))
                repo = GetWorkItemRepository();
            else if (typeof(WorkItemType) == typeof(TModel))
                repo = GetWorkItemTypeRepository();
            else if (typeof(Project) == typeof(TModel))
                repo = GetProjectRepository();

            return repo as IRepositoryBase<TModel>;
        }

        public IUserAccountRepository GetUserAccountRepository()
        {
            return _userAccountRepository ?? (_userAccountRepository = new UserAccountRepository(_dataFolder));
        }

        public IWorkItemRepository GetWorkItemRepository()
        {
            return _workItemRepository ?? (_workItemRepository = new WorkItemRepository(_dataFolder));
        }

        public IWorkItemTypeRepository GetWorkItemTypeRepository()
        {
            return _workItemTypeRepository ?? (_workItemTypeRepository = new WorkItemTypeRepository(_dataFolder));
        }

        public IProjectRepository GetProjectRepository()
        {
            return _projectRepository ?? (_projectRepository = new ProjectRepository(_dataFolder, GetWorkItemRepository()));
        }
    }
}
