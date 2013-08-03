using NUnit.Framework;
using STT.Data.Memory;
using STT.Model.Entity;
using STT.UI.Desktop.ViewModel.Security;

namespace STT.UI.Desktop.ViewModel.Test.Security
{
    [TestFixture]
    public class LoginViewModelTest
    {
        [Test]
        public void CreateEmptyLoginViewModel()
        {
            var viewModel = new LoginViewModel(new RepositoryFactory());
            Assert.That(viewModel, Is.Not.Null);
        }

        [Test]
        public void LoginViewModelInheritsViewModelBase()
        {
            var viewModel = new LoginViewModel(new RepositoryFactory());
            Assert.That(viewModel, Is.InstanceOf<ViewModelBase>());
        }

        [Test]
        public void LoginViewModelCancelCommandRaisesEvent()
        {
            var eventSuccessful = false;

            var viewModel = new LoginViewModel(new RepositoryFactory());
            viewModel.LoginEnded += (success, user) =>
                                        {
                                            eventSuccessful = !success;
                                            Assert.That(user, Is.Null);
                                        };

            viewModel.CancelCommand.Execute(null);

            if (!eventSuccessful)
                Assert.Fail();
        }

        [Test]
        public void LoginViewModelLoginCommandRaisesEvent()
        {
            var eventSuccessful = false;

            var factory = new RepositoryFactory();
            var repository = factory.GetUserAccountRepository();
            var userAccount = new UserAccount("test", "password", true, false);
            repository.Save(userAccount);

            var viewModel = new LoginViewModel(factory);
            viewModel.LoginEnded += (success, user) =>
                                        {
                                            eventSuccessful = success;
                                            Assert.That(user, Is.EqualTo(userAccount));
                                        };

            viewModel.Username = "test";
            viewModel.Password = "password";

            viewModel.LoginCommand.Execute(null);

            if (!eventSuccessful)
                Assert.Fail();
        }

        [Test]
        public void LoginViewModelLoginInactiveAccount()
        {
            var factory = new RepositoryFactory();
            var repository = factory.GetUserAccountRepository();
            var userAccount = new UserAccount("test", "password", false, false);
            repository.Save(userAccount);

            var viewModel = new LoginViewModel(factory);
            viewModel.LoginEnded += (success, user) => Assert.Fail();

            viewModel.Username = "test";
            viewModel.Password = "password";

            viewModel.LoginCommand.Execute(null);
        }

        [Test]
        public void LoginViewModelLoginInvalidUsername()
        {
            var factory = new RepositoryFactory();
            var repository = factory.GetUserAccountRepository();
            var userAccount = new UserAccount("test", "password", false, false);
            repository.Save(userAccount);

            var viewModel = new LoginViewModel(factory);
            viewModel.LoginEnded += (success, user) => Assert.Fail();

            viewModel.Username = "username";
            viewModel.Password = "password";

            viewModel.LoginCommand.Execute(null);
        }

        [Test]
        public void LoginViewModelLoginInvalidPassword()
        {
            var factory = new RepositoryFactory();
            var repository = factory.GetUserAccountRepository();
            var userAccount = new UserAccount("test", "password", false, false);
            repository.Save(userAccount);

            var viewModel = new LoginViewModel(factory);
            viewModel.LoginEnded += (success, user) => Assert.Fail();

            viewModel.Username = "test";
            viewModel.Password = "wrong password";

            viewModel.LoginCommand.Execute(null);
        }
    }
}
