using NUnit.Framework;
using STT.Data.Memory;
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
            var viewModel = new WorkItemTypeViewModel(model, new RepositoryFactory());
            Assert.That(viewModel, Is.Not.Null);
        }

        [Test]
        public void WorkItemTypeViewModelInheritsViewModelBase()
        {
            var viewModel = new WorkItemTypeViewModel(new WorkItemType(), new RepositoryFactory());
            Assert.That(viewModel, Is.InstanceOf<ViewModelBase>());
        }

        [Test]
        public void ViewModelWrapsModel()
        {
            var model = new WorkItemType("test", "description");
            var viewModel = new WorkItemTypeViewModel(model, new RepositoryFactory());

            Assert.That(viewModel.Name, Is.EqualTo(model.Name));
            Assert.That(viewModel.Description, Is.EqualTo(model.Description));
        }

        [Test]
        public void WorkItemTypeViewModelSaveNew()
        {
            var eventSuccessful = false;

            var factory = new RepositoryFactory();
            var repo = factory.GetWorkItemTypeRepository();

            var model = new WorkItemType();
            var viewModel = new WorkItemTypeViewModel(model, factory);
            viewModel.ModelSaved += itemViewModel =>
                                        {
                                            eventSuccessful = true;
                                            Assert.That(itemViewModel, Is.Not.Null);
                                            Assert.That(itemViewModel, Is.EqualTo(viewModel));

                                            var fetchedModel = repo.Find(model.Key);
                                            Assert.That(fetchedModel, Is.Not.Null);
                                            Assert.That(fetchedModel.Name, Is.EqualTo("name"));
                                            Assert.That(fetchedModel.Description, Is.EqualTo("description"));
                                        };

            Assert.That(repo.Find(model.Key), Is.Null);

            viewModel.Name = "name";
            viewModel.Description = "description";
            viewModel.Save();

            if (!eventSuccessful)
                Assert.Fail();
        }

        [Test]
        public void WorkItemTypeViewModelSaveExisting()
        {
            var eventSuccessful = false;

            var factory = new RepositoryFactory();
            var repo = factory.GetWorkItemTypeRepository();

            var model = new WorkItemType("test", "description");
            repo.Save(model);

            var viewModel = new WorkItemTypeViewModel(model, factory);
            viewModel.ModelSaved += itemViewModel =>
                                        {
                                            eventSuccessful = true;
                                            Assert.That(itemViewModel, Is.Not.Null);
                                            Assert.That(itemViewModel, Is.EqualTo(viewModel));
                                            Assert.That(itemViewModel.IsInEditMode, Is.False);

                                            var item = repo.Find(model.Key);
                                            Assert.That(item.Name, Is.EqualTo("test2"));
                                            Assert.That(item.Description, Is.EqualTo("description2"));
                                        };

            viewModel.IsInEditMode = true;
            viewModel.Name = "test2";
            viewModel.Description = "description2";
            viewModel.Save();

            if (!eventSuccessful)
                Assert.Fail();
        }

        [Test]
        public void WorkItemTypeViewModelCancelEdit()
        {
            var factory = new RepositoryFactory();
            var repo = factory.GetWorkItemTypeRepository();

            var model = new WorkItemType("test", "description");
            repo.Save(model);

            var viewModel = new WorkItemTypeViewModel(model, factory);
            viewModel.ModelSaved += itemViewModel => Assert.Fail();

            viewModel.IsInEditMode = true;
            viewModel.Name = "test2";
            viewModel.Description = "description2";
            viewModel.Cancel();

            Assert.That(viewModel.IsInEditMode, Is.False);
            Assert.That(viewModel.Name, Is.EqualTo("test"));
            Assert.That(viewModel.Description, Is.EqualTo("description"));
        }
    }
}
