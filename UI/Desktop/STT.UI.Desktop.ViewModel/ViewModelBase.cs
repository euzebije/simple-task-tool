using Microsoft.Practices.Prism.ViewModel;
using STT.Model.Entity;

namespace STT.UI.Desktop.ViewModel
{
    public abstract class ViewModelBase : NotificationObject
    {
    }

    public abstract class ViewModelBase<TModel> : ViewModelBase where TModel: EntityBase
    {
        protected TModel Model { get; private set; }

        protected ViewModelBase(TModel model)
        {
            Model = model;
        }
    }
}
