using Models.Models;
using Resources.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Interfaces
{
    public interface IServiceIndustryManager
    {
        bool Create(ServiceIndustry service);
        bool Delete(string serviceid);
        ServiceIndustry GetById(string id);
        List<ServiceIndustry> GetAll();
        bool Update(ServiceIndustry ser);
        IEnumerable<ServiceIndustry> Sort(IEnumerable<ServiceIndustry> services, SortStateInd sortOrder);
    }
}
