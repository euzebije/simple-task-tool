using System.Windows;
using STT.Data;
using STT.UI.Desktop.View.Helper;
using STT.UI.Desktop.ViewModel;

namespace STT.UI.Desktop.View
{
    /// <summary>
    /// Interaction logic for WorkItemTypeListView.xaml
    /// </summary>
    public partial class WorkItemTypeListView
    {
        public WorkItemTypeListView()
        {
            InitializeComponent();

            DataContextChanged += OnDataContextChanged;
        }

        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs eventArgs)
        {
            // Deregister old event
            var oldViewModel = eventArgs.OldValue as WorkItemTypeListViewModel;
            if (oldViewModel != null)
            {
                oldViewModel.NewOrEditStarted -= OnNewOrEditStarted;
            }

            // Register new event
            var newViewModel = eventArgs.NewValue as WorkItemTypeListViewModel;
            if (newViewModel != null)
            {
                newViewModel.NewOrEditStarted += OnNewOrEditStarted;
            }
        }

        private void OnNewOrEditStarted(WorkItemTypeViewModel viewModel, DataCommand dataCommand)
        {
            var mainView = VisualTreeHelper.GetParent<Shell>(this);
            var view = new NewEditWorkItemType(viewModel, dataCommand, mainView);
            view.ShowDialog();
        }
    }
}
