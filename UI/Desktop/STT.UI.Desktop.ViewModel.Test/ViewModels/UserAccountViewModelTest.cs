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
    }
}
