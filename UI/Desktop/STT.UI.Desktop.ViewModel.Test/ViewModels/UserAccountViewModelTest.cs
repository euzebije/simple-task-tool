using NUnit.Framework;
using STT.Data.Memory;
using STT.Model.Entity;

namespace STT.UI.Desktop.ViewModel.Test
{
    [TestFixture]
    public class UserAccountViewModelTest
    {
        [Test]
        public void CreateEmptyUserAccountViewModel()
        {
            var model = new UserAccount();
            var viewModel = new UserAccountViewModel(model, new RepositoryFactory());
            Assert.That(viewModel, Is.Not.Null);
        }

        [Test]
        public void UserAccountViewModelInheritsViewModelBase()
        {
            var viewModel = new UserAccountViewModel(new UserAccount(), new RepositoryFactory());
            Assert.That(viewModel, Is.InstanceOf<ViewModelBase>());
        }

        [Test]
        public void ViewModelWrapsModel()
        {
            var model = new UserAccount("test", "password", false, false);
            var viewModel = new UserAccountViewModel(model, new RepositoryFactory());

            Assert.That(viewModel.Username, Is.EqualTo(model.Username));
            Assert.That(viewModel.CreatedOn, Is.EqualTo(model.CreatedOn));
            Assert.That(viewModel.LastLogin, Is.EqualTo(model.LastLogin));
            Assert.That(viewModel.IsActive, Is.EqualTo(model.IsActive));
            Assert.That(viewModel.IsPowerUser, Is.EqualTo(model.IsPowerUser));
        }

        [Test]
        public void UserAccountViewModelSaveNew()
        {
            var eventSuccessful = false;

            var factory = new RepositoryFactory();
            var repo = factory.GetUserAccountRepository();

            var model = new UserAccount();
            var viewModel = new UserAccountViewModel(model, factory);
            viewModel.ModelSaved += itemViewModel =>
            {
                eventSuccessful = true;
                Assert.That(itemViewModel, Is.Not.Null);
                Assert.That(itemViewModel, Is.EqualTo(viewModel));

                var fetchedModel = repo.Find(model.Key);
                Assert.That(fetchedModel, Is.Not.Null);
                Assert.That(fetchedModel.Username, Is.EqualTo("test"));
                Assert.That(fetchedModel.IsPasswordValid("password"), Is.True);
                Assert.That(fetchedModel.IsActive, Is.True);
            };

            Assert.That(repo.Find(model.Key), Is.Null);

            viewModel.Username = "test";
            //viewModel.Password = "password";
            viewModel.IsActive = true;
            viewModel.Save();

            if (!eventSuccessful)
                Assert.Fail();
        }

        [Test]
        public void UserAccountViewModelSaveExisting()
        {
            var eventSuccessful = false;

            var factory = new RepositoryFactory();
            var repo = factory.GetUserAccountRepository();

            var model = new UserAccount("test", "password", true, false);
            repo.Save(model);

            var viewModel = new UserAccountViewModel(model, factory);
            viewModel.ModelSaved += itemViewModel =>
            {
                eventSuccessful = true;
                Assert.That(itemViewModel, Is.Not.Null);
                Assert.That(itemViewModel, Is.EqualTo(viewModel));
                Assert.That(itemViewModel.IsInEditMode, Is.False);

                var item = repo.Find(model.Key);
                Assert.That(item.Username, Is.EqualTo("test2"));
                Assert.That(item.IsActive, Is.False);
            };

            viewModel.IsInEditMode = true;
            viewModel.Username = "test2";
            viewModel.IsActive = false;
            viewModel.Save();

            if (!eventSuccessful)
                Assert.Fail();
        }

        [Test]
        public void UserAccountViewModelCancelEdit()
        {
            var factory = new RepositoryFactory();
            var repo = factory.GetUserAccountRepository();

            var model = new UserAccount("test", "password", true, false);
            repo.Save(model);

            var viewModel = new UserAccountViewModel(model, factory);
            viewModel.ModelSaved += itemViewModel => Assert.Fail();

            viewModel.IsInEditMode = true;
            viewModel.Username = "test2";
            viewModel.IsActive = false;
            viewModel.Cancel();

            Assert.That(viewModel.IsInEditMode, Is.False);
            Assert.That(viewModel.Username, Is.EqualTo("test"));
            Assert.That(viewModel.IsActive, Is.True);
        }

        [Test]
        public void UserAccountViewModelChangePassword()
        {
            var eventSuccessful = false;

            var factory = new RepositoryFactory();
            var repo = factory.GetUserAccountRepository();

            var model = new UserAccount("test", "password", true, false);
            repo.Save(model);

            Assert.That(model.IsPasswordValid("password"), Is.True);

            var viewModel = new UserAccountViewModel(model, factory);
            viewModel.ModelSaved += itemViewModel =>
                                        {
                                            eventSuccessful = true;
                                            Assert.That(itemViewModel, Is.Not.Null);
                                            Assert.That(itemViewModel, Is.EqualTo(viewModel));
                                            
                                            Assert.That(model.IsPasswordValid("password"), Is.False);
                                            Assert.That(model.IsPasswordValid("new_password"), Is.True);
                                        };

            Assert.That(viewModel.IsPasswordChangeError, Is.False);
            
            viewModel.ChangePassword();
            Assert.That(viewModel.IsPasswordChangeError, Is.True);

            viewModel.OldPassword = "password";
            viewModel.ChangePassword();
            Assert.That(viewModel.IsPasswordChangeError, Is.True);

            viewModel.NewPassword = "new_password";
            viewModel.ChangePassword();
            Assert.That(viewModel.IsPasswordChangeError, Is.True);

            viewModel.NewPasswordConfirm = "new_password";
            viewModel.ChangePassword();
            Assert.That(viewModel.IsPasswordChangeError, Is.False);

            if (!eventSuccessful)
                Assert.Fail();
        }
    }
}
