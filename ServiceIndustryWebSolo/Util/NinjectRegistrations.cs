using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BAL.Interfaces;
using BAL.Managers;
using DAL;
using DAL.Interfaces;
using DAL.Repositories;
using Ninject.Modules;

namespace ServiceIndustryWebSolo.Util
{
    public class NinjectRegistrations : NinjectModule
    {
        public override void Load()
        {
            Bind<IUnitOfWork>().To<UnitOfWork>().WithConstructorArgument("context", new ServiceContext());
            Bind(typeof(IBaseRepository<>)).To(typeof(BaseRepository<>));
            Bind(typeof(ICustomerManager)).To(typeof(CustomerManager));
            Bind(typeof(IExecutorManager)).To(typeof(ExecutorManager));
            Bind(typeof(IServiceIndustryManager)).To(typeof(ServiceIndustryManager));
            Bind(typeof(IServiceIndustryTypeManager)).To(typeof(ServiceIndustryTypeManager));
            Bind<ServiceContext>().To<ServiceContext>();
        }
    }
}