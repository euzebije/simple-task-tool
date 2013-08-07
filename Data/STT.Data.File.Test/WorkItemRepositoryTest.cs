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
            var workItemType = new WorkItem();

            Assert.That(repository.Find(workItemType.Key), Is.Null);

            repository.Save(workItemType);
            var savedModel = repository.Find(workItemType.Key);
            Assert.That(savedModel.IsEqualTo(workItemType), Is.True);
        }

        [Test]
        public void UpdateItem()
        {
            var repository = GetRepository();
            var workItemType = new WorkItem();
            repository.Save(workItemType);

            var savedModel = repository.Find(workItemType.Key);
            Assert.That(savedModel.Title, Is.EqualTo(workItemType.Title));

            workItemType.Title = "test";
            repository.Save(workItemType);

            savedModel = repository.Find(workItemType.Key);
            Assert.That(savedModel.Title, Is.EqualTo(workItemType.Title));
        }

        [Test]
        public void DeleteItem()
        {
            var repository = GetRepository();
            var workItemType = new WorkItem();
            repository.Save(workItemType);

            Assert.That(repository.Find(workItemType.Key), Is.Not.Null);

            repository.Delete(workItemType);
            Assert.That(repository.Find(workItemType.Key), Is.Null);
        }

        [Test]
        public void FindItem()
        {
            var repository = GetRepository();

            var workItem1 = new WorkItem();
            var workItem2 = new WorkItem();

            repository.Save(workItem1);
            repository.Save(workItem2);

            var savedModel1 = repository.Find(workItem1.Key);
            var savedModel2 = repository.Find(workItem2.Key);
            Assert.That(savedModel1.IsEqualTo(workItem1), Is.True);
            Assert.That(savedModel2.IsEqualTo(workItem2), Is.True);
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
                var savedItem = savedItems.SingleOrDefault(x => x.Key == item.Key);
                Assert.That(savedItem, Is.Not.Null);
            }
        }
    }
}
