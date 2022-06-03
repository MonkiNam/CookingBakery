using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace EventManagementFPT.Utils.Repository.Interface
{
    public interface IRepository<T> where T : class
    {
        T Get(string key);

        void AddAsync(T entity);
        void AddRangeAsync(ICollection<T> entities);

        ICollection<T> GetAll(
            Func<IQueryable<T>, ICollection<T>> options = null,
            string includeProperties = null
            );

        T GetFirstOrDefault(
            Expression<Func<T, bool>> filter = null,
            string includeProperties = null
            );

        void Update(T entity);
        void UpdateRange(ICollection<T> entities);

        void Remove(string key);

        void Remove(T entity);

        void RemoveRange(ICollection<T> entities);

    }
}
