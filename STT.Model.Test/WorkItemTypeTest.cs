using System;
using NUnit.Framework;
using STT.Model.Entity;

namespace STT.Model.Test
{
    [TestFixture]
    public class WorkItemTypeTest
    {
        [Test]
        public void CreateEmptyWorkItemType()
        {
            var workItemType = new WorkItemType();
            Assert.IsNotNull(workItemType);
        }

        [Test]
        public void WorkItemTypeInheritsBaseModel()
        {
            var workItemType = new WorkItemType();
            Assert.IsInstanceOf<EntityBase>(workItemType);
        }

        [Test]
        public void WorkItemTypeHasKey()
        {
            var key = Guid.NewGuid();

            var workItemType = new WorkItemType
                                   {
                                       Key = key
                                   };

            Assert.AreEqual(key, workItemType.Key);
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

            Assert.AreNotEqual(Guid.Empty, workItemType.Key);
            Assert.AreEqual(name, workItemType.Name);
            Assert.AreEqual(description, workItemType.Description);
        }

        [Test]
        public void CreateWorkItemTypeViaNonDefaultConstructor()
        {
            const string name = "test";
            const string description = "test description";

            var workItemType = new WorkItemType(name, description);

            Assert.AreNotEqual(Guid.Empty, workItemType.Key);
            Assert.AreEqual(name, workItemType.Name);
            Assert.AreEqual(description, workItemType.Description);
        }
    }
}
