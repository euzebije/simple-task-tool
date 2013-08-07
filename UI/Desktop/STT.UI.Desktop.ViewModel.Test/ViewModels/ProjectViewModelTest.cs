using NUnit.Framework;
using STT.Data.Memory;
using STT.Model.Entity;

namespace STT.UI.Desktop.ViewModel.Test
{
    [TestFixture]
    public class ProjectViewModelTest
    {
        [Test]
        public void CreateEmptyProjectViewModel()
        {
            var model = new Project();
            var viewModel = new ProjectViewModel(model, new RepositoryFactory());
            Assert.That(viewModel, Is.Not.Null);
        }

        [Test]
        public void ProjectViewModelInheritsViewModelBase()
        {
            var viewModel = new ProjectViewModel(new Project(), new RepositoryFactory());
            Assert.That(viewModel, Is.InstanceOf<ViewModelBase>());
        }

        [Test]
        public void ViewModelWrapsModel()
        {
            var model = new Project("test", "description", new UserAccount());
            var viewModel = new ProjectViewModel(model, new RepositoryFactory());

            Assert.That(viewModel.Name, Is.EqualTo(model.Name));
            Assert.That(viewModel.Description, Is.EqualTo(model.Description));
            Assert.That(viewModel.Owner, Is.EqualTo(model.Owner));
        }

        [Test]
        public void ProjectViewModelSaveNew()
        {
            var eventSuccessful = false;

            var factory = new RepositoryFactory();
            var repo = factory.GetProjectRepository();

            var owner = new UserAccount();
            var model = new Project();
            var viewModel = new ProjectViewModel(model, factory);
            viewModel.ModelSaved += itemViewModel =>
                                        {
                                            eventSuccessful = true;
                                            Assert.That(itemViewModel, Is.Not.Null);
                                            Assert.That(itemViewModel, Is.EqualTo(viewModel));

                                            var fetchedModel = repo.Find(model.Key);
                                            Assert.That(fetchedModel, Is.Not.Null);
                                            Assert.That(fetchedModel.Name, Is.EqualTo("name"));
                                            Assert.That(fetchedModel.Description, Is.EqualTo("description"));
                                            Assert.That(fetchedModel.Owner, Is.EqualTo(owner));
                                        };

            Assert.That(repo.Find(model.Key), Is.Null);

            viewModel.Name = "name";
            viewModel.Description = "description";
            viewModel.Owner = owner;
            viewModel.Save();

            if (!eventSuccessful)
                Assert.Fail();
        }

        [Test]
        public void ProjectViewModelSaveExisting()
        {
            var eventSuccessful = false;

            var factory = new RepositoryFactory();
            var repo = factory.GetProjectRepository();

            var model = new Project("test", "description", new UserAccount());
            repo.Save(model);

            var owner2 = new UserAccount();
            var viewModel = new ProjectViewModel(model, factory);
            viewModel.ModelSaved += itemViewModel =>
                                        {
                                            eventSuccessful = true;
                                            Assert.That(itemViewModel, Is.Not.Null);
                                            Assert.That(itemViewModel, Is.EqualTo(viewModel));
                                            Assert.That(itemViewModel.IsInEditMode, Is.False);

                                            var item = repo.Find(model.Key);
                                            Assert.That(item.Name, Is.EqualTo("test2"));
                                            Assert.That(item.Description, Is.EqualTo("description2"));
                                            Assert.That(item.Owner, Is.EqualTo(owner2));
                                        };

            viewModel.IsInEditMode = true;
            viewModel.Name = "test2";
            viewModel.Description = "description2";
            viewModel.Owner = owner2;
            viewModel.Save();

            if (!eventSuccessful)
                Assert.Fail();
        }

        [Test]
        public void WorkItemTypeViewModelCancelEdit()
        {
            var factory = new RepositoryFactory();
            var repo = factory.GetProjectRepository();

            var owner = new UserAccount();
            var model = new Project("test", "description", owner);
            repo.Save(model);

            var viewModel = new ProjectViewModel(model, factory);
            viewModel.ModelSaved += itemViewModel => Assert.Fail();

            viewModel.IsInEditMode = true;
            viewModel.Name = "test2";
            viewModel.Description = "description2";
            viewModel.Owner = new UserAccount();
            viewModel.Cancel();

            Assert.That(viewModel.IsInEditMode, Is.False);
            Assert.That(viewModel.Name, Is.EqualTo("test"));
            Assert.That(viewModel.Description, Is.EqualTo("description"));
            Assert.That(viewModel.Owner, Is.EqualTo(owner));
        }
    }
}
