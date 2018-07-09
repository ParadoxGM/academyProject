using Models.Models;
using Resources.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Interfaces
{
    public interface IExecutorManager
    {
        bool Create(Executor executor);
        bool Delete(string executorid);
        Executor GetById(string id);
        List<Executor> GetAll();
        bool Update(Executor ex);
        IEnumerable<Executor> Sort(IEnumerable<Executor> executors, SortStateExe sortOrder);
        IEnumerable<Executor> Search(IEnumerable<Executor> executors, string option, string search);

    }
}
