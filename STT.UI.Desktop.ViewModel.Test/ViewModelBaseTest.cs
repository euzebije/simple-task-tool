using NUnit.Framework;
using STT.UI.Desktop.ViewModel.Test.Mock;

namespace STT.UI.Desktop.ViewModel.Test
{
    [TestFixture]
    public class ViewModelBaseTest
    {
        [Test]
        public void NotifyPropertyChangedImplicit()
        {
            var viewModel = new MockViewModel();
            viewModel.PropertyChanged +=
                (sender, args) => Assert.AreEqual("ImplicitNotificationProperty", args.PropertyName);

            viewModel.ImplicitNotificationProperty = "test";
        }

        [Test]
        public void NotifyPropertyChangedExplicit()
        {
            var viewModel = new MockViewModel();
            viewModel.PropertyChanged +=
                (sender, args) => Assert.AreEqual("ExplicitNotificationProperty", args.PropertyName);

            viewModel.ExplicitNotificationProperty = "test";
        }
    }
}
