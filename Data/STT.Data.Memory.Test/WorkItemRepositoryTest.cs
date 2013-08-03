using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using STT.Model.Entity;

namespace STT.Data.Memory.Test
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

            var savedUser = repository.Find(workItem.Key);
            Assert.That(savedUser.Title, Is.EqualTo(workItem.Title));

            workItem.Title = "test";
            repository.Save(workItem);

            savedUser = repository.Find(workItem.Key);
            Assert.That(savedUser.Title, Is.EqualTo(workItem.Title));
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
        public void FindItemWithChildren()
        {
            var repository = GetRepository();

            var workItem = new WorkItem();

            var child1 = new WorkItem{Parent = workItem};
            var child2 = new WorkItem{Parent = workItem};
            var child3 = new WorkItem {Parent = workItem};

            workItem.Children.Add(child1);
            workItem.Children.Add(child2);
            workItem.Children.Add(child3);

            repository.Save(workItem);

            var savedItem = repository.Find(workItem.Key);
            var children = savedItem.Children.ToList();
            Assert.That(children, Contains.Item(child1));
            Assert.That(children, Contains.Item(child2));
            Assert.That(children, Contains.Item(child3));
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

        [Test]
        public void GetItemsWithChildren()
        {
            var repository = GetRepository();

            var workItem1 = new WorkItem();
            var workItem2 = new WorkItem();
            var items = new List<WorkItem> {workItem1, workItem2};

            var child11 = new WorkItem {Parent = workItem1};
            var child12 = new WorkItem {Parent = workItem1};
            workItem1.Children.Add(child11);
            workItem1.Children.Add(child12);

            var child21 = new WorkItem { Parent = workItem2 };
            var child22 = new WorkItem { Parent = workItem2 };
            workItem2.Children.Add(child21);
            workItem2.Children.Add(child22);

            repository.Save(workItem1);
            repository.Save(workItem2);

            var savedItems = repository.GetWithChildren();
            foreach (var savedItem in savedItems)
            {
                var item = items.SingleOrDefault(x => x.Key == savedItem.Key);
                if (item != null)
                {
                    var children = savedItem.Children.ToList();
                    foreach (var child in item.Children)
                    {
                        Assert.That(children, Contains.Item(child));
                    }
                }
            }
        }
    }
}
