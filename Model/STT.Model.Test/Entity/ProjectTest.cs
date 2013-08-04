using System;
using System.Collections.Generic;
using NUnit.Framework;
using STT.Model.Entity;

namespace STT.Model.Test.Entity
{
    [TestFixture]
    public class ProjectTest
    {
        [Test]
        public void CreateEmptyProject()
        {
            var project = new Project();
            Assert.That(project, Is.Not.Null);
        }

        [Test]
        public void ProjectInheritsBaseModel()
        {
            var project = new Project();
            Assert.That(project, Is.InstanceOf<EntityBase>());
        }

        [Test]
        public void ProjectHasKey()
        {
            var key = Guid.NewGuid();

            var project = new Project
                              {
                                  Key = key
                              };

            Assert.That(project.Key, Is.EqualTo(key));
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

            Assert.That(project.Key, Is.Not.EqualTo(Guid.Empty));
            Assert.That(project.Name, Is.EqualTo(name));
            Assert.That(project.Description, Is.EqualTo(description));
            Assert.That(project.CreatedOn, Is.EqualTo(createdOn));
            Assert.That(project.Owner, Is.EqualTo(owner));
            Assert.That(project.WorkItems, Is.EqualTo(workItems));
        }

        [Test]
        public void CreateProjectViaNonDefaultConstructor()
        {
            const string name = "test project";
            const string description = "test project description";
            var owner = new UserAccount();

            var project = new Project(name, description, owner);

            Assert.That(project.Key, Is.Not.EqualTo(Guid.Empty));
            Assert.That(project.Name, Is.EqualTo(name));
            Assert.That(project.Description, Is.EqualTo(description));
            Assert.That(project.Owner, Is.EqualTo(owner));
            Assert.That(project.WorkItems, Is.Not.Null);
        }
    }
}
