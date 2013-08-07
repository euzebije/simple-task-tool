using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using STT.Model.Entity;

namespace STT.Data.File.Test
{
    [TestFixture]
    public class UserAccountRepositoryTest
    {
        private IUserAccountRepository GetRepository()
        {
            var factory = new RepositoryFactory();
            return factory.GetUserAccountRepository();
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
            var workItemType = new UserAccount();

            Assert.That(repository.Find(workItemType.Key), Is.Null);

            repository.Save(workItemType);
            var savedModel = repository.Find(workItemType.Key);
            Assert.That(savedModel.IsEqualTo(workItemType), Is.True);
        }

        [Test]
        public void UpdateItem()
        {
            var repository = GetRepository();
            var workItemType = new UserAccount();
            repository.Save(workItemType);

            var savedModel = repository.Find(workItemType.Key);
            Assert.That(savedModel.Username, Is.EqualTo(workItemType.Username));

            workItemType.Username = "test";
            repository.Save(workItemType);

            savedModel = repository.Find(workItemType.Key);
            Assert.That(savedModel.Username, Is.EqualTo(workItemType.Username));
        }

        [Test]
        public void DeleteItem()
        {
            var repository = GetRepository();
            var workItemType = new UserAccount();
            repository.Save(workItemType);

            Assert.That(repository.Find(workItemType.Key), Is.Not.Null);

            repository.Delete(workItemType);
            Assert.That(repository.Find(workItemType.Key), Is.Null);
        }

        [Test]
        public void FindItem()
        {
            var repository = GetRepository();

            var userAccount1 = new UserAccount();
            var userAccount2 = new UserAccount();

            repository.Save(userAccount1);
            repository.Save(userAccount2);

            var savedModel1 = repository.Find(userAccount1.Key);
            var savedModel2 = repository.Find(userAccount2.Key);
            Assert.That(savedModel1.IsEqualTo(userAccount1), Is.True);
            Assert.That(savedModel2.IsEqualTo(userAccount2), Is.True);
        }

        [Test]
        public void GetItems()
        {
            var repository = GetRepository();

            var items = new List<UserAccount>
                            {
                                new UserAccount(),
                                new UserAccount(),
                                new UserAccount()
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
