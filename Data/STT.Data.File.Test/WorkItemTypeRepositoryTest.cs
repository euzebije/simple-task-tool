﻿using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using STT.Model.Entity;

namespace STT.Data.File.Test
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
            var savedModel = repository.Find(workItemType.Key);
            Assert.That(savedModel.IsEqualTo(workItemType), Is.True);
        }

        [Test]
        public void UpdateItem()
        {
            var repository = GetRepository();
            var workItemType = new WorkItemType();
            repository.Save(workItemType);

            var savedModel = repository.Find(workItemType.Key);
            Assert.That(savedModel.Name, Is.EqualTo(workItemType.Name));

            workItemType.Name = "test";
            repository.Save(workItemType);

            savedModel = repository.Find(workItemType.Key);
            Assert.That(savedModel.Name, Is.EqualTo(workItemType.Name));
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

            var savedModel1 = repository.Find(workItemType1.Key);
            var savedModel2 = repository.Find(workItemType2.Key);
            Assert.That(savedModel1.IsEqualTo(workItemType1), Is.True);
            Assert.That(savedModel2.IsEqualTo(workItemType2), Is.True);
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
                var savedItem = savedItems.SingleOrDefault(x => x.Key == item.Key);
                Assert.That(savedItem, Is.Not.Null);
            }
        }
    }
}