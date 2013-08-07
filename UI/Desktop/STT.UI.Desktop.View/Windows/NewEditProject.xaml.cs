using System.Windows;
using STT.Data;
using STT.UI.Desktop.ViewModel;

namespace STT.UI.Desktop.View
{
    /// <summary>
    /// Interaction logic for NewEditProject.xaml
    /// </summary>
    public partial class NewEditProject
    {
        private readonly ProjectViewModel _viewModel;

        public NewEditProject(ProjectViewModel viewModel, DataCommand dataCommand, Window owner)
        {
            InitializeComponent();

            _viewModel = viewModel;

            DataContext = viewModel;
            Owner = owner;

            Title = dataCommand == DataCommand.New
                        ? Common.Localization.NewProject
                        : string.Format(Common.Localization.EditProject, viewModel.Name);

            NameText.Focus();
        }

        private void SaveClick(object sender, RoutedEventArgs e)
        {
            _viewModel.Save();
            Close();
        }

        private void CancelClick(object sender, RoutedEventArgs e)
        {
            _viewModel.Cancel();
            Close();
        }
    }
}
