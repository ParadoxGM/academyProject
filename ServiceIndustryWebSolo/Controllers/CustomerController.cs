using BAL.Interfaces;
using BAL.Managers;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Models.Models;
using ServiceIndustryWebSolo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ServiceIndustryWebSolo.Controllers
{
    [OutputCache(VaryByParam = "*", Duration = 0, NoStore = true)]
    public class CustomerController : Controller
    {
        ICustomerManager _custManager;


        ServiceIndustryWebSolo.App_Start.CustomerManager d;
        public ServiceIndustryWebSolo.App_Start.CustomerManager D
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ServiceIndustryWebSolo.App_Start.CustomerManager>();
            }
        }

        private CustomerManager _userManager;
        public CustomerManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<CustomerManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        public CustomerController(ICustomerManager custManager)
        {
            _custManager = custManager;
        }

        public ActionResult Index()
        {
            List<IndexViewModel> list = new List<IndexViewModel>();
            foreach (Customer c in _custManager.GetAll())
            {
                list.Add(IndexViewModel.CustomerToIVM(c));
            }
            return View(list);
        }


        [HttpGet]
        public ActionResult Delete(string email)//refactor
        {
            Customer c = _custManager.GetAll().Find(rc => rc.Email == email);
            
            return View(IndexViewModel.CustomerToIVM(c));
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(string email)
        {
            _custManager.Delete(_custManager.GetAll().Find(x => x.Email == email).Id);
            return RedirectToAction("Index");
        }
    }
}