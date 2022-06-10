using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using EventManagementFPT.Model;
using EventManagementFPT.Utils.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace EventManagementFPT.Utils.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {

        private readonly EventManagementContext _db;
        internal DbSet<T> DbSet;

        public Repository(EventManagementContext db)
        {
            _db = db;
            DbSet = _db.Set<T>();
        }

        public void AddAsync(T entity)
        {
            DbSet.AddAsync(entity);
            _db.SaveChanges();
        }

        public void AddRangeAsync(ICollection<T> entities)
        {
            DbSet.AddRangeAsync(entities);
            _db.SaveChanges();
        }

        public T GetByID(string key)
        {
            return DbSet.Find(key);
        }

        public ICollection<T> GetAll(Func<IQueryable<T>, ICollection<T>> options = null, string includeProperties = null)
        {
            IQueryable<T> query = DbSet;
            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }

            if (options != null)
            {
                return options(query).ToList();
            }

            return query.ToList();
        }

        public T GetFirstOrDefault(Expression<Func<T, bool>> filter = null, string includeProperties = null)
        {
            IQueryable<T> query = DbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }

            return query.FirstOrDefault();
        }

        public void Remove(string key)
        {
            var entity = DbSet.Find(key);
            DbSet.Remove(entity);
            _db.SaveChanges();
        }

        public void Remove(T entity)
        {
            DbSet.Remove(entity);
            _db.SaveChanges();
        }

        public void RemoveRange(ICollection<T> entities)
        {
            DbSet.RemoveRange(entities);
            _db.SaveChanges();
        }

        public void Update(T entity)
        {
            DbSet.Update(entity);
            _db.SaveChanges();
        }

        public void UpdateRange(ICollection<T> entities)
        {
            DbSet.UpdateRange(entities);
            _db.SaveChanges();
        }
    }
}
