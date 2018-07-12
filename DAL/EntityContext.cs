using Models.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DAL
{
    public class ServiceContext : IdentityDbContext<Customer>
    {
        public ServiceContext()
            : base("ManoliiGordDB1") { }

        public static ServiceContext Create()
        {
            return new ServiceContext();
        }


        public DbSet<ServiceIndustry> Services { get; set; }
        public DbSet<Executor> Executors { get; set; }
        public DbSet<ServiceIndustryType> ServiceTypes { get; set; }
    }
}
