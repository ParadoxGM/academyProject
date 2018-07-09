using DAL;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace ServiceIndustryWebSolo.App_Start
{

    public class CustomerManager : UserManager<Customer>
    {
        public CustomerManager(IUserStore<Customer> store)
                : base(store)
        {
        }
        public static CustomerManager Create(IdentityFactoryOptions<CustomerManager> options, IOwinContext context)
        {
            ServiceContext db = context.Get<ServiceContext>();
            CustomerManager manager = new CustomerManager(new UserStore<Customer>(db));
            return manager;
        }
    }

    class ApplicationRoleManager : RoleManager<ApplicationRole>
    {
        public ApplicationRoleManager(RoleStore<ApplicationRole> store)
                    : base(store)
        { }
        public static ApplicationRoleManager Create(IdentityFactoryOptions<ApplicationRoleManager> options, IOwinContext context)
        {
            return new ApplicationRoleManager(new RoleStore<ApplicationRole>(context.Get<ServiceContext>()));
        }
    }


    // Настройка диспетчера входа для приложения.
    public class ApplicationSignInManager : SignInManager<Customer, string>
    {
        public ApplicationSignInManager(CustomerManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(Customer user)
        {
            return user.GenerateUserIdentityAsync((CustomerManager)UserManager);
        }

        public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
        {
            return new ApplicationSignInManager(context.GetUserManager<CustomerManager>(), context.Authentication);
        }
    }


    public class IdentityConfig
    {
    }
}