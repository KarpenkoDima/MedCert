using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp1.Models;

namespace WindowsFormsApp1.Data.Repositories
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        List<Customer> Search(string searchText);
    }
}
