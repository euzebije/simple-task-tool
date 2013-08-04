using NUnit.Framework;
using STT.Model.Entity;

namespace STT.UI.Desktop.ViewModel.Test
{
    [TestFixture]
    public class WorkItemTypeViewModelTest
    {
        [Test]
        public void CreateEmptyWorkItemTypeViewModel()
        {
            var model = new WorkItemType();
            var viewModel = new WorkItemTypeViewModel(model);
            Assert.That(viewModel, Is.Not.Null);
        }

        [Test]
        public void WorkItemTypeViewModelInheritsViewModelBase()
        {
            var viewModel = new WorkItemTypeViewModel(new WorkItemType());
            Assert.That(viewModel, Is.InstanceOf<ViewModelBase>());
        }

        [Test]
        public void ViewModelWrapsModel()
        {
            var model = new WorkItemType("test", "description");
            var viewModel = new WorkItemTypeViewModel(model);

            Assert.That(viewModel.Name, Is.EqualTo(model.Name));
            Assert.That(viewModel.Description, Is.EqualTo(model.Description));
        }
    }
}
