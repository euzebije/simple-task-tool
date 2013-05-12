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

        //[Test]
        //public void CreateWorkItemAndFillBasicData()
        //{
        //    const string name = "Test work item";
        //    const string description = 

        //    var workItem = new WorkItem();
        //}
    }
}
