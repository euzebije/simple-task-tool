using System.Windows;
using STT.Data;
using STT.UI.Desktop.ViewModel;

namespace STT.UI.Desktop.View
{
    /// <summary>
    /// Interaction logic for NewEditWorkItem.xaml
    /// </summary>
    public partial class NewEditWorkItem
    {
        private readonly WorkItemViewModel _viewModel;

        public NewEditWorkItem(WorkItemViewModel viewModel, DataCommand dataCommand, Window owner)
        {
            InitializeComponent();

            _viewModel = viewModel;

            DataContext = viewModel;
            Owner = owner;

            Title = dataCommand == DataCommand.New
                        ? Common.Localization.NewWorkItem
                        : string.Format(Common.Localization.EditWorkItem, viewModel.Title);

            TitleText.Focus();
        }

        private void SaveClick(object sender, RoutedEventArgs e)
        {
            _viewModel.Save();
            if (_viewModel.IsValid)
                Close();
        }

        private void CancelClick(object sender, RoutedEventArgs e)
        {
            _viewModel.Cancel();
            Close();
        }
    }
}
