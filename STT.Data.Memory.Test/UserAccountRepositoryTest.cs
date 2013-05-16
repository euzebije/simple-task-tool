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
            Assert.IsNotNull(repository);
        }

        [Test]
        public void InsertItem()
        {
            var repository = GetRepository();
            var userAccount = new UserAccount();
            
            Assert.IsNull(repository.Find(userAccount.Key));

            repository.Save(userAccount);
            Assert.AreEqual(userAccount, repository.Find(userAccount.Key));
        }

        [Test]
        public void UpdateItem()
        {
            var repository = GetRepository();
            var userAccount = new UserAccount();
            repository.Save(userAccount);

            var savedUser = repository.Find(userAccount.Key);
            Assert.AreEqual(userAccount.Username, savedUser.Username);

            userAccount.Username = "test";
            repository.Save(userAccount);

            savedUser = repository.Find(userAccount.Key);
            Assert.AreEqual(userAccount.Username, savedUser.Username);
        }

        [Test]
        public void DeleteItem()
        {
            var repository = GetRepository();
            var userAccount = new UserAccount();
            repository.Save(userAccount);

            Assert.IsNotNull(repository.Find(userAccount.Key));
            
            repository.Delete(userAccount);
            Assert.IsNull(repository.Find(userAccount.Key));
        }

        [Test]
        public void FindItem()
        {
            var repository = GetRepository();

            var userAccount1 = new UserAccount();
            var userAccount2 = new UserAccount();

            repository.Save(userAccount1);
            repository.Save(userAccount2);

            Assert.AreEqual(userAccount1, repository.Find(userAccount1.Key));
            Assert.AreEqual(userAccount2, repository.Find(userAccount2.Key));
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
                Assert.Contains(item, savedItems);
            }
        }
    }
}
