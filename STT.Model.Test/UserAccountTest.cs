﻿using System;
using NUnit.Framework;
using STT.Model.Entity;

namespace STT.Model.Test
{
    [TestFixture]
    public class UserAccountTest
    {
        [Test]
        public void CreateEmptyUserAccount()
        {
            var userAccount = new UserAccount();
            Assert.IsNotNull(userAccount);
        }

        [Test]
        public void UserAccountInheritsBaseModel()
        {
            var userAccount = new UserAccount();
            Assert.IsInstanceOf<EntityBase>(userAccount);
        }

        [Test]
        public void UserAccountHasKey()
        {
            var key = Guid.NewGuid();

            var userAccount = new UserAccount
            {
                Key = key
            };

            Assert.AreEqual(key, userAccount.Key);
        }

        [Test]
        public void CreateUserAccountViaDefaultConstructor()
        {
            const string username = "test";
            var created = DateTime.Now;
            var lastLogin = created.AddMinutes(5);
            const bool isActive = true;
            const bool isPowerUser = true;
            
            var userAccount = new UserAccount
                                  {
                                      Username = username,
                                      CreatedOn = created,
                                      LastLogin = lastLogin,
                                      IsActive = isActive,
                                      IsPowerUser = isPowerUser
                                  };

            Assert.AreNotEqual(Guid.Empty, userAccount.Key);
            Assert.AreEqual(username, userAccount.Username);
            Assert.AreEqual(created, userAccount.CreatedOn);
            Assert.AreEqual(lastLogin, userAccount.LastLogin);
            Assert.AreEqual(isActive, userAccount.IsActive);
            Assert.AreEqual(isPowerUser, userAccount.IsPowerUser);
        }

        [Test]
        public void CreateUserAccountViaNonDefaultConstructor()
        {
            const string username = "test";
            const bool isActive = true;
            const bool isPowerUser = true;

            var userAccount = new UserAccount(username, isActive, isPowerUser);

            Assert.AreNotEqual(Guid.Empty, userAccount.Key);
            Assert.AreEqual(username, userAccount.Username);
            Assert.AreEqual(isActive, userAccount.IsActive);
            Assert.AreEqual(isPowerUser, userAccount.IsPowerUser);
        }
    }
}
