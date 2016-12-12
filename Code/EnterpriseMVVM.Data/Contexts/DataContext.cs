using System.Data.Entity;
using EnterpriseMVVM.Data.Models;

namespace EnterpriseMVVM.Data
{
    public class DataContext : DbContext
    {
        public DataContext() : base("Default")
        {
        }

        public DbSet<Customer> Customers { get; set; }
    }
}
