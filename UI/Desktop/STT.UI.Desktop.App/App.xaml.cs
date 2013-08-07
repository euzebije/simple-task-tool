using System.Windows;
using STT.Data;
using STT.Data.File;
using STT.Model.Entity;
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
            var factory = new RepositoryFactory();
            //factory.GetUserAccountRepository().Save(new UserAccount("admin", "pwd", true, true));

            Container.RegisterInstance<IRepositoryFactory>(factory);

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
