using System.Windows;
using STT.Data;
using STT.UI.Desktop.View.Helper;
using STT.UI.Desktop.ViewModel;

namespace STT.UI.Desktop.View
{
    /// <summary>
    /// Interaction logic for WorkItemListView.xaml
    /// </summary>
    public partial class WorkItemListView
    {
        public WorkItemListView()
        {
            InitializeComponent();

            DataContextChanged += OnDataContextChanged;
        }

        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs eventArgs)
        {
            // Deregister old event
            var oldViewModel = eventArgs.OldValue as WorkItemListViewModel;
            if (oldViewModel != null)
            {
                oldViewModel.NewOrEditStarted -= OnNewOrEditStarted;
            }

            // Register new event
            var newViewModel = eventArgs.NewValue as WorkItemListViewModel;
            if (newViewModel != null)
            {
                newViewModel.NewOrEditStarted += OnNewOrEditStarted;
            }
        }

        private void OnNewOrEditStarted(WorkItemViewModel viewModel, DataCommand dataCommand)
        {
            var mainView = VisualTreeHelper.GetParent<Shell>(this);
            var view = new NewEditWorkItem(viewModel, dataCommand, mainView);
            view.ShowDialog();
        }
    }
}
