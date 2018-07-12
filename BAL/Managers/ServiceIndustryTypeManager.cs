using BAL.Interfaces;
using DAL.Interfaces;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BAL.Managers
{

    public class ServiceIndustryTypeManager : BaseManager, IServiceIndustryTypeManager
    {
        IServiceIndustryManager _servManager;
        public ServiceIndustryTypeManager(IUnitOfWork unitOfWork, IServiceIndustryManager servManager)
           : base(unitOfWork)
        { _servManager = servManager; }

        public List<ServiceIndustryType> GetAll()
        {
            return _unitOfWork.ServTypeRepos.GetAll();
        }
        
        public ServiceIndustryType GetById(string id)
        {
            return _unitOfWork.ServTypeRepos.GetById(id);
        }

        public bool Create(ServiceIndustryType serviceType)
        {
            try
            {
                _unitOfWork.ServTypeRepos.Insert(serviceType);
                _unitOfWork.Save();
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return false;
        }

        public bool Delete(string serviceTypeid)
        {
            try
            {
                List<ServiceIndustry> services = _servManager.GetAll().Where(e => e.ServiceIndustryTypeId == serviceTypeid).ToList();
                _unitOfWork.ServTypeRepos.Delete(_unitOfWork.ServTypeRepos.GetAll().Find(st => st.ServiceIndustryTypeId == serviceTypeid));
                DeleteServInd(services);
                _unitOfWork.Save();
                return true;
            }
            catch (Exception e)
            {
            }
            return false;
        }
        private void DeleteServInd(List<ServiceIndustry> services)
        {
            if (services.Any())   //if there are any executors with this service
            {
                foreach (ServiceIndustry ex in services)
                {
                    _servManager.Delete(ex.ServiceIndustryId);
                }
            }
            _unitOfWork.Save();
        }
        public bool Update(ServiceIndustryType ser)
        {
            try
            {
                _unitOfWork.ServTypeRepos.Update(ser);
                _unitOfWork.Save();
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return false;
        }

    }
}
