using System.Windows;
using STT.Data;
using STT.UI.Desktop.View.Helper;
using STT.UI.Desktop.ViewModel;

namespace STT.UI.Desktop.View
{
    /// <summary>
    /// Interaction logic for ProjectListView.xaml
    /// </summary>
    public partial class ProjectListView
    {
        public ProjectListView()
        {
            InitializeComponent();

            DataContextChanged += OnDataContextChanged;
        }

        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs eventArgs)
        {
            // Deregister old event
            var oldViewModel = eventArgs.OldValue as ProjectListViewModel;
            if (oldViewModel != null)
            {
                oldViewModel.NewOrEditStarted -= OnNewOrEditStarted;
            }

            // Register new event
            var newViewModel = eventArgs.NewValue as ProjectListViewModel;
            if (newViewModel != null)
            {
                newViewModel.NewOrEditStarted += OnNewOrEditStarted;
            }
        }

        private void OnNewOrEditStarted(ProjectViewModel viewModel, DataCommand dataCommand)
        {
            var mainView = VisualTreeHelper.GetParent<Shell>(this);
            var view = new NewEditProject(viewModel, dataCommand, mainView);
            view.ShowDialog();
        }
    }
}
