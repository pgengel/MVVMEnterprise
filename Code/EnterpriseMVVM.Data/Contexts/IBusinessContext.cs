using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnterpriseMVVM.Data.Models;

namespace EnterpriseMVVM.Data.Contexts
{
    public interface IBusinessContext
    {
        void CreateCustomer(Customer customer);
        void DeleteCustomer(Customer customer);
        void UpdateCustomer(Customer customer);
        ICollection<Customer> GetCustomerList();
    }
}
