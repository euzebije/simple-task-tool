using System.ComponentModel;
using System.Runtime.CompilerServices;
using STT.Model.Entity;
using STT.UI.Desktop.ViewModel.Annotations;

namespace STT.UI.Desktop.ViewModel
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) 
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
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
