using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using STT.Model.Entity;

namespace STT.Data.Memory.Test
{
    [TestFixture]
    public class ProjectRepositoryTest
    {
        private IProjectRepository GetRepository()
        {
            var factory = new RepositoryFactory();
            return factory.GetProjectRepository();
        }
        private IWorkItemRepository GetWorkItemRepository()
        {
            var factory = new RepositoryFactory();
            return factory.GetWorkItemRepository();
        }

        [Test]
        public void CreateRepository()
        {
            var repository = GetRepository();
            Assert.That(repository, Is.Not.Null);
        }

        [Test]
        public void InsertItem()
        {
            var repository = GetRepository();
            var project = new Project();
            
            Assert.That(repository.Find(project.Key), Is.Null);

            repository.Save(project);
            Assert.That(repository.Find(project.Key), Is.EqualTo(project));
        }

        [Test]
        public void UpdateItem()
        {
            var repository = GetRepository();
            var project = new Project();
            repository.Save(project);

            var savedUser = repository.Find(project.Key);
            Assert.That(savedUser.Name, Is.EqualTo(project.Name));

            project.Name = "test";
            repository.Save(project);

            savedUser = repository.Find(project.Key);
            Assert.That(savedUser.Name, Is.EqualTo(project.Name));
        }

        [Test]
        public void DeleteItem()
        {
            var repository = GetRepository();
            var project = new Project();
            repository.Save(project);

            Assert.That(repository.Find(project.Key), Is.Not.Null);
            
            repository.Delete(project);
            Assert.That(repository.Find(project.Key), Is.Null);
        }

        [Test]
        public void FindItem()
        {
            var repository = GetRepository();

            var project1 = new Project();
            var project2 = new Project();

            repository.Save(project1);
            repository.Save(project2);

            Assert.That(repository.Find(project1.Key), Is.EqualTo(project1));
            Assert.That(repository.Find(project2.Key), Is.EqualTo(project2));
        }

        [Test]
        public void FindItemWithWorkItems()
        {
            var repository = GetRepository();
            var workItemRepository = GetWorkItemRepository();

            var project = new Project();

            var workItem1 = new WorkItem { Project = project };
            var workItem2 = new WorkItem { Project = project };
            var workItem3 = new WorkItem { Project = project };

            project.WorkItems.Add(workItem1);
            project.WorkItems.Add(workItem2);
            project.WorkItems.Add(workItem3);

            repository.Save(project);
            workItemRepository.Save(workItem1);
            workItemRepository.Save(workItem2);
            workItemRepository.Save(workItem3);

            var savedItem = repository.Find(project.Key);
            var workItems = savedItem.WorkItems.ToList();
            Assert.That(workItems, Contains.Item(workItem1));
            Assert.That(workItems, Contains.Item(workItem2));
            Assert.That(workItems, Contains.Item(workItem3));
        }

        [Test]
        public void GetItems()
        {
            var repository = GetRepository();

            var items = new List<Project>
                            {
                                new Project(),
                                new Project(),
                                new Project()
                            };

            foreach (var item in items)
            {
                repository.Save(item);
            }

            var savedItems = repository.Get().ToList();
            foreach (var item in items)
            {
                Assert.That(savedItems, Contains.Item(item));
            }
        }

        [Test]
        public void GetItemsWithChildren()
        {
            var repository = GetRepository();
            var workItemRepository = GetWorkItemRepository();

            var project1 = new Project();
            var project2 = new Project();
            var items = new List<Project> { project1, project2 };

            var workItem11 = new WorkItem { Project = project1 };
            var workItem12 = new WorkItem { Project = project1 };
            project1.WorkItems.Add(workItem11);
            project1.WorkItems.Add(workItem12);

            var workItem21 = new WorkItem { Project = project2 };
            var workItem22 = new WorkItem { Project = project2 };
            project2.WorkItems.Add(workItem21);
            project2.WorkItems.Add(workItem22);

            repository.Save(project1);
            repository.Save(project2);
            workItemRepository.Save(workItem11);
            workItemRepository.Save(workItem12);
            workItemRepository.Save(workItem21);
            workItemRepository.Save(workItem22);

            var savedItems = repository.GetWithWorkItems();
            foreach (var savedItem in savedItems)
            {
                var item = items.SingleOrDefault(x => x.Key == savedItem.Key);
                if (item != null)
                {
                    var workItems = savedItem.WorkItems.ToList();
                    foreach (var child in item.WorkItems)
                    {
                        Assert.That(workItems, Contains.Item(child));
                    }
                }
            }
        }
    }
}
