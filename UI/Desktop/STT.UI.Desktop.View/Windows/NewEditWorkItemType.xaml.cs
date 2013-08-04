using System.Windows;
using STT.Data;
using STT.UI.Desktop.ViewModel;

namespace STT.UI.Desktop.View
{
    /// <summary>
    /// Interaction logic for NewEditWorkItemType.xaml
    /// </summary>
    public partial class NewEditWorkItemType
    {
        private readonly WorkItemTypeViewModel _viewModel;

        public NewEditWorkItemType(WorkItemTypeViewModel viewModel, DataCommand dataCommand, Window owner)
        {
            InitializeComponent();

            _viewModel = viewModel;

            DataContext = viewModel;
            Owner = owner;

            Title = dataCommand == DataCommand.New
                        ? Common.Localization.NewWorkItemType
                        : string.Format(Common.Localization.EditWorkItemType, viewModel.Name);

            NameText.Focus();
        }

        private void SaveClick(object sender, RoutedEventArgs e)
        {
            _viewModel.Save();
            Close();
        }

        private void CancelClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
