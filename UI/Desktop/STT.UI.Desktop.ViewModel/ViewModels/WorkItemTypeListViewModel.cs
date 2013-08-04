using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using STT.Data;
using STT.Model.Entity;

namespace STT.UI.Desktop.ViewModel
{
    public class WorkItemTypeListViewModel : ViewModelBase
    {
        private readonly IRepositoryFactory _factory;
        private readonly IWorkItemTypeRepository _repository;

        private ObservableCollection<WorkItemTypeViewModel> _items;
        private WorkItemTypeViewModel _selectedItem;

        private DelegateCommand _newCommand;
        private DelegateCommand _editCommand;
        private DelegateCommand _deleteCommand;

        public WorkItemTypeListViewModel(IRepositoryFactory repositoryFactory)
        {
            if (repositoryFactory == null)
                throw new ArgumentNullException("repositoryFactory");

            _factory = repositoryFactory;
            _repository = repositoryFactory.GetWorkItemTypeRepository();

            Items = new ObservableCollection<WorkItemTypeViewModel>();
            foreach (var item in _repository.Get())
            {
                Items.Add(new WorkItemTypeViewModel(item, repositoryFactory));
            }
        }

        public ObservableCollection<WorkItemTypeViewModel> Items
        {
            get { return _items; }
            set
            {
                if (Equals(value, _items)) return;
                _items = value;
                RaisePropertyChanged(() => Items);
            }
        }

        public WorkItemTypeViewModel SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                if (Equals(value, _selectedItem)) return;
                _selectedItem = value;
                RaisePropertyChanged(() => SelectedItem);
                
                ((DelegateCommand)EditCommand).RaiseCanExecuteChanged();
                ((DelegateCommand)DeleteCommand).RaiseCanExecuteChanged();
            }
        }

        public ICommand NewCommand
        {
            get { return _newCommand ?? (_newCommand = new DelegateCommand(NewHandler)); }
        }
        private void NewHandler()
        {
            var model = new WorkItemType();
            var viewModel = new WorkItemTypeViewModel(model, _factory);
            viewModel.ModelSaved += ViewModelOnModelSaved;

            RaiseNewOrEditStarted(viewModel, DataCommand.New);
        }

        private void ViewModelOnModelSaved(ViewModelBase<WorkItemType> viewModelBase)
        {
            var viewModel = viewModelBase as WorkItemTypeViewModel;
            if (viewModel != null)
            {
                viewModel.ModelSaved -= ViewModelOnModelSaved;
                Items.Add(viewModel);
            }
        }

        public ICommand EditCommand
        {
            get { return _editCommand ?? (_editCommand = new DelegateCommand(EditHandler, CanEditHandler)); }
        }
        private void EditHandler()
        {
            RaiseNewOrEditStarted(SelectedItem, DataCommand.Edit);
        }
        private bool CanEditHandler()
        {
            return SelectedItem != null;
        }

        public ICommand DeleteCommand
        {
            get { return _deleteCommand ?? (_deleteCommand = new DelegateCommand(DeleteHandler, CanDeleteHandler)); }
        }

        private void DeleteHandler()
        {
            SelectedItem.Delete();
            Items.Remove(SelectedItem);
            SelectedItem = null;
        }
        private bool CanDeleteHandler()
        {
            return SelectedItem != null;
        }

        public event Action<WorkItemTypeViewModel, DataCommand> NewOrEditStarted;

        private void RaiseNewOrEditStarted(WorkItemTypeViewModel viewModel, DataCommand dataCommand)
        {
            var handler = NewOrEditStarted;
            if (handler != null)
                handler(viewModel, dataCommand);
        }
    }
}
