using NUnit.Framework;
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
            var viewModel = new UserAccountViewModel(model);
            Assert.IsNotNull(viewModel);
        }

        [Test]
        public void UserAccountViewModelInheritsViewModelBase()
        {
            var viewModel = new UserAccountViewModel(new UserAccount());
            Assert.IsInstanceOf<ViewModelBase>(viewModel);
        }

        [Test]
        public void ViewModelWrapsModel()
        {
            var model = new UserAccount("test", false, false);
            var viewModel = new UserAccountViewModel(model);

            Assert.AreEqual(model.Username, viewModel.Username);
            Assert.AreEqual(model.CreatedOn, viewModel.CreatedOn);
            Assert.AreEqual(model.LastLogin, viewModel.LastLogin);
            Assert.AreEqual(model.IsActive, viewModel.IsActive);
            Assert.AreEqual(model.IsPowerUser, viewModel.IsPowerUser);
        }
    }
}
