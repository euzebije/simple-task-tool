﻿using System.Collections.Generic;
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
            Assert.IsNotNull(repository);
        }

        [Test]
        public void InsertItem()
        {
            var repository = GetRepository();
            var workItem = new WorkItem();
            
            Assert.IsNull(repository.Find(workItem.Key));

            repository.Save(workItem);
            Assert.AreEqual(workItem, repository.Find(workItem.Key));
        }

        [Test]
        public void UpdateItem()
        {
            var repository = GetRepository();
            var workItem = new WorkItem();
            repository.Save(workItem);

            var savedUser = repository.Find(workItem.Key);
            Assert.AreEqual(workItem.Title, savedUser.Title);

            workItem.Title = "test";
            repository.Save(workItem);

            savedUser = repository.Find(workItem.Key);
            Assert.AreEqual(workItem.Title, savedUser.Title);
        }

        [Test]
        public void DeleteItem()
        {
            var repository = GetRepository();
            var workItem = new WorkItem();
            repository.Save(workItem);

            Assert.IsNotNull(repository.Find(workItem.Key));
            
            repository.Delete(workItem);
            Assert.IsNull(repository.Find(workItem.Key));
        }

        [Test]
        public void FindItem()
        {
            var repository = GetRepository();

            var workItem1 = new WorkItem();
            var workItem2 = new WorkItem();

            repository.Save(workItem1);
            repository.Save(workItem2);

            Assert.AreEqual(workItem1, repository.Find(workItem1.Key));
            Assert.AreEqual(workItem2, repository.Find(workItem2.Key));
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
            Assert.Contains(child1, children);
            Assert.Contains(child2, children);
            Assert.Contains(child3, children);
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
                Assert.Contains(item, savedItems);
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
                        Assert.Contains(child, children);
                    }
                }
            }
        }
    }
}
