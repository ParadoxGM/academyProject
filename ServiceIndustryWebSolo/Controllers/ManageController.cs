using Microsoft.AspNet.Identity;
using ServiceIndustryWebSolo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using ServiceIndustryWebSolo.App_Start;
using Models.Models;
using ServiceIndustryWebSolo.Attributes;
using System.IO;
using BAL.Interfaces;

namespace ServiceIndustryWebSolo.Controllers
{
    [CustomAuthorize(Roles = "admin,user")]
    [OutputCache(VaryByParam = "*", Duration = 0, NoStore = true)]
    public class ManageController : Controller
    {
        
        private ApplicationSignInManager _signInManager;
        private CustomerManager _userManager;
        public ManageController()
        {
        }

        public ManageController(CustomerManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

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
        
        public async Task<ActionResult> Index()
        {
            Customer c = await UserManager.FindByEmailAsync(User.Identity.Name);
            return View(IndexViewModel.CustomerToIVM(c));
        }
        public ActionResult ChangePassword()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                return RedirectToAction("Index");
            }
            AddErrors(result);
            return View(model);
        }

        [HttpGet]
        public ActionResult Delete(IndexViewModel customer)
        {
            return View(customer);
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirmed()
        {
            Customer user = await UserManager.FindByEmailAsync(User.Identity.Name);
            if (user != null)
            {
                IdentityResult result = await UserManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("LogOff", "Account");
                }
            }
            return RedirectToAction("Index", "Home");
        }


        public async Task<ActionResult> Edit(string ph)
        {
            Customer c = await UserManager.FindByEmailAsync(User.Identity.Name);
            if (c != null)
            {
                IndexViewModel model = new IndexViewModel
                {
                    Name = c.Name,
                    Surname = c.Surname,
                    UserName = c.UserName,
                    Phone = c.PhoneNumber,
                    Email = c.Email,
                    EmailConfirmed = c.EmailConfirmed,
                    Photo = ph ?? c.Photo,
                    Executors = c.Executors.ToList()
                };
                return View(model);
            }
            return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        public async Task<ActionResult> Edit(IndexViewModel model, string ph)
        {
            Customer user = await UserManager.FindByEmailAsync(User.Identity.Name);
            if (user != null)
            {
                user.Name = model.Name;
                user.Surname = model.Surname;
                user.UserName = model.UserName;
                user.PhoneNumber = model.Phone;
                user.Email = model.Email;
                user.EmailConfirmed = model.EmailConfirmed;
                user.Photo = ph ?? model.Photo;
                user.Executors = model.Executors;   
                   
                IdentityResult result = await UserManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Something went wrong...");
                }
            }
            else
            {
                ModelState.AddModelError("", "Пользователь не найден");
            }

            return View(model);
        }

        public ActionResult Upload(HttpPostedFileBase photo)
        {
            return PartialView("UpLoadPhoto");
        }

        [HttpPost]
        [ActionName("Upload")]
        public ActionResult UploadConfirm(HttpPostedFileBase photo)
        {
            string directory = @"D:\rep\ServiceIndustryWebSolo\Images\UserImgs";

            if (photo != null && photo.ContentLength > 0)
            {
                var fileName = Path.GetFileName(photo.FileName);
                photo.SaveAs(Path.Combine(directory, fileName));
                UserManager.FindById(User.Identity.GetUserId()).Photo= fileName;
                return RedirectToAction("Edit", new { ph = photo.FileName });
            }
            return RedirectToAction("Edit");
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
    }
   
}