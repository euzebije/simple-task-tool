using System.Collections.ObjectModel;

namespace STT.UI.Desktop.ViewModel
{
    public class WorkItemTypeListViewModel : ViewModelBase
    {
        private ObservableCollection<WorkItemTypeViewModel> _items;

        public WorkItemTypeListViewModel()
        {
            Items = new ObservableCollection<WorkItemTypeViewModel>();
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
    }
}
