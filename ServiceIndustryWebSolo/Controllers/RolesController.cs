using DAL;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Models.Models;
using ServiceIndustryWebSolo.App_Start;
using ServiceIndustryWebSolo.Attributes;
using ServiceIndustryWebSolo.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ServiceIndustryWebSolo.Controllers
{
    [CustomAuthorize(Roles = "admin")]
    [OutputCache(VaryByParam = "*", Duration = 0, NoStore = true)]
    public class RolesController : Controller
    {
        private ApplicationRoleManager RoleManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ApplicationRoleManager>();
            }
        }

        private CustomerManager UserManager
        {
            get { return HttpContext.GetOwinContext().GetUserManager<CustomerManager>(); }
        }
        
        public ActionResult Index()
        {
            return View(RoleManager.Roles);
        }
        
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreateRoleModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityResult result = await RoleManager.CreateAsync(new ApplicationRole
                {
                    Name = model.Name,
                    Description = model.Description
                });
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Что-то пошло не так");
                }
            }
            return View(model);
        }
        
        public async Task<ActionResult> Edit(string id)
        {
            ApplicationRole role = await RoleManager.FindByIdAsync(id);
            if (role != null)
            {
                return View(new EditRoleModel { Id = role.Id, Name = role.Name, Description = role.Description });
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<ActionResult> Edit(EditRoleModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationRole role = await RoleManager.FindByIdAsync(model.Id);
                if (role != null)
                {
                    role.Description = model.Description;
                    role.Name = model.Name;
                    IdentityResult result = await RoleManager.UpdateAsync(role);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Что-то пошло не так");
                    }
                }
            }
            return View(model);
        }
        
        public async Task<ActionResult> Delete(string id)
        {
            ApplicationRole role = await RoleManager.FindByIdAsync(id);
            if (role != null)
            {
                IdentityResult result = await RoleManager.DeleteAsync(role);
            }
            return RedirectToAction("Index");
        }
        
        public ActionResult ShowUserRoles()
        {
            List<UserRoleViewModel> userRole = new List<UserRoleViewModel>();
            ServiceContext context = new ServiceContext();
            foreach(Customer c in context.Users.ToList())
            {
                foreach(ApplicationRole r in context.Roles.ToList())
                {
                    foreach(var cu in c.Roles.ToList())
                    {
                        if (cu.RoleId == r.Id)
                        {
                            userRole.Add(new UserRoleViewModel() { Email = c.Email, RoleName = r.Name });
                        }
                    }
                }
            }
            return View(userRole);
        }
    }
}