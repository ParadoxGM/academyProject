using DAL.Interfaces;
using DAL.Repositories;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        private readonly ServiceContext db;

        private IBaseRepository<Customer> custRepos;
        private IBaseRepository<ServiceIndustry> servRepos;
        private IBaseRepository<Executor> executRepos;
        private IBaseRepository<ServiceIndustryType> servTypeRepos;

        public UnitOfWork(ServiceContext context)
        {
            db = context;
        }

        public IBaseRepository<Customer> CustRepos
        {
            get
            {
                if (custRepos == null) { custRepos = new BaseRepository<Customer>(db); }
                return custRepos;
            }
        }
        public IBaseRepository<ServiceIndustry> ServRepos
        {
            get
            {
                if (servRepos == null) { servRepos = new BaseRepository<ServiceIndustry>(db); }
                return servRepos;
            }
        }
        public IBaseRepository<Executor> ExecutRepos
        {
            get
            {
                if (executRepos == null) { executRepos = new BaseRepository<Executor>(db); }
                return executRepos;
            }
        }
        public IBaseRepository<ServiceIndustryType> ServTypeRepos
        {
            get
            {
                if (servTypeRepos == null) { servTypeRepos = new BaseRepository<ServiceIndustryType>(db); }
                return servTypeRepos;
            }
        }

        public int Save()
        {
            return db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
