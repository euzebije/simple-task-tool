using System.Windows;
using STT.Data;
using STT.Data.Memory;
using STT.UI.Desktop.Common;
using STT.UI.Desktop.View;
using STT.UI.Desktop.View.Security;
using STT.UI.Desktop.ViewModel;
using STT.UI.Desktop.ViewModel.Security;

namespace STT.UI.Desktop.App
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            Container.RegisterType<IRepositoryFactory, RepositoryFactory>();
            Container.RegisterType<MainViewModel>();
            Container.RegisterType<LoginViewModel>();

            var shell = Container.Resolve<Shell>();
            MainWindow = shell;

            // Login
            var loginView = Container.Resolve<LoginView>();
            var loginSuccessful = loginView.ShowDialog();
            
            if (loginSuccessful == true)
            {
                shell.ViewModel.LoggedInUser = loginView.UserAccount;
                shell.Show();
            }
            else
            {
                Shutdown();
            }
        }
    }
}
