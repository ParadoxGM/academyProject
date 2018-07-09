using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BAL.Interfaces;
using Models.Models;

namespace ServiceIndustryWebSolo.Controllers
{
    [OutputCache(VaryByParam = "*", Duration = 0, NoStore = true)]
    public class ServiceIndustryTypeController : Controller
    {
        IServiceIndustryTypeManager _servTypeManager;
        public ServiceIndustryTypeController(IServiceIndustryTypeManager servTypeManager)
        {
            _servTypeManager = servTypeManager;
        }

        public ActionResult Index()
        {
            List<ServiceIndustryType> serviceIndustryTypes = _servTypeManager.GetAll();
            if (!serviceIndustryTypes.Any())
            {
                return RedirectToAction("NoServTypesYet");
            }
            else
                return View(serviceIndustryTypes);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ActionName("Create")]
        public ActionResult CreatePost(ServiceIndustryType serviceIndustryType)
        {
            serviceIndustryType.ServiceIndustryTypeId = Guid.NewGuid().ToString();
            _servTypeManager.Create(serviceIndustryType);
            return RedirectToAction("Index");
        }


        [HttpGet]
        public ActionResult Delete(string id)
        {
            return View(_servTypeManager.GetById(id));
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(string id)
        {
            _servTypeManager.Delete(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(string id)
        {
           return View(_servTypeManager.GetById(id));
        }

        [HttpPost]
        public ActionResult Edit(ServiceIndustryType serviceIndustryType)
        {
            _servTypeManager.Update(serviceIndustryType);
            return RedirectToAction("Index");
        }

        public ActionResult NoServTypesYet()
        {
            return View();
        }

    }
}