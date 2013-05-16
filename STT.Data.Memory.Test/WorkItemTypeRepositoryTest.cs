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
            Assert.IsNotNull(repository);
        }

        [Test]
        public void InsertItem()
        {
            var repository = GetRepository();
            var workItemType = new WorkItemType();
            
            Assert.IsNull(repository.Find(workItemType.Key));

            repository.Save(workItemType);
            Assert.AreEqual(workItemType, repository.Find(workItemType.Key));
        }

        [Test]
        public void UpdateItem()
        {
            var repository = GetRepository();
            var workItemType = new WorkItemType();
            repository.Save(workItemType);

            var savedUser = repository.Find(workItemType.Key);
            Assert.AreEqual(workItemType.Name, savedUser.Name);

            workItemType.Name = "test";
            repository.Save(workItemType);

            savedUser = repository.Find(workItemType.Key);
            Assert.AreEqual(workItemType.Name, savedUser.Name);
        }

        [Test]
        public void DeleteItem()
        {
            var repository = GetRepository();
            var workItemType = new WorkItemType();
            repository.Save(workItemType);

            Assert.IsNotNull(repository.Find(workItemType.Key));
            
            repository.Delete(workItemType);
            Assert.IsNull(repository.Find(workItemType.Key));
        }

        [Test]
        public void FindItem()
        {
            var repository = GetRepository();

            var workItemType1 = new WorkItemType();
            var workItemType2 = new WorkItemType();

            repository.Save(workItemType1);
            repository.Save(workItemType2);

            Assert.AreEqual(workItemType1, repository.Find(workItemType1.Key));
            Assert.AreEqual(workItemType2, repository.Find(workItemType2.Key));
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
                Assert.Contains(item, savedItems);
            }
        }
    }
}
