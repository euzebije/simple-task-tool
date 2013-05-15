using System;
using NUnit.Framework;
using STT.Model.Entity;

namespace STT.Model.Test
{
    [TestFixture]
    public class WorkItemTest
    {
        [Test]
        public void CreateEmptyWorkItem()
        {
            var workItem = new WorkItem();
            Assert.IsNotNull(workItem);
        }

        [Test]
        public void WorkItemInheritsBaseModel()
        {
            var userAccount = new WorkItem();
            Assert.IsInstanceOf<EntityBase>(userAccount);
        }

        [Test]
        public void WorkItemHasKey()
        {
            var key = Guid.NewGuid();

            var userAccount = new WorkItem
            {
                Key = key
            };

            Assert.AreEqual(key, userAccount.Key);
        }

        [Test]
        public void CreateWorkItemViaDefaultConstructor()
        {
            const string title = "test work item";
            const string description = "test work item description";
            var createdBy = new UserAccount();
            var createdOn = DateTime.Now;
            var assignedTo = new UserAccount();
            var lastUpdate = createdOn.AddMinutes(5);
            const Priority priority = Priority.Normal;
            var type = new WorkItemType();
            var parent = new WorkItem();
            const bool isFinished = true;

            var workItem = new WorkItem
                               {
                                   Title = title,
                                   Description = description,
                                   CreatedBy = createdBy,
                                   CreatedOn = createdOn,
                                   AssignedTo = assignedTo,
                                   LastUpdate = lastUpdate,
                                   Priority = priority,
                                   Type = type,
                                   Parent = parent,
                                   IsFinished = isFinished
                               };

            Assert.AreNotEqual(Guid.Empty, workItem.Key);
            Assert.AreEqual(title, workItem.Title);
            Assert.AreEqual(description, workItem.Description);
            Assert.AreEqual(createdBy, workItem.CreatedBy);
            Assert.AreEqual(createdOn, workItem.CreatedOn);
            Assert.AreEqual(assignedTo, workItem.AssignedTo);
            Assert.AreEqual(lastUpdate, workItem.LastUpdate);
            Assert.AreEqual(priority, workItem.Priority);
            Assert.AreEqual(type, workItem.Type);
            Assert.AreEqual(parent, workItem.Parent);
            Assert.AreEqual(isFinished, workItem.IsFinished);
        }

        [Test]
        public void CreateWorkItemViaNonDefaultConstructor()
        {
            const string title = "test work item";
            const string description = "test work item description";
            var createdBy = new UserAccount();
            const Priority priority = Priority.Normal;
            var type = new WorkItemType();

            var workItem = new WorkItem(title, description, createdBy, priority, type);

            Assert.AreNotEqual(Guid.Empty, workItem.Key);
            Assert.AreEqual(title, workItem.Title);
            Assert.AreEqual(description, workItem.Description);
            Assert.AreEqual(createdBy, workItem.CreatedBy);
            Assert.AreEqual(null, workItem.AssignedTo);
            Assert.AreEqual(priority, workItem.Priority);
            Assert.AreEqual(type, workItem.Type);
            Assert.AreEqual(null, workItem.Parent);
            Assert.AreEqual(false, workItem.IsFinished);
        }

        [Test]
        public void CreateWorkItemViaNonDefaultConstructorWithParent()
        {
            const string title = "test work item";
            const string description = "test work item description";
            var createdBy = new UserAccount();
            const Priority priority = Priority.Normal;
            var type = new WorkItemType();
            var parent = new WorkItem();

            var workItem = new WorkItem(title, description, createdBy, priority, type, parent);

            Assert.AreNotEqual(Guid.Empty, workItem.Key);
            Assert.AreEqual(title, workItem.Title);
            Assert.AreEqual(description, workItem.Description);
            Assert.AreEqual(createdBy, workItem.CreatedBy);
            Assert.AreEqual(null, workItem.AssignedTo);
            Assert.AreEqual(priority, workItem.Priority);
            Assert.AreEqual(type, workItem.Type);
            Assert.AreEqual(parent, workItem.Parent);
            Assert.AreEqual(false, workItem.IsFinished);
        }
    }
}
