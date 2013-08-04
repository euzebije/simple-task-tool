using System.Collections.ObjectModel;
using NUnit.Framework;

namespace STT.UI.Desktop.ViewModel.Test
{
    [TestFixture]
    public class WorkItemTypeListViewModelTest
    {
        [Test]
        public void CreateEmptyWorkItemTypeListViewModel()
        {
            var viewModel = new WorkItemTypeListViewModel();
            Assert.That(viewModel, Is.Not.Null);
        }

        [Test]
        public void WorkItemTypeListViewModelInheritsViewModelBase()
        {
            var viewModel = new WorkItemTypeListViewModel();
            Assert.That(viewModel, Is.InstanceOf<ViewModelBase>());
        }

        [Test]
        public void WorkItemTypeListViewModelHasCollectionOfItems()
        {
            var viewModel = new WorkItemTypeListViewModel();

            Assert.That(viewModel.Items, Is.Not.Null);
            Assert.That(viewModel.Items, Is.InstanceOf<ObservableCollection<WorkItemTypeViewModel>>());
        }
    }
}
