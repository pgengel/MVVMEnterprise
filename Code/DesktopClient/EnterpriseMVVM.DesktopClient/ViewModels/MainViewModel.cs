using EnterpriseMVVM.Data.Contexts;
using EnterpriseMVVM.Data.Models;
using EnterpriseMVVM.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace EnterpriseMVVM.DesktopClient.ViewModels
{
    public class MainViewModel : ViewModel
    {
        private readonly IBusinessContext context;
        private Customer selectedCustomer;

        public MainViewModel(IBusinessContext context)
        {
            this.context = context;
            Customers = new ObservableCollection<Customer>();
        }

        public ICollection<Customer> Customers { get; private set; }

        public ICommand GetCustomerListCommand
        {
            get
            {
                return new ActionCommand(p => GetCustomerList());
            }
        }

        public Customer SelectedCustomer
        {
            get { return selectedCustomer; }
            set
            {
                selectedCustomer = value;
                NotifyPropertyChanged();
            }
        }

        public bool IsValid
        {
            get
            {
                return SelectedCustomer == null ||
                       (!String.IsNullOrWhiteSpace(SelectedCustomer.FirstName) &&
                       !String.IsNullOrWhiteSpace(SelectedCustomer.LastName) &&
                       !String.IsNullOrWhiteSpace(SelectedCustomer.Email));

            }
        }

        public ActionCommand AddCustomerCommand
        {
            get
            {
                return new ActionCommand(p => AddCustomer(),
                                         p => IsValid);
            }
        }

        public ActionCommand SaveCustomerCommand
        {
            get
            {
                return new ActionCommand(p => SaveCustomer(),
                                         p => IsValid);
            }
        }

        public ICommand DeleteCustomerCommand
        {
            get
            {
                return new ActionCommand(p => DeleteCustomer());
            }
        }



        private void AddCustomer()
        {

            Customer customer = new Customer
            {
                FirstName = "FirstName",
                LastName = "LastName",
                Email = "Email"
            };

            try
            {
                context.CreateCustomer(customer);
            }
            catch (Exception e)
            {
               
                throw;
            }

            Customers.Add(SelectedCustomer);

        }

        private void GetCustomerList()
        {
            Customers.Clear();

            foreach (var customer in context.GetCustomerList())
                Customers.Add(customer);
        }

        private void SaveCustomer()
        {
            context.UpdateCustomer(SelectedCustomer);
        }

        private void DeleteCustomer()
        {
            context.DeleteCustomer(SelectedCustomer);
            Customers.Remove(SelectedCustomer);
            SelectedCustomer = null;
        }

    }
}
