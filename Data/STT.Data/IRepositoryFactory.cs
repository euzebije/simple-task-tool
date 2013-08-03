using STT.Model.Entity;

namespace STT.Data
{
    public interface IRepositoryFactory
    {
        IRepositoryBase<TModel> GetRepository<TModel>() where TModel : EntityBase;

        IUserAccountRepository GetUserAccountRepository();
        IWorkItemRepository GetWorkItemRepository();
        IWorkItemTypeRepository GetWorkItemTypeRepository();
        IProjectRepository GetProjectRepository();
    }
}
