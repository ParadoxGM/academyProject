using BAL.Interfaces;
using BAL.Managers;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using Resources.Enums;

namespace ServiceIndustryWebSolo.Controllers
{
    [OutputCache(VaryByParam = "*", Duration = 0, NoStore = true)]
    public class ExecutorController : Controller
    {
        IExecutorManager _exeManager;
        IServiceIndustryManager _servIndManager;
        IServiceIndustryTypeManager _servIndTypeManager;
        ICustomerManager _custManager;

        public ExecutorController(ICustomerManager custManager, IExecutorManager exeManager, IServiceIndustryManager servIndManager, IServiceIndustryTypeManager servIndTypeManager)
        {
            _custManager = custManager;
            _exeManager = exeManager;
            _servIndManager = servIndManager;
            _servIndTypeManager = servIndTypeManager;
        }

        public ActionResult Index(string option, string search, int? pageNumber, SortStateExe sortOrder = SortStateExe.TimeAsc)
        {
            if (!_exeManager.GetAll().Any())
            {
                return RedirectToAction("NoExecutorsYet");
            }
            else
            {
                IEnumerable<Executor> executors = _exeManager.GetAll();
                if (search != null)
                {
                    executors = _exeManager.Search(executors, option, search);
                }
                ViewData["TimeSort"] = sortOrder == SortStateExe.TimeAsc ? SortStateExe.TimeDesc : SortStateExe.TimeAsc;
                ViewData["PriceSort"] = sortOrder == SortStateExe.PriceAsc ? SortStateExe.PriceDesc : SortStateExe.PriceAsc;
                ViewData["CustomerSort"] = sortOrder == SortStateExe.CustomerAsc ? SortStateExe.CustomerDesc : SortStateExe.CustomerAsc;
                ViewData["ServiceIndustrySort"] = sortOrder == SortStateExe.ServiceIndustryAsc ? SortStateExe.ServiceIndustryDesc : SortStateExe.ServiceIndustryAsc;

                return View(_exeManager.Sort(executors, sortOrder).ToPagedList(pageNumber ?? 1, 10));
            }
        }

        public ActionResult ExeListByServId(string typeid, int? pageNumber)
        {
            var item = _exeManager.GetAll().Find(x => x.ServiceIndustryId == typeid);
            if (item != null)
            {
                ViewBag.Name = item.ServiceIndustry.Name;
                return View("Index", _exeManager.GetAll().Where(x => x.ServiceIndustryId == typeid).ToPagedList(pageNumber ?? 1, 10));
            }
            else
                return RedirectToAction("NoExecutorsYet");
        }

        public ActionResult ExeListByCustId(string email, int? pageNumber)
        {
            var item = _exeManager.GetAll().Find(x => x.Customer.Email == email);
            if (item != null)
            {
                ViewBag.Name = item.Customer.UserName;
                return View("Index", _exeManager.GetAll().Where(x => x.Customer.Email == email).ToPagedList(pageNumber ?? 1, 10));
            }
            else
                return RedirectToAction("NoExecutorsYet");
        }

        public ActionResult ExecutorInfo(string id)
        {
            Executor ex = _exeManager.GetById(id);
            if (ex == null)
                return RedirectToAction("Index");
            return View(ex);
        }


        [HttpGet]
        public ActionResult Create()
        {
            SelectList types = new SelectList(_servIndTypeManager.GetAll().ToList(), "Name", "Name");
            SelectList services = new SelectList(_servIndManager.GetAll().ToList(), "Name", "Name");

            ViewBag.ServiceInds = services;
            ViewBag.Types = types;
            return View();
        }
        [HttpPost]
        [ActionName("Create")]
        public ActionResult CreatePost(Executor executor)
        {
            try
            {
                Customer cu = _custManager.GetAll().FirstOrDefault(c => c.Email == User.Identity.Name);
                ServiceIndustry service = _servIndManager.GetAll().FirstOrDefault(s => s.Name == executor.ServiceIndustry.Name);
                executor.Customer = cu;
                executor.CustomerId = cu.Id;
                executor.ServiceIndustry = service;
                executor.ServiceIndustryId = service.ServiceIndustryId;
                service.Executors.Add(executor);
                cu.Executors.Add(executor);

                executor.ExecutorId = Guid.NewGuid().ToString();
                executor.CreationTime = DateTime.UtcNow;
                _exeManager.Create(executor);
            }
            catch (Exception e)
            {
                RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }


        [HttpGet]
        public ActionResult Delete(string id)
        {
            return View(_exeManager.GetById(id));
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(string id)
        {
            _exeManager.Delete(id);
            return RedirectToAction("Index");
        }


        [HttpGet]
        public ActionResult Edit(string id)
        {
            return View(_exeManager.GetById(id));
        }

        [HttpPost]
        public ActionResult Edit(Executor executor)
        {
            executor.Customer = _custManager.GetById(executor.CustomerId);
            executor.ServiceIndustry = _servIndManager.GetById(executor.ServiceIndustryId);
            _exeManager.Update(executor);
            return RedirectToAction("Index");
        }

        public ActionResult NoExecutorsYet()
        {
            return View();
        }
    }

}