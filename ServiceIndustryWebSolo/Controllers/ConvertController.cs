using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BAL.Interfaces;
using Models.Models;
using ServiceIndustryWebSolo.Attributes;
using ServiceIndustryWebSolo.Manager;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace ServiceIndustryWebSolo.Controllers
{
    [CustomAuthorize(Roles = "admin")]
    [OutputCache(VaryByParam = "*", Duration = 0, NoStore = true)]
    public class ConvertController : Controller
    {
        IExecutorManager _exeManager;
        IServiceIndustryManager _servIndManager;
        IServiceIndustryTypeManager _servIndTypeManager;
        ICustomerManager _custManager;
        readonly string contentType = "Application/json";
        readonly string directory = @"D:\PROGRAMMING\AcademySS\ServiceIndustryWebSolo\ServiceIndustryWebSolo\ServiceIndustryWebSolo\ConvertFiles\JsonFilesForDB";
        readonly string directoryDBToJson = @"D:\PROGRAMMING\AcademySS\ServiceIndustryWebSolo\ServiceIndustryWebSolo\ServiceIndustryWebSolo\ConvertFiles\JsonsFromDB\";

        public ConvertController(ICustomerManager custManager, IExecutorManager exeManager, IServiceIndustryManager servIndManager, IServiceIndustryTypeManager servIndTypeManager)
        {
            _custManager = custManager;
            _exeManager = exeManager;
            _servIndManager = servIndManager;
            _servIndTypeManager = servIndTypeManager;
        }
        public ActionResult Index(string statusMesage)
        {
            ViewBag.status = statusMesage;
            return View();
        }

        public ActionResult DBToJsonCust()
        {
            JsonManager<Customer> jman = new JsonManager<Customer>();
            jman.SaveToFile(_custManager.GetAll(), directoryDBToJson, "CustomersFromDB");

            Response.ContentType = contentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=CustomersFromDB.json");
            Response.TransmitFile(Server.MapPath("~/ConvertFiles/JsonsFromDB/CustomersFromDB.json"));
            Response.End();

            return View("Index");
        }
        public ActionResult DBToJsonExe()
        {
            JsonManager<Executor> jman = new JsonManager<Executor>();
            jman.SaveToFile(_exeManager.GetAll(), directoryDBToJson, "ExecutorsFromDB");

            Response.ContentType = "Application/json";
            Response.AppendHeader("Content-Disposition", "attachment; filename=ExecutorsFromDB.json");
            Response.TransmitFile(Server.MapPath("~/ConvertFiles/JsonsFromDB/ExecutorsFromDB.json"));
            Response.End();

            return View("Index");
        }
        public ActionResult DBToJsonServ()
        {
            JsonManager<ServiceIndustry> jman = new JsonManager<ServiceIndustry>();//remove it
            jman.SaveToFile(_servIndManager.GetAll(), directoryDBToJson, "ServiceIndustryFromDB");

            Response.ContentType = "Application/json";
            Response.AppendHeader("Content-Disposition", "attachment; filename=ServiceIndustryFromDB.json");
            Response.TransmitFile(Server.MapPath("~/ConvertFiles/JsonsFromDB/ServiceIndustryFromDB.json"));
            Response.End();
            return View("Index");
        }
        public ActionResult DBToJsonServTypes()
        {
            JsonManager<ServiceIndustryType> jman = new JsonManager<ServiceIndustryType>();
            jman.SaveToFile(_servIndTypeManager.GetAll(), directoryDBToJson, "ServiceIndustryTypesFromDB");

            Response.ContentType = "Application/json";
            Response.AppendHeader("Content-Disposition", "attachment; filename=ServiceIndustryTypesFromDB.json");
            Response.TransmitFile(Server.MapPath("~/ConvertFiles/JsonsFromDB/ServiceIndustryTypesFromDB.json"));
            Response.End();
            return View("Index");
        }

        //to db

        [HttpPost]
        public ActionResult JsonToDBCust(HttpPostedFileBase file)
        {
            string status;
            try
            {
                if (file != null && file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    file.SaveAs(Path.Combine(directory, fileName));
                }
                JsonManager<Customer> jman = new JsonManager<Customer>();
                List<Customer> customers = jman.ReadFile(@"D:\PROGRAMMING\AcademySS\ServiceIndustryWebSolo\ServiceIndustryWebSolo\ServiceIndustryWebSolo\ConvertFiles\JsonFilesForDB\", file.FileName);

                foreach (Customer c in customers)
                {
                    c.Executors = null;
                    _custManager.Create(c);
                }
                status = "Success";
            }
            catch (Exception e)
            {
                status = e.Message;
            }
            return RedirectToAction("Index", new { statusMesage = status });
        }
        [HttpPost]
        public ActionResult JsonToDBExe(HttpPostedFileBase file)
        {
            string status;
            try
            {
                if (file != null && file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    file.SaveAs(Path.Combine(directory, fileName));
                }
                JsonManager<Executor> jman = new JsonManager<Executor>();
                List<Executor> customers = jman.ReadFile(@"D:\PROGRAMMING\AcademySS\ServiceIndustryWebSolo\ServiceIndustryWebSolo\ServiceIndustryWebSolo\ConvertFiles\JsonFilesForDB\", file.FileName);

                foreach (Executor c in customers)
                {
                    _custManager.GetAll().Find(x => x.Id == c.CustomerId).Executors.Add(c);

                    _exeManager.Create(c);
                }
                status = "Success";
            }
            catch (Exception e)
            {
                status = e.Message;
            }
            return RedirectToAction("Index", new{statusMesage = status });
        }
        [HttpPost]
        public ActionResult JsonToDBServInd(HttpPostedFileBase file)
        {
            string status;
            try
            {
                if (file != null && file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    file.SaveAs(Path.Combine(directory, fileName));
                }
                JsonManager<ServiceIndustry> jman = new JsonManager<ServiceIndustry>();
                List<ServiceIndustry> customers = jman.ReadFile(@"D:\PROGRAMMING\AcademySS\ServiceIndustryWebSolo\ServiceIndustryWebSolo\ServiceIndustryWebSolo\ConvertFiles\JsonFilesForDB\", file.FileName);

                foreach (ServiceIndustry c in customers)
                {
                    _servIndManager.Create(c);
                }
                status = "Success";
            }
            catch (Exception e)
            {
                status = e.Message;
            }
            return RedirectToAction("Index", new { statusMesage = status });
        }
        [HttpPost]
        public ActionResult JsonToDServIndType(HttpPostedFileBase file)
        {
            string status;
            try
            {
                if (file != null && file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    file.SaveAs(Path.Combine(directory, fileName));
                }
                JsonManager<ServiceIndustryType> jman = new JsonManager<ServiceIndustryType>();
                List<ServiceIndustryType> customers = jman.ReadFile(@"D:\PROGRAMMING\AcademySS\ServiceIndustryWebSolo\ServiceIndustryWebSolo\ServiceIndustryWebSolo\ConvertFiles\JsonFilesForDB\", file.FileName);

                foreach (ServiceIndustryType c in customers)
                {
                    _servIndTypeManager.Create(c);
                }
                status = "Success";
            }
            catch (Exception e)
            {
                status = e.Message;
            }
            return RedirectToAction("Index", new { statusMesage = status });
        }
    }
}