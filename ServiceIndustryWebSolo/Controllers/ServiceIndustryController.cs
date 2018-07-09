using BAL.Interfaces;
using Models.Models;
using Resources.Enums;
using ServiceIndustryWebSolo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace ServiceIndustryWebSolo.Controllers
{
    [OutputCache(VaryByParam = "*", Duration = 0, NoStore = true)]
    public class ServiceIndustryController : Controller
    {
        IServiceIndustryManager _servManager;
        IServiceIndustryTypeManager _servTypeManager;
        public ServiceIndustryController(IServiceIndustryManager servManager, IServiceIndustryTypeManager servTypeManager)
        {
            _servTypeManager = servTypeManager;
            _servManager = servManager;
        }

        public ActionResult Index(SortStateInd sortOrder = SortStateInd.NameAsc)
        {
            if (!_servManager.GetAll().Any())
            {
                return RedirectToAction("NoServicesYet");
            }
            else
            { 
                ViewData["NameSort"] = sortOrder == SortStateInd.NameAsc ? SortStateInd.NameDesc : SortStateInd.NameAsc;
                ViewData["TypeSort"] = sortOrder == SortStateInd.TypeAsc ? SortStateInd.TypeDesc : SortStateInd.TypeAsc;
                
                return View(_servManager.Sort(_servManager.GetAll(), sortOrder));
            }
        }
        
        public ActionResult ServListByTypeId(string typeid)
        {
            List<ServiceIndustry> serviceIndustries = _servManager.GetAll().Where(x => x.ServiceIndustryTypeId == typeid).ToList();
            if (serviceIndustries.Any())
            {
                ViewBag.ServiceIndName = serviceIndustries.FirstOrDefault().serviceIndustryType.Name;
                return View("Index", serviceIndustries);
            }
            else            
                return RedirectToAction("NoServicesYet");
            
        }
        
        [HttpGet]
        public ActionResult Create()
        {
            SelectList types = new SelectList(_servTypeManager.GetAll().ToList(), "Name", "Name");
            ViewBag.Types = types;
            return View();
        }
        [HttpPost]
        [ActionName("Create")]
        public ActionResult CreatePost(ServiceIndustry serviceIndustry)
        {
            try
            {
                ServiceIndustryType serviceIndustryType = _servTypeManager.GetAll().Find(s => s.Name == serviceIndustry.serviceIndustryType.Name);
                serviceIndustry.ServiceIndustryId = Guid.NewGuid().ToString();
                serviceIndustry.ServiceIndustryTypeId = serviceIndustryType.ServiceIndustryTypeId;
                serviceIndustry.serviceIndustryType = serviceIndustryType;
                _servManager.Create(serviceIndustry);
            }
            catch(Exception e)
            {
                RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }


        [HttpGet]
        public ActionResult Delete(string id)
        {
            return View(_servManager.GetById(id));
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(string id)
        {
            _servManager.Delete(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(string id)
        {
                return View(_servManager.GetById(id));
        }

        [HttpPost]
        public ActionResult Edit(ServiceIndustry serviceIndustry)
        {
            serviceIndustry.serviceIndustryType = _servTypeManager.GetById(serviceIndustry.ServiceIndustryTypeId);
            _servManager.Update(serviceIndustry);
            return RedirectToAction("Index");
        }


        public ActionResult NoServicesYet()
        {
            return View();
        }
    }
}