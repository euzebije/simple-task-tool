using System;
using System.Windows;
using STT.Data;
using STT.UI.Desktop.ViewModel;
using VisualTreeHelper = STT.UI.Desktop.View.Helper.VisualTreeHelper;

namespace STT.UI.Desktop.View
{
    /// <summary>
    /// Interaction logic for UserAccountListView.xaml
    /// </summary>
    public partial class UserAccountListView
    {
        public UserAccountListView()
        {
            InitializeComponent();

            DataContextChanged += OnDataContextChanged;
        }

        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs eventArgs)
        {
            // Deregister old event
            var oldViewModel = eventArgs.OldValue as UserAccountListViewModel;
            if (oldViewModel != null)
            {
                oldViewModel.NewOrEditStarted -= OnNewOrEditStarted;
                oldViewModel.ChangePasswordStarted -= OnChangePasswordStarted;
            }

            // Register new event
            var newViewModel = eventArgs.NewValue as UserAccountListViewModel;
            if (newViewModel != null)
            {
                newViewModel.NewOrEditStarted += OnNewOrEditStarted;
                newViewModel.ChangePasswordStarted += OnChangePasswordStarted;
            }
        }

        private void OnNewOrEditStarted(UserAccountViewModel viewModel, DataCommand dataCommand)
        {
            var mainView = VisualTreeHelper.GetParent<Shell>(this);
            var view = new NewEditUserAccount(viewModel, dataCommand, mainView);
            view.ShowDialog();
        }

        private void OnChangePasswordStarted(UserAccountViewModel viewModel)
        {
            var mainView = VisualTreeHelper.GetParent<Shell>(this);
            var view = new ChangeUserAccountPassword(viewModel, mainView);
            view.ShowDialog();
        }
    }
}
