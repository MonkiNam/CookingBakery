using BussinessObject.Models;
using Repositories.Utils.BakeryRepository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Repositories.BakeryModules.CategoryModule.Interface
{
    public interface ICategoryRepository : IRepository<Category>
    {
        public ICollection<Category> GetCategoriesBy(
            Expression<Func<Category, bool>> filter = null,
            Func<IQueryable<Category>, ICollection<Category>> options = null,
            string includeProperties = null
        );
    }
}
