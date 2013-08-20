using System.Collections.ObjectModel;
using System.Linq;
using NUnit.Framework;
using STT.Data;
using STT.Data.Memory;
using STT.Model.Entity;
using STT.UI.Desktop.Common;

namespace STT.UI.Desktop.ViewModel.Test
{
    [TestFixture]
    public class WorkItemListViewModelTest
    {
        [TestFixtureSetUp]
        public void Setup()
        {
            AppSession.SetLoggedInUser(new UserAccount());
        }

        [Test]
        public void CreateEmpty()
        {
            var viewModel = new WorkItemListViewModel(new RepositoryFactory());
            Assert.That(viewModel, Is.Not.Null);
        }

        [Test]
        public void InheritsViewModelBase()
        {
            var viewModel = new WorkItemListViewModel(new RepositoryFactory());
            Assert.That(viewModel, Is.InstanceOf<ViewModelBase>());
        }

        [Test]
        public void HasCollectionOfItems()
        {
            var viewModel = new WorkItemListViewModel(new RepositoryFactory());

            Assert.That(viewModel.Items, Is.Not.Null);
            Assert.That(viewModel.Items, Is.InstanceOf<ObservableCollection<WorkItemViewModel>>());
        }

        [Test]
        public void AddNewItem()
        {
            var eventSuccessful = false;

            var viewModel = new WorkItemListViewModel(new RepositoryFactory());
            Assert.That(viewModel.Items, Is.Empty);

            viewModel.NewOrEditStarted += (itemViewModel, dataCommand) =>
            {
                eventSuccessful = true;
                Assert.That(itemViewModel, Is.Not.Null);
                Assert.That(dataCommand, Is.EqualTo(DataCommand.New));
                itemViewModel.Title = "test";
                itemViewModel.Type = new WorkItemType();
                itemViewModel.Project = new Project();
                itemViewModel.Save();
                Assert.That(viewModel.Items, Is.Not.Empty);
            };

            viewModel.NewCommand.Execute(null);

            if (!eventSuccessful)
                Assert.Fail();
        }

        [Test]
        public void EditItem()
        {
            var eventSuccessful = false;

            var viewModel = new WorkItemListViewModel(new RepositoryFactory());
            viewModel.NewOrEditStarted += (itemViewModel, dataCommand) =>
            {
                eventSuccessful = true;
                Assert.That(itemViewModel, Is.Not.Null);
                Assert.That(itemViewModel, Is.EqualTo(viewModel.SelectedItem));
                Assert.That(dataCommand, Is.EqualTo(DataCommand.Edit));
            };

            Assert.That(viewModel.EditCommand.CanExecute(null), Is.False);

            viewModel.SelectedItem = new WorkItemViewModel(new WorkItem(), new RepositoryFactory());
            Assert.That(viewModel.EditCommand.CanExecute(null), Is.True);

            viewModel.EditCommand.Execute(null);

            if (!eventSuccessful)
                Assert.Fail();
        }

        [Test]
        public void DeleteItem()
        {
            var factory = new RepositoryFactory();
            var repo = factory.GetWorkItemRepository();

            var item = new WorkItem();
            repo.Save(item);

            Assert.That(repo.Find(item.Key), Is.Not.Null);

            var viewModel = new WorkItemListViewModel(factory);

            Assert.That(viewModel.DeleteCommand.CanExecute(null), Is.False);

            viewModel.SelectedItem = viewModel.Items.First();
            Assert.That(viewModel.DeleteCommand.CanExecute(null), Is.True);

            viewModel.DeleteCommand.Execute(null);

            Assert.That(viewModel.SelectedItem, Is.Null);
            Assert.That(viewModel.Items, Is.Empty);
            Assert.That(repo.Find(item.Key), Is.Null);
        }
    }
}
