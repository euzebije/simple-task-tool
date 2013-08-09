using System;
using NUnit.Framework;
using STT.Data.Memory;
using STT.Model.Entity;

namespace STT.UI.Desktop.ViewModel.Test
{
    [TestFixture]
    public class WorkItemViewModelTest
    {
        [Test]
        public void CreateEmpty()
        {
            var model = new WorkItem();
            var viewModel = new WorkItemViewModel(model, new RepositoryFactory(), new UserAccount());
            Assert.That(viewModel, Is.Not.Null);
        }

        [Test]
        public void InheritsViewModelBase()
        {
            var viewModel = new WorkItemViewModel(new WorkItem(), new RepositoryFactory(), new UserAccount());
            Assert.That(viewModel, Is.InstanceOf<ViewModelBase>());
        }

        [Test]
        public void WrapsModel()
        {
            var model = new WorkItem("test", "description", new UserAccount(), Priority.Normal, new WorkItemType(), new Project());
            var viewModel = new WorkItemViewModel(model, new RepositoryFactory(), new UserAccount());

            Assert.That(viewModel.Title, Is.EqualTo(model.Title));
            Assert.That(viewModel.Description, Is.EqualTo(model.Description));
            Assert.That(viewModel.CreatedBy, Is.EqualTo(model.CreatedBy));
            Assert.That(viewModel.AssignedTo, Is.EqualTo(model.AssignedTo));
            Assert.That(viewModel.LastUpdate, Is.EqualTo(model.LastUpdate));
            Assert.That(viewModel.Priority, Is.EqualTo(model.Priority));
            Assert.That(viewModel.Type, Is.EqualTo(model.Type));
            Assert.That(viewModel.Project, Is.EqualTo(model.Project));
            Assert.That(viewModel.IsFinished, Is.EqualTo(model.IsFinished));
        }

        [Test]
        public void SaveNew()
        {
            var eventSuccessful = false;

            var factory = new RepositoryFactory();
            var repo = factory.GetWorkItemRepository();

            var model = new WorkItem();
            var viewModel = new WorkItemViewModel(model, factory, new UserAccount());

            var assignedTo = new UserAccount();
            var type = new WorkItemType();
            var project = new Project();

            viewModel.ModelSaved += itemViewModel =>
            {
                eventSuccessful = true;
                Assert.That(itemViewModel, Is.Not.Null);
                Assert.That(itemViewModel, Is.EqualTo(viewModel));

                var fetchedModel = repo.Find(model.Key);
                Assert.That(fetchedModel, Is.Not.Null);
                Assert.That(fetchedModel.Title, Is.EqualTo("test"));
                Assert.That(fetchedModel.Description, Is.EqualTo("description"));
                Assert.That(fetchedModel.CreatedBy, Is.Not.Null);
                Assert.That(fetchedModel.CreatedOn, Is.Not.EqualTo(DateTime.MinValue));
                Assert.That(fetchedModel.AssignedTo, Is.EqualTo(assignedTo));
                Assert.That(fetchedModel.LastUpdate, Is.Not.EqualTo(DateTime.MinValue));
                Assert.That(fetchedModel.Priority, Is.EqualTo(Priority.Low));
                Assert.That(fetchedModel.Type, Is.EqualTo(type));
                Assert.That(fetchedModel.Project, Is.EqualTo(project));
                Assert.That(fetchedModel.IsFinished, Is.True);
            };

            Assert.That(repo.Find(model.Key), Is.Null);

            viewModel.Title = "test";
            viewModel.Description = "description";
            viewModel.AssignedTo = assignedTo;
            viewModel.Priority = Priority.Low;
            viewModel.Type = type;
            viewModel.Project = project;
            viewModel.IsFinished = true;
            viewModel.Save();

            if (!eventSuccessful)
                Assert.Fail();
        }

        [Test]
        public void SaveExisting()
        {
            var eventSuccessful = false;

            var factory = new RepositoryFactory();
            var repo = factory.GetWorkItemRepository();

            var model = new WorkItem("test", "description", new UserAccount(), Priority.Normal, new WorkItemType(), new Project());
            repo.Save(model);

            var viewModel = new WorkItemViewModel(model, factory, new UserAccount());

            var assignedTo = new UserAccount();
            var type = new WorkItemType();
            var project = new Project();

            viewModel.ModelSaved += itemViewModel =>
            {
                eventSuccessful = true;
                Assert.That(itemViewModel, Is.Not.Null);
                Assert.That(itemViewModel, Is.EqualTo(viewModel));
                Assert.That(itemViewModel.IsInEditMode, Is.False);

                var item = repo.Find(model.Key);
                Assert.That(item, Is.Not.Null);
                Assert.That(item.Title, Is.EqualTo("test2"));
                Assert.That(item.Description, Is.EqualTo("description2"));
                Assert.That(item.AssignedTo, Is.EqualTo(assignedTo));
                Assert.That(item.Priority, Is.EqualTo(Priority.Low));
                Assert.That(item.Type, Is.EqualTo(type));
                Assert.That(item.Project, Is.EqualTo(project));
                Assert.That(item.IsFinished, Is.True);
            };

            viewModel.IsInEditMode = true;
            viewModel.Title = "test2";
            viewModel.Description = "description2";
            viewModel.AssignedTo = assignedTo;
            viewModel.Priority = Priority.Low;
            viewModel.Type = type;
            viewModel.Project = project;
            viewModel.IsFinished = true;
            viewModel.Save();

            if (!eventSuccessful)
                Assert.Fail();
        }

        [Test]
        public void CancelEdit()
        {
            var factory = new RepositoryFactory();
            var repo = factory.GetWorkItemRepository();

            var type = new WorkItemType();
            var project = new Project();

            var model = new WorkItem("test", "description", new UserAccount(), Priority.Normal, type, project);
            repo.Save(model);

            var viewModel = new WorkItemViewModel(model, factory, new UserAccount());
            viewModel.ModelSaved += itemViewModel => Assert.Fail();

            viewModel.IsInEditMode = true;
            viewModel.Title = "test2";
            viewModel.Description = "description2";
            viewModel.AssignedTo = new UserAccount();
            viewModel.Priority = Priority.Low;
            viewModel.Type = new WorkItemType();
            viewModel.Project = new Project();
            viewModel.IsFinished = true;
            viewModel.Cancel();

            Assert.That(viewModel.IsInEditMode, Is.False);
            Assert.That(viewModel.Title, Is.EqualTo("test"));
            Assert.That(viewModel.Description, Is.EqualTo("description"));
            Assert.That(viewModel.AssignedTo, Is.Null);
            Assert.That(viewModel.Priority, Is.EqualTo(Priority.Normal));
            Assert.That(viewModel.Type, Is.EqualTo(type));
            Assert.That(viewModel.Project, Is.EqualTo(project));
            Assert.That(viewModel.IsFinished, Is.False);
        }

        [Test]
        public void WorkItemTypeViewModelSaveValidation()
        {
            var factory = new RepositoryFactory();
            var repo = factory.GetWorkItemRepository();

            Assert.That(repo.Get(), Is.Empty);

            var viewModel = new WorkItemViewModel(new WorkItem(), factory, new UserAccount());

            Assert.That(viewModel.IsValid, Is.False);
            viewModel.Save();
            Assert.That(repo.Get(), Is.Empty);

            viewModel.Title = "test";
            viewModel.Save();
            Assert.That(viewModel.IsValid, Is.False);
            Assert.That(repo.Get(), Is.Empty);

            viewModel.Type = new WorkItemType();
            viewModel.Save();
            Assert.That(viewModel.IsValid, Is.False);
            Assert.That(repo.Get(), Is.Empty);

            viewModel.Project = new Project();
            viewModel.Save();
            Assert.That(viewModel.IsValid, Is.True);
            Assert.That(repo.Get(), Is.Not.Empty);
        }
    }
}
