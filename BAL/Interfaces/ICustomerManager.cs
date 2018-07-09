using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Interfaces
{
    public interface ICustomerManager
    {
        bool Create(Customer executor);
        bool Delete(string executorid);
        Customer GetById(string id);
        List<Customer> GetAll();
        bool Update(Customer ex);
    }
}
