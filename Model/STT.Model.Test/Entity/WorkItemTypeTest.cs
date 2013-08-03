using System;
using NUnit.Framework;
using STT.Model.Entity;

namespace STT.Model.Test.Entity
{
    [TestFixture]
    public class WorkItemTypeTest
    {
        [Test]
        public void CreateEmptyWorkItemType()
        {
            var workItemType = new WorkItemType();
            Assert.That(workItemType, Is.Not.Null);
        }

        [Test]
        public void WorkItemTypeInheritsBaseModel()
        {
            var workItemType = new WorkItemType();
            Assert.That(workItemType, Is.InstanceOf<EntityBase>());
        }

        [Test]
        public void WorkItemTypeHasKey()
        {
            var key = Guid.NewGuid();

            var workItemType = new WorkItemType
                                   {
                                       Key = key
                                   };

            Assert.That(workItemType.Key, Is.EqualTo(key));
        }

        [Test]
        public void CreateWorkItemTypeViaDefaultConstructor()
        {
            const string name = "test";
            const string description = "test description";

            var workItemType = new WorkItemType
                                   {
                                       Name = name,
                                       Description = description
                                   };

            Assert.That(workItemType.Key, Is.Not.EqualTo(Guid.Empty));
            Assert.That(workItemType.Name, Is.EqualTo(name));
            Assert.That(workItemType.Description, Is.EqualTo(description));
        }

        [Test]
        public void CreateWorkItemTypeViaNonDefaultConstructor()
        {
            const string name = "test";
            const string description = "test description";

            var workItemType = new WorkItemType(name, description);

            Assert.That(workItemType.Key, Is.Not.EqualTo(Guid.Empty));
            Assert.That(workItemType.Name, Is.EqualTo(name));
            Assert.That(workItemType.Description, Is.EqualTo(description));
        }
    }
}
