using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using EnterpriseMVVM.Data;
using EnterpriseMVVM.Data.Contexts;
using EnterpriseMVVM.Data.Models;
using EnterpriseMVVM.DesktopClient.ViewModels;
using EnterpriseMVVM.Windows;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EnterpriseMVVM.Data.Tests;
using Moq;

namespace EnterpriseMVVMDesktopClient.Tests.UnitTests
{
    /// <summary>
    /// Summary description for MainViewModelTests
    /// </summary>
    [TestClass]
    public class MainViewModelTests 
    {
        private Mock<IBusinessContext> mock;
        private List<Customer> store;

        [TestInitialize]
        public void TestInitialize()
        {
            store  = new List<Customer>();
            mock = new Mock<IBusinessContext>();

            mock.Setup(m => m.GetCustomerList()).Returns(store);
            mock.Setup(m => m.CreateCustomer(It.IsAny<Customer>())).Callback<Customer>(customer => store.Add(customer));
            mock.Setup(m => m.DeleteCustomer(It.IsAny<Customer>())).Callback<Customer>(customer => store.Remove(customer));
            mock.Setup(m => m.UpdateCustomer(It.IsAny<Customer>())).Callback<Customer>(customer =>
            {
                int i = store.IndexOf(customer);
                store[i] = customer;
            });

        }

        [TestMethod]
        public void IsViewModel()
        {
            Assert.IsTrue(typeof(MainViewModel).BaseType == typeof(ViewModel));
        }


        [TestMethod]
        public void AddCustomerCommandCannotExecuteWhenFirstNameIsNotValid()
        {
            var viewModel = new MainViewModel(mock.Object)
            {
                SelectedCustomer = new Customer()
                {
                    FirstName = null,
                    LastName = "Anderson",
                    Email = "noreply@msdn.com"
                } 
            };

            Assert.IsFalse(viewModel.AddCustomerCommand.CanExecute(null));
        }

        [TestMethod]
        public void AddCustomerCommandCannotExecuteWhenLasttNameIsNotValid()
        {
            var viewModel = new MainViewModel(mock.Object)
            {
                SelectedCustomer = new Customer()
                {
                    FirstName = "David",
                    LastName = null,
                    Email = "noreply@msdn.com"
                }
            };

            Assert.IsFalse(viewModel.AddCustomerCommand.CanExecute(null));
        }

        [TestMethod]
        public void AddCustomerCommandCannotExecuteWhenFEmailIsNotValid()
        {
            var viewModel = new MainViewModel(mock.Object)
            {
                SelectedCustomer = new Customer()
                {
                    FirstName = "David",
                    LastName = "Anderson",
                    Email = null
                }
            };

            Assert.IsFalse(viewModel.AddCustomerCommand.CanExecute(null));
        }

        [TestMethod]
        public void AddCustomerCommandAddsCustomerToCustomerCollectionWhenExecuteSuccessfully()
        {
            var viewModel = new MainViewModel(mock.Object)
            {
                SelectedCustomer = new Customer()
                {
                    FirstName = "David",
                    LastName = "Anderson",
                    Email = "noreply@msdn.com"
                }
            };

            viewModel.AddCustomerCommand.Execute();
            Assert.IsTrue(viewModel.Customers.Count == 1);
        }

        [TestMethod]
        public void GetCustomerListCommandPopulateCustomersProperty()
        {

            mock.Object.CreateCustomer(new Customer() { Email = "1@1.com", FirstName = "1", LastName = "a" });
            mock.Object.CreateCustomer(new Customer() { Email = "2@2.com", FirstName = "2", LastName = "b" });
            mock.Object.CreateCustomer(new Customer() { Email = "3@3.com", FirstName = "3", LastName = "c" });

            var viewModel = new MainViewModel(mock.Object);

            viewModel.GetCustomerListCommand.Execute(null);

            Assert.IsTrue(viewModel.Customers.Count == 3);
            
        }

        [TestMethod]
        public void SaveCommand_InvokdesIBusinessContextupdateCustomerMethod()
        {
            //arrange
            mock.Object.CreateCustomer(new Customer {Email = "1@1.com", FirstName = "1", LastName = "A"});

            var viewModel = new MainViewModel(mock.Object);

            viewModel.GetCustomerListCommand.Execute(null);
            viewModel.SelectedCustomer = viewModel.Customers.First();

            //act
            viewModel.SelectedCustomer.Email = "newValue";
            viewModel.SaveCustomerCommand.Execute(null);

            //assert
            mock.Verify(m => m.UpdateCustomer(It.IsAny<Customer>()), Times.Once);
        }

        [TestMethod]
        public void DeleteCommand_InvokesIBusinessContextDeleteCustomerMethod()
        {
            //arrange
            mock.Object.CreateCustomer(new Customer { Email = "1@1.com", FirstName = "1", LastName = "A" });

            var viewModel = new MainViewModel(mock.Object);
            viewModel.GetCustomerListCommand.Execute(null);
            viewModel.SelectedCustomer = viewModel.Customers.First();
                
            //act
            viewModel.DeleteCustomerCommand.Execute(null);

            //assert
            mock.Verify(m => m.DeleteCustomer(It.IsAny<Customer>()), Times.Once);

        }

        [TestMethod]
        public void PropertyChanged_IsRaisedForSelectedCustomerWhenSelectedCustomerPropertyHasChanged()
        {
            var viewModel = new MainViewModel(mock.Object);

            bool eventRaised = false;

            viewModel.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == "SelectedCustomer")
                    eventRaised = true;
            };

            viewModel.SelectedCustomer = null;

            Assert.IsTrue(eventRaised);
        }
    }
}
