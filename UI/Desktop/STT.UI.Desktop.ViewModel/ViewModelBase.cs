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
        private bool _isInEditMode;

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

        public bool IsInEditMode
        {
            get { return _isInEditMode; }
            set
            {
                if (value.Equals(_isInEditMode)) return;
                _isInEditMode = value;
                RaisePropertyChanged(() => IsInEditMode);

                if (IsInEditMode)
                    StartEditMode();
            }
        }

        public virtual void Save()
        {
            var repo = RepositoryFactory.GetRepository<TModel>();
            repo.Save(Model);
            SubmitEdit();
            IsInEditMode = false;
            RaiseModelSaved();
        }
        public virtual void Delete()
        {
            var repo = RepositoryFactory.GetRepository<TModel>();
            repo.Delete(Model);
            RaiseModelDeleted();
        }
        public virtual void Cancel()
        {
            RevertEdit();
            IsInEditMode = false;
        }

        protected abstract void StartEditMode();
        protected abstract void SubmitEdit();
        protected abstract void RevertEdit();

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
