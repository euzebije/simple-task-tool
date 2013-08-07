using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using STT.Model.Entity;

namespace STT.Data.File.Test
{
    [TestFixture]
    public class WorkItemRepositoryTest
    {
        private IWorkItemRepository GetRepository()
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
            var workItem = new WorkItem();

            Assert.That(repository.Find(workItem.Key), Is.Null);

            repository.Save(workItem);
            Assert.That(repository.Find(workItem.Key), Is.EqualTo(workItem));
        }

        [Test]
        public void UpdateItem()
        {
            var repository = GetRepository();
            var workItem = new WorkItem();
            repository.Save(workItem);

            var savedModel = repository.Find(workItem.Key);
            Assert.That(savedModel.Title, Is.EqualTo(workItem.Title));

            workItem.Title = "test";
            repository.Save(workItem);

            savedModel = repository.Find(workItem.Key);
            Assert.That(savedModel.Title, Is.EqualTo(workItem.Title));
        }

        [Test]
        public void DeleteItem()
        {
            var repository = GetRepository();
            var workItem = new WorkItem();
            repository.Save(workItem);

            Assert.That(repository.Find(workItem.Key), Is.Not.Null);

            repository.Delete(workItem);
            Assert.That(repository.Find(workItem.Key), Is.Null);
        }

        [Test]
        public void FindItem()
        {
            var repository = GetRepository();

            var workItem1 = new WorkItem();
            var workItem2 = new WorkItem();

            repository.Save(workItem1);
            repository.Save(workItem2);

            Assert.That(repository.Find(workItem1.Key), Is.EqualTo(workItem1));
            Assert.That(repository.Find(workItem2.Key), Is.EqualTo(workItem2));
        }

        [Test]
        public void GetItems()
        {
            var repository = GetRepository();

            var items = new List<WorkItem>
                            {
                                new WorkItem(),
                                new WorkItem(),
                                new WorkItem()
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
    }
}
