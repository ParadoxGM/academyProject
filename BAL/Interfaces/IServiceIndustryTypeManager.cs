using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Interfaces
{
    public interface IServiceIndustryTypeManager
    {
        bool Create(ServiceIndustryType serviceType);
        bool Delete(string serviceTypeid);
        ServiceIndustryType GetById(string id);
        List<ServiceIndustryType> GetAll();
        bool Update(ServiceIndustryType ser);
    }
}
