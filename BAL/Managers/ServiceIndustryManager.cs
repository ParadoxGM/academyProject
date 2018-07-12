using BAL.Interfaces;
using DAL.Interfaces;
using Models.Models;
using Resources.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BAL.Managers
{

    public class ServiceIndustryManager : BaseManager, IServiceIndustryManager
    {
        IExecutorManager _exeManager;
        public ServiceIndustryManager(IUnitOfWork unitOfWork, IExecutorManager exeManager)
           : base(unitOfWork)
        { _exeManager = exeManager; }

        public List<ServiceIndustry> GetAll()
        {
            return _unitOfWork.ServRepos.GetAll();
        }

        public ServiceIndustry GetById(string id)
        {
            return _unitOfWork.ServRepos.GetById(id);
        }
        public bool Create(ServiceIndustry service)
        {
            try
            {
                _unitOfWork.ServRepos.Insert(service);
                _unitOfWork.Save();
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);

            }
            return false;
        }

        public bool Delete(string serviceid)
        {
            try
            {
                List<Executor> executors = _exeManager.GetAll().Where(x => x.ServiceIndustryId == serviceid).ToList();
                _unitOfWork.ServRepos.Delete(_unitOfWork.ServRepos.GetAll().Find(x=>x.ServiceIndustryId==serviceid));
                DeleteExecutors(executors);
                //ServiceIndustry service = _unitOfWork.ServRepos.GetAll().Find(s => s.ServiceIndustryId == serviceid);   //remove service
                //DeleteExecutors(_exeManager.Get().FindAll(e => e.ServiceIndustryId == serviceid));
                //_unitOfWork.ServRepos.Delete(service);              
                _unitOfWork.Save();
                return true;
            }
            catch (Exception e)
            {
            }
            return false;
        }

        private void DeleteExecutors(List<Executor> executors)
        {
            if (executors.Any())   //if there are any executors with this service
            {
                foreach (Executor ex in executors)
                {
                    _exeManager.Delete(ex.ExecutorId);
                }
            }
            _unitOfWork.Save();
        }

        public bool Update(ServiceIndustry ser)
        {
            try
            {
                _unitOfWork.ServRepos.Update(ser);
                _unitOfWork.Save();
                return true;
            }
            catch (Exception e)
            {
            }
            return false;
        }
        
        public IEnumerable<ServiceIndustry> Sort(IEnumerable<ServiceIndustry> services, SortStateInd sortOrder)
        {
            switch (sortOrder)
            {
                case SortStateInd.NameDesc:
                    services = services.OrderByDescending(s => s.Name);
                    break;
                case SortStateInd.TypeAsc:
                    services = services.OrderBy(s => s.serviceIndustryType.Name);
                    break;
                case SortStateInd.TypeDesc:
                    services = services.OrderByDescending(s => s.serviceIndustryType.Name);
                    break;
                default:
                    services = services.OrderBy(s => s.Name);
                    break;
            }
            return (services);
        }

    }
}
