using System.Collections.ObjectModel;
using System.Linq;
using NUnit.Framework;
using STT.Data;
using STT.Data.Memory;
using STT.Model.Entity;

namespace STT.UI.Desktop.ViewModel.Test
{
    [TestFixture]
    public class WorkItemTypeListViewModelTest
    {
        [Test]
        public void CreateEmptyWorkItemTypeListViewModel()
        {
            var viewModel = new WorkItemTypeListViewModel(new RepositoryFactory());
            Assert.That(viewModel, Is.Not.Null);
        }

        [Test]
        public void WorkItemTypeListViewModelInheritsViewModelBase()
        {
            var viewModel = new WorkItemTypeListViewModel(new RepositoryFactory());
            Assert.That(viewModel, Is.InstanceOf<ViewModelBase>());
        }

        [Test]
        public void WorkItemTypeListViewModelHasCollectionOfItems()
        {
            var viewModel = new WorkItemTypeListViewModel(new RepositoryFactory());

            Assert.That(viewModel.Items, Is.Not.Null);
            Assert.That(viewModel.Items, Is.InstanceOf<ObservableCollection<WorkItemTypeViewModel>>());
        }

        [Test]
        public void WorkItemTypeListViewModelAddNewItem()
        {
            var eventSuccessful = false;

            var viewModel = new WorkItemTypeListViewModel(new RepositoryFactory());
            Assert.That(viewModel.Items, Is.Empty);

            viewModel.NewOrEditStarted += (itemViewModel, dataCommand) =>
                                              {
                                                  eventSuccessful = true;
                                                  Assert.That(itemViewModel, Is.Not.Null);
                                                  Assert.That(dataCommand, Is.EqualTo(DataCommand.New));
                                                  itemViewModel.Name = "test";
                                                  itemViewModel.Save();
                                                  Assert.That(viewModel.Items, Is.Not.Empty);
                                              };

            viewModel.NewCommand.Execute(null);

            if (!eventSuccessful)
                Assert.Fail();
        }

        [Test]
        public void WorkItemTypeListViewModelEditItem()
        {
            var eventSuccessful = false;

            var viewModel = new WorkItemTypeListViewModel(new RepositoryFactory());
            viewModel.NewOrEditStarted += (itemViewModel, dataCommand) =>
            {
                eventSuccessful = true;
                Assert.That(itemViewModel, Is.Not.Null);
                Assert.That(itemViewModel, Is.EqualTo(viewModel.SelectedItem));
                Assert.That(dataCommand, Is.EqualTo(DataCommand.Edit));
            };

            Assert.That(viewModel.EditCommand.CanExecute(null), Is.False);

            viewModel.SelectedItem = new WorkItemTypeViewModel(new WorkItemType(), new RepositoryFactory());
            Assert.That(viewModel.EditCommand.CanExecute(null), Is.True);

            viewModel.EditCommand.Execute(null);

            if (!eventSuccessful)
                Assert.Fail();
        }

        [Test]
        public void WorkItemTypeListViewModelDeleteItem()
        {
            var factory = new RepositoryFactory();
            var repo = factory.GetWorkItemTypeRepository();

            var item = new WorkItemType();
            repo.Save(item);

            Assert.That(repo.Find(item.Key), Is.Not.Null);

            var viewModel = new WorkItemTypeListViewModel(factory);

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
