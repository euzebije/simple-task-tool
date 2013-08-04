using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using STT.Model.Entity;

namespace STT.Data.Memory.Test
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
            var userAccount = new UserAccount();
            
            Assert.That(repository.Find(userAccount.Key), Is.Null);

            repository.Save(userAccount);
            Assert.That(repository.Find(userAccount.Key), Is.EqualTo(userAccount));
        }

        [Test]
        public void UpdateItem()
        {
            var repository = GetRepository();
            var userAccount = new UserAccount();
            repository.Save(userAccount);

            var savedUser = repository.Find(userAccount.Key);
            Assert.That(savedUser.Username, Is.EqualTo(userAccount.Username));

            userAccount.Username = "test";
            repository.Save(userAccount);

            savedUser = repository.Find(userAccount.Key);
            Assert.That(savedUser.Username, Is.EqualTo(userAccount.Username));
        }

        [Test]
        public void DeleteItem()
        {
            var repository = GetRepository();
            var userAccount = new UserAccount();
            repository.Save(userAccount);

            Assert.That(repository.Find(userAccount.Key), Is.Not.Null);
            
            repository.Delete(userAccount);
            Assert.That(repository.Find(userAccount.Key), Is.Null);
        }

        [Test]
        public void FindItem()
        {
            var repository = GetRepository();

            var userAccount1 = new UserAccount();
            var userAccount2 = new UserAccount();

            repository.Save(userAccount1);
            repository.Save(userAccount2);

            Assert.That(repository.Find(userAccount1.Key), Is.EqualTo(userAccount1));
            Assert.That(repository.Find(userAccount2.Key), Is.EqualTo(userAccount2));
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
                Assert.That(savedItems, Contains.Item(item));
            }
        }
    }
}
