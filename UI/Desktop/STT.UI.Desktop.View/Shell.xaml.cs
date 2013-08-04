using STT.UI.Common;
using STT.UI.Desktop.ViewModel;

namespace STT.UI.Desktop.View
{
    /// <summary>
    /// Interaction logic for Shell.xaml
    /// </summary>
    public partial class Shell
    {
        public MainViewModel ViewModel { get; private set; }

        public Shell(MainViewModel viewModel)
        {
            InitializeComponent();

            Title = Localization.ApplicationName;
            DataContext = viewModel;

            ViewModel = viewModel;
        }
    }
}
