using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;

namespace DAL.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly ServiceContext db;
        private DbSet<T> dbSet;

        public BaseRepository(ServiceContext context)
        {
            db = context;
            dbSet = db.Set<T>();
        }

        public virtual List<T> GetAll()
        {
            return dbSet.ToList();
        }

        public T GetById(string id)
        {
            return dbSet.Find(id);
        }

        public virtual void Insert(T entity)
        {
            dbSet.AddOrUpdate(entity);
        }

        public virtual void Update(T entity)
        {
            dbSet.AddOrUpdate(entity);
        }

        public virtual void Delete(T entity)
        {
            if (db.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }
            dbSet.Remove(entity);
        }
    }
}
