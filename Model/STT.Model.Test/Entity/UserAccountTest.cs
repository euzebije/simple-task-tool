using System;
using NUnit.Framework;
using STT.Model.Entity;

namespace STT.Model.Test.Entity
{
    [TestFixture]
    public class UserAccountTest
    {
        [Test]
        public void CreateEmptyUserAccount()
        {
            var userAccount = new UserAccount();
            Assert.That(userAccount, Is.Not.Null);
        }

        [Test]
        public void UserAccountInheritsBaseModel()
        {
            var userAccount = new UserAccount();
            Assert.That(userAccount, Is.InstanceOf<EntityBase>());
        }

        [Test]
        public void UserAccountHasKey()
        {
            var key = Guid.NewGuid();

            var userAccount = new UserAccount
            {
                Key = key
            };

            Assert.That(userAccount.Key, Is.EqualTo(key));
        }

        [Test]
        public void CreateUserAccountViaDefaultConstructor()
        {
            const string username = "test";
            const string password = "password";
            const string salt = "salt";
            var created = DateTime.Now;
            var lastLogin = created.AddMinutes(5);
            const bool isActive = true;
            const bool isPowerUser = true;
            
            var userAccount = new UserAccount
                                  {
                                      Username = username,
                                      Password = password,
                                      PasswordSalt = salt,
                                      CreatedOn = created,
                                      LastLogin = lastLogin,
                                      IsActive = isActive,
                                      IsPowerUser = isPowerUser
                                  };

            Assert.That(userAccount.Key, Is.Not.EqualTo(Guid.Empty));
            Assert.That(userAccount.Username, Is.EqualTo(username));
            Assert.That(userAccount.Password, Is.EqualTo(password));
            Assert.That(userAccount.PasswordSalt, Is.EqualTo(salt));
            Assert.That(userAccount.CreatedOn, Is.EqualTo(created));
            Assert.That(userAccount.LastLogin, Is.EqualTo(lastLogin));
            Assert.That(userAccount.IsActive, Is.EqualTo(isActive));
            Assert.That(userAccount.IsPowerUser, Is.EqualTo(isPowerUser));
        }

        [Test]
        public void CreateUserAccountViaNonDefaultConstructor()
        {
            const string username = "test";
            const string password = "password";
            const bool isActive = true;
            const bool isPowerUser = true;

            var userAccount = new UserAccount(username, password, isActive, isPowerUser);

            Assert.That(userAccount.Key, Is.Not.EqualTo(Guid.Empty));
            Assert.That(userAccount.Username, Is.EqualTo(username));
            Assert.That(userAccount.IsActive, Is.EqualTo(isActive));
            Assert.That(userAccount.IsPowerUser, Is.EqualTo(isPowerUser));
            Assert.That(userAccount.IsPasswordValid(password), Is.True);
        }

        [Test]
        public void UserAccountChangePassword()
        {
            const string password = "password";

            var userAccount = new UserAccount("test", password, true, false);
            Assert.That(userAccount.IsPasswordValid(password), Is.True);

            const string newPassword = "new_password";
            userAccount.ChangePassword(password, newPassword);
            Assert.That(userAccount.IsPasswordValid(newPassword), Is.True);
        }
    }
}
