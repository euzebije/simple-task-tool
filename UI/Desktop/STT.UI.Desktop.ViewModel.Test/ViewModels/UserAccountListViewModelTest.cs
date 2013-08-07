﻿using System.Collections.ObjectModel;
using System.Linq;
using NUnit.Framework;
using STT.Data;
using STT.Data.Memory;
using STT.Model.Entity;

namespace STT.UI.Desktop.ViewModel.Test
{
    [TestFixture]
    public class UserAccountListViewModelTest
    {
        [Test]
        public void CreateEmptyUserAccountListViewModel()
        {
            var viewModel = new UserAccountListViewModel(new RepositoryFactory());
            Assert.That(viewModel, Is.Not.Null);
        }

        [Test]
        public void UserAccountListViewModelInheritsViewModelBase()
        {
            var viewModel = new UserAccountListViewModel(new RepositoryFactory());
            Assert.That(viewModel, Is.InstanceOf<ViewModelBase>());
        }

        [Test]
        public void UserAccountListViewModelHasCollectionOfItems()
        {
            var viewModel = new UserAccountListViewModel(new RepositoryFactory());

            Assert.That(viewModel.Items, Is.Not.Null);
            Assert.That(viewModel.Items, Is.InstanceOf<ObservableCollection<UserAccountViewModel>>());
        }

        [Test]
        public void UserAccountListViewModelAddNewItem()
        {
            var eventSuccessful = false;

            var viewModel = new UserAccountListViewModel(new RepositoryFactory());
            Assert.That(viewModel.Items, Is.Empty);

            viewModel.NewOrEditStarted += (itemViewModel, dataCommand) =>
                                              {
                                                  eventSuccessful = true;
                                                  Assert.That(itemViewModel, Is.Not.Null);
                                                  Assert.That(dataCommand, Is.EqualTo(DataCommand.New));
                                                  itemViewModel.Password = "password";
                                                  itemViewModel.Save();
                                                  Assert.That(viewModel.Items, Is.Not.Empty);
                                              };

            viewModel.NewCommand.Execute(null);

            if (!eventSuccessful)
                Assert.Fail();
        }

        [Test]
        public void UserAccountListViewModelEditItem()
        {
            var eventSuccessful = false;

            var viewModel = new UserAccountListViewModel(new RepositoryFactory());
            viewModel.NewOrEditStarted += (itemViewModel, dataCommand) =>
            {
                eventSuccessful = true;
                Assert.That(itemViewModel, Is.Not.Null);
                Assert.That(itemViewModel, Is.EqualTo(viewModel.SelectedItem));
                Assert.That(dataCommand, Is.EqualTo(DataCommand.Edit));
            };

            Assert.That(viewModel.EditCommand.CanExecute(null), Is.False);

            viewModel.SelectedItem = new UserAccountViewModel(new UserAccount(), new RepositoryFactory());
            Assert.That(viewModel.EditCommand.CanExecute(null), Is.True);

            viewModel.EditCommand.Execute(null);

            if (!eventSuccessful)
                Assert.Fail();
        }

        [Test]
        public void UserAccountListViewModelDeleteItem()
        {
            var factory = new RepositoryFactory();
            var repo = factory.GetUserAccountRepository();

            var item = new UserAccount();
            repo.Save(item);

            Assert.That(repo.Find(item.Key), Is.Not.Null);

            var viewModel = new UserAccountListViewModel(factory);

            Assert.That(viewModel.DeleteCommand.CanExecute(null), Is.False);

            viewModel.SelectedItem = viewModel.Items.First();
            Assert.That(viewModel.DeleteCommand.CanExecute(null), Is.True);

            viewModel.DeleteCommand.Execute(null);

            Assert.That(viewModel.SelectedItem, Is.Null);
            Assert.That(viewModel.Items, Is.Empty);
            Assert.That(repo.Find(item.Key), Is.Null);
        }

        [Test]
        public void UserAccountListViewModelChangePassword()
        {
            var eventSuccessful = false;

            var viewModel = new UserAccountListViewModel(new RepositoryFactory());
            viewModel.ChangePasswordStarted += itemViewModel =>
            {
                eventSuccessful = true;
                Assert.That(itemViewModel, Is.Not.Null);
                Assert.That(itemViewModel, Is.EqualTo(viewModel.SelectedItem));
            };

            Assert.That(viewModel.ChangePasswordCommand.CanExecute(null), Is.False);

            viewModel.SelectedItem = new UserAccountViewModel(new UserAccount(), new RepositoryFactory());
            Assert.That(viewModel.ChangePasswordCommand.CanExecute(null), Is.True);

            viewModel.ChangePasswordCommand.Execute(null);

            if (!eventSuccessful)
                Assert.Fail();
        }
    }
}
