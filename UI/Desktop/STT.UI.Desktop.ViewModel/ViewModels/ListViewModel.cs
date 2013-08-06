using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using STT.Data;
using STT.Model.Entity;

namespace STT.UI.Desktop.ViewModel
{
    public abstract class ListViewModel<TModel, TViewModel> : ViewModelBase
        where TModel: EntityBase
        where TViewModel: ViewModelBase<TModel>
    {
        protected IRepositoryFactory RepositoryFactory { get; private set; }

        private ObservableCollection<TViewModel> _items;
        private TViewModel _selectedItem;

        private DelegateCommand _newCommand;
        private DelegateCommand _editCommand;
        private DelegateCommand _deleteCommand;

        protected ListViewModel(IRepositoryFactory repositoryFactory)
        {
            if (repositoryFactory == null)
                throw new ArgumentNullException("repositoryFactory");

            RepositoryFactory = repositoryFactory;

            Items = new ObservableCollection<TViewModel>();
        }

        public ObservableCollection<TViewModel> Items
        {
            get { return _items; }
            set
            {
                if (Equals(value, _items)) return;
                _items = value;
                RaisePropertyChanged(() => Items);
            }
        }

        public TViewModel SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                if (Equals(value, _selectedItem)) return;
                _selectedItem = value;
                RaisePropertyChanged(() => SelectedItem);

                ((DelegateCommand)EditCommand).RaiseCanExecuteChanged();
                ((DelegateCommand)DeleteCommand).RaiseCanExecuteChanged();
                OnSelectedItemChanged();
            }
        }

        public ICommand NewCommand
        {
            get { return _newCommand ?? (_newCommand = new DelegateCommand(NewHandler)); }
        }
        protected abstract void NewHandler();

        public ICommand EditCommand
        {
            get { return _editCommand ?? (_editCommand = new DelegateCommand(EditHandler, CanEditHandler)); }
        }
        protected virtual void EditHandler()
        {
            SelectedItem.IsInEditMode = true;
            RaiseNewOrEditStarted(SelectedItem, DataCommand.Edit);
        }
        protected virtual bool CanEditHandler()
        {
            return SelectedItem != null;
        }

        public ICommand DeleteCommand
        {
            get { return _deleteCommand ?? (_deleteCommand = new DelegateCommand(DeleteHandler, CanDeleteHandler)); }
        }
        protected virtual void DeleteHandler()
        {
            SelectedItem.Delete();
            Items.Remove(SelectedItem);
            SelectedItem = null;
        }
        protected virtual bool CanDeleteHandler()
        {
            return SelectedItem != null;
        }

        public event Action<TViewModel, DataCommand> NewOrEditStarted;

        protected void RaiseNewOrEditStarted(TViewModel viewModel, DataCommand dataCommand)
        {
            var handler = NewOrEditStarted;
            if (handler != null)
                handler(viewModel, dataCommand);
        }

        protected virtual void OnSelectedItemChanged()
        {
        }
    }
}
