using STT.Data;
using STT.Model.Entity;
using STT.UI.Desktop.Common;

namespace STT.UI.Desktop.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public bool IsAdmin
        {
            get { return AppSession.LoggedInUser != null && AppSession.LoggedInUser.IsPowerUser; }
        }

        public ProjectListViewModel ProjectList { get; private set; }
        public WorkItemListViewModel WorkItemList { get; private set; }
        public WorkItemTypeListViewModel WorkItemTypeList { get; private set; }
        public UserAccountListViewModel UserAccountList { get; private set; }

        public MainViewModel(IRepositoryFactory repositoryFactory)
        {
            ProjectList = new ProjectListViewModel(repositoryFactory);
            WorkItemList = new WorkItemListViewModel(repositoryFactory);
            WorkItemTypeList = new WorkItemTypeListViewModel(repositoryFactory);
            UserAccountList = new UserAccountListViewModel(repositoryFactory);

            AppSession.LoggedInUserChanged += OnLoggedInUserChanged;
        }

        private void OnLoggedInUserChanged(UserAccount userAccount)
        {
            RaisePropertyChanged(() => IsAdmin);
        }
    }
}
