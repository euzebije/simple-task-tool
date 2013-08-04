using System;
using Microsoft.Practices.Prism.ViewModel;
using STT.Data;
using STT.Model.Entity;

namespace STT.UI.Desktop.ViewModel
{
    public abstract class ViewModelBase : NotificationObject
    {
    }

    public abstract class ViewModelBase<TModel> : ViewModelBase where TModel: EntityBase
    {
        protected TModel Model { get; private set; }
        protected IRepositoryFactory RepositoryFactory { get; private set; }

        protected ViewModelBase(TModel model, IRepositoryFactory repositoryFactory)
        {
            if (model == null)
                throw new ArgumentNullException("model");
            if (repositoryFactory == null)
                throw new ArgumentNullException("repositoryFactory");

            Model = model;
            RepositoryFactory = repositoryFactory;
        }

        public event Action<ViewModelBase<TModel>> ModelSaved;
        public event Action<ViewModelBase<TModel>> ModelDeleted; 

        public virtual void Save()
        {
            var repo = RepositoryFactory.GetRepository<TModel>();
            repo.Save(Model);
            RaiseModelSaved();
        }
        public virtual void Delete()
        {
            var repo = RepositoryFactory.GetRepository<TModel>();
            repo.Delete(Model);
            RaiseModelDeleted();
        }

        protected void RaiseModelSaved()
        {
            var handler = ModelSaved;
            if (handler != null)
                handler(this);
        }
        protected void RaiseModelDeleted()
        {
            var handler = ModelDeleted;
            if (handler != null)
                handler(this);
        }
    }
}
