using NUnit.Framework;
using STT.UI.Desktop.ViewModel.Test.Mock;

namespace STT.UI.Desktop.ViewModel.Test
{
    [TestFixture]
    public class ViewModelBaseTest
    {
        [Test]
        public void NotifyPropertyChangedExpression()
        {
            var viewModel = new MockViewModel();
            viewModel.PropertyChanged +=
                (sender, args) => Assert.That(args.PropertyName, Is.EqualTo("ExpressionNotificationProperty"));

            viewModel.ExpressionNotificationProperty = "test";
        }

        [Test]
        public void NotifyPropertyChangedExplicit()
        {
            var viewModel = new MockViewModel();
            viewModel.PropertyChanged +=
                (sender, args) => Assert.That(args.PropertyName, Is.EqualTo("StringNotificationProperty"));

            viewModel.StringNotificationProperty = "test";
        }
    }
}
