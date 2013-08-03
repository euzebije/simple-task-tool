using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using STT.Model.Entity;

namespace STT.Data.Memory.Test
{
    [TestFixture]
    public class WorkItemTypeRepositoryTest
    {
        private IWorkItemTypeRepository GetRepository()
        {
            var factory = new RepositoryFactory();
            return factory.GetWorkItemTypeRepository();
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
            var workItemType = new WorkItemType();
            
            Assert.That(repository.Find(workItemType.Key), Is.Null);

            repository.Save(workItemType);
            Assert.That(repository.Find(workItemType.Key), Is.EqualTo(workItemType));
        }

        [Test]
        public void UpdateItem()
        {
            var repository = GetRepository();
            var workItemType = new WorkItemType();
            repository.Save(workItemType);

            var savedUser = repository.Find(workItemType.Key);
            Assert.That(savedUser.Name, Is.EqualTo(workItemType.Name));

            workItemType.Name = "test";
            repository.Save(workItemType);

            savedUser = repository.Find(workItemType.Key);
            Assert.That(savedUser.Name, Is.EqualTo(workItemType.Name));
        }

        [Test]
        public void DeleteItem()
        {
            var repository = GetRepository();
            var workItemType = new WorkItemType();
            repository.Save(workItemType);

            Assert.That(repository.Find(workItemType.Key), Is.Not.Null);
            
            repository.Delete(workItemType);
            Assert.That(repository.Find(workItemType.Key), Is.Null);
        }

        [Test]
        public void FindItem()
        {
            var repository = GetRepository();

            var workItemType1 = new WorkItemType();
            var workItemType2 = new WorkItemType();

            repository.Save(workItemType1);
            repository.Save(workItemType2);

            Assert.That(repository.Find(workItemType1.Key), Is.EqualTo(workItemType1));
            Assert.That(repository.Find(workItemType2.Key), Is.EqualTo(workItemType2));
        }

        [Test]
        public void GetItems()
        {
            var repository = GetRepository();

            var items = new List<WorkItemType>
                            {
                                new WorkItemType(),
                                new WorkItemType(),
                                new WorkItemType()
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
