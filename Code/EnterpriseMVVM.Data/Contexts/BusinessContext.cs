using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnterpriseMVVM.Data.Models;

namespace EnterpriseMVVM.Data.Contexts
{
    public sealed class  BusinessContext : IDisposable, IBusinessContext
    {
        private readonly DataContext context;
        private bool dispose;
        public BusinessContext()
        {
            context = new DataContext();
        }

        public DataContext DataContext 
        {
            get { return context; }
        }

        public void CreateCustomer(Customer customer)
        {
            Check.Require(customer.Email);
            Check.Require(customer.FirstName);
            Check.Require(customer.LastName);

            context.Customers.Add(customer);
            context.SaveChanges();

        }

        public void UpdateCustomer(Customer customer)
        {
            Customer entity = context.Customers.Find(customer.Id);

            if (entity == null)
            {
                throw new NotImplementedException("Need to handle this!");
            }

            context.Entry(customer).CurrentValues.SetValues(customer);
            context.SaveChanges();
        }

        public void DeleteCustomer(Customer customer)
        {
            context.Customers.Remove(customer);
            context.SaveChanges();
        }

        public ICollection<Customer> GetCustomerList()
        {
            return context.Customers.OrderBy(p => p.Id).ToArray();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (dispose || disposing)
            {
                return;
            }

            if (context != null)
            {
                context.Dispose();
            }

            dispose = true;
        }

        static class Check
        {
            public static void Require(string value)
            {
                if (value == null)
                    throw new ArgumentNullException();
                else if (value.Trim().Length == 0)
                    throw new ArgumentNullException();
            }



        }
    }


}
