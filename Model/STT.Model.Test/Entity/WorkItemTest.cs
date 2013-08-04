using System;
using NUnit.Framework;
using STT.Model.Entity;

namespace STT.Model.Test.Entity
{
    [TestFixture]
    public class WorkItemTest
    {
        [Test]
        public void CreateEmptyWorkItem()
        {
            var workItem = new WorkItem();
            Assert.That(workItem, Is.Not.Null);
        }

        [Test]
        public void WorkItemInheritsBaseModel()
        {
            var userAccount = new WorkItem();
            Assert.That(userAccount, Is.InstanceOf<EntityBase>());
        }

        [Test]
        public void WorkItemHasKey()
        {
            var key = Guid.NewGuid();

            var userAccount = new WorkItem
            {
                Key = key
            };

            Assert.That(userAccount.Key, Is.EqualTo(key));
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
            const bool isFinished = true;
            var project = new Project();

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
                                   IsFinished = isFinished,
                                   Project = project
                               };

            Assert.That(workItem.Key, Is.Not.EqualTo(Guid.Empty));
            Assert.That(workItem.Title, Is.EqualTo(title));
            Assert.That(workItem.Description, Is.EqualTo(description));
            Assert.That(workItem.CreatedBy, Is.EqualTo(createdBy));
            Assert.That(workItem.CreatedOn, Is.EqualTo(createdOn));
            Assert.That(workItem.AssignedTo, Is.EqualTo(assignedTo));
            Assert.That(workItem.LastUpdate, Is.EqualTo(lastUpdate));
            Assert.That(workItem.Priority, Is.EqualTo(priority));
            Assert.That(workItem.Type, Is.EqualTo(type));
            Assert.That(workItem.IsFinished, Is.EqualTo(isFinished));
            Assert.That(workItem.Project, Is.EqualTo(project));
        }

        [Test]
        public void CreateWorkItemViaNonDefaultConstructor()
        {
            const string title = "test work item";
            const string description = "test work item description";
            var createdBy = new UserAccount();
            const Priority priority = Priority.Normal;
            var type = new WorkItemType();
            var project = new Project();

            var workItem = new WorkItem(title, description, createdBy, priority, type, project);

            Assert.That(workItem.Key, Is.Not.EqualTo(Guid.Empty));
            Assert.That(workItem.Title, Is.EqualTo(title));
            Assert.That(workItem.Description, Is.EqualTo(description));
            Assert.That(workItem.CreatedBy, Is.EqualTo(createdBy));
            Assert.That(workItem.AssignedTo, Is.Null);
            Assert.That(workItem.Priority, Is.EqualTo(priority));
            Assert.That(workItem.Type, Is.EqualTo(type));
            Assert.That(workItem.IsFinished, Is.False);
            Assert.That(workItem.Project, Is.EqualTo(project));
        }
    }
}
