using DAL.Repositories;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IBaseRepository<Customer> CustRepos { get; }
        IBaseRepository<ServiceIndustry> ServRepos { get; }
        IBaseRepository<Executor> ExecutRepos { get; }
        IBaseRepository<ServiceIndustryType> ServTypeRepos { get; }
        int Save();
    }
}
