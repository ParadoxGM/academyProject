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

    public class ExecutorManager : BaseManager, IExecutorManager
    {
        public ExecutorManager(IUnitOfWork unitOfWork)
           : base(unitOfWork)
        { }

        public List<Executor> GetAll()
        {
            return _unitOfWork.ExecutRepos.GetAll();
        }

        public Executor GetById(string id)
        {
            return _unitOfWork.ExecutRepos.GetById(id);
        }

        public bool Create(Executor executor)
        {
            try
            {
                _unitOfWork.ExecutRepos.Insert(executor);
                _unitOfWork.Save();
                return true;
            }
            catch (Exception e)
            {
               throw new Exception(e.Message); 
            }
            return false;
        }

        public bool Delete(string executorid)
        {
            try
            {
                Executor executor = _unitOfWork.ExecutRepos.GetAll().Find(e => e.ExecutorId == executorid);
                _unitOfWork.ExecutRepos.Delete(executor);
                _unitOfWork.Save();

                return true;
            }
            catch (Exception e)
            {
            }
            return false;

        }

        public bool Update(Executor ex)
        {
            try
            {
                _unitOfWork.ExecutRepos.Update(ex);
                _unitOfWork.Save();
                return true;
            }
            catch (Exception e)
            {
            }
            return false;
        }

        public IEnumerable<Executor> Sort(IEnumerable<Executor> executors, SortStateExe sortOrder)
        {
            switch (sortOrder)
            {
                case SortStateExe.TimeDesc:
                    executors = executors.OrderByDescending(s => s.CreationTime);
                    break;
                case SortStateExe.PriceAsc:
                    executors = executors.OrderBy(s => s.Price);
                    break;
                case SortStateExe.PriceDesc:
                    executors = executors.OrderByDescending(s => s.Price);
                    break;
                case SortStateExe.CustomerAsc:
                    executors = executors.OrderBy(s => s.Customer.Name);
                    break;
                case SortStateExe.CustomerDesc:
                    executors = executors.OrderByDescending(s => s.Customer.Name);
                    break;
                case SortStateExe.ServiceIndustryAsc:
                    executors = executors.OrderBy(s => s.ServiceIndustry.Name);
                    break;
                case SortStateExe.ServiceIndustryDesc:
                    executors = executors.OrderByDescending(s => s.ServiceIndustry.Name);
                    break;
                default:
                    executors = executors.OrderBy(s => s.CreationTime);
                    break;
            }
            return executors;
        }

        public IEnumerable<Executor> Search(IEnumerable<Executor> executors, string option, string search)
        {
            switch (option)
            {
                case "Creation Time":
                    executors = executors.Where(x => (x.CreationTime.ToString().ToLower() == search.ToLower()) || (x.CreationTime.ToString().ToLower().StartsWith(search.ToLower())));
                    break;
                case "Price":
                    executors = executors.Where(x => (x.Price.ToString().ToLower() == search.ToLower()) || (x.Price.ToString().ToLower().StartsWith(search.ToLower())));
                    break;
                case "Service Industry":
                    executors = executors.Where(x => (x.ServiceIndustry.Name.ToLower() == search.ToLower()) || (x.ServiceIndustry.Name.ToLower().StartsWith(search.ToLower())));
                    break;
                case "Customer":
                    executors = executors.Where(x => (x.Customer.Name.ToLower() == search.ToLower()) || (x.Customer.Name.ToLower().StartsWith(search.ToLower())));
                    break;
                default:
                    executors = executors.Where(x => x.ServiceIndustry.Name.StartsWith(search));
                    break;
            }
            return executors;
        }

    }
}
