using System;
using System.Collections.Generic;
using NUnit.Framework;
using STT.Model.Entity;

namespace STT.Model.Test
{
    [TestFixture]
    public class ProjectTest
    {
        [Test]
        public void CreateEmptyProject()
        {
            var project = new Project();
            Assert.IsNotNull(project);
        }

        [Test]
        public void ProjectInheritsBaseModel()
        {
            var project = new Project();
            Assert.IsInstanceOf<EntityBase>(project);
        }

        [Test]
        public void ProjectHasKey()
        {
            var key = Guid.NewGuid();

            var project = new Project
                              {
                                  Key = key
                              };

            Assert.AreEqual(key, project.Key);
        }

        [Test]
        public void CreateProjectViaDefaultConstructor()
        {
            const string name = "test project";
            const string description = "test project description";
            var createdOn = DateTime.Now;
            var owner = new UserAccount();
            var workItems = new List<WorkItem>
                                {
                                    new WorkItem(),
                                    new WorkItem(),
                                    new WorkItem()
                                };

            var project = new Project
                              {
                                  Name = name,
                                  Description = description,
                                  CreatedOn = createdOn,
                                  Owner = owner,
                                  WorkItems = workItems
                              };

            Assert.AreNotEqual(Guid.Empty, project.Key);
            Assert.AreEqual(name, project.Name);
            Assert.AreEqual(description, project.Description);
            Assert.AreEqual(createdOn, project.CreatedOn);
            Assert.AreEqual(owner, project.Owner);
            Assert.AreEqual(workItems, project.WorkItems);
        }

        [Test]
        public void CreateProjectViaNonDefaultConstructor()
        {
            const string name = "test project";
            const string description = "test project description";
            var owner = new UserAccount();

            var project = new Project(name, description, owner);

            Assert.AreNotEqual(Guid.Empty, project.Key);
            Assert.AreEqual(name, project.Name);
            Assert.AreEqual(description, project.Description);
            Assert.AreEqual(owner, project.Owner);
            Assert.IsNotNull(project.WorkItems);
        }
    }
}
