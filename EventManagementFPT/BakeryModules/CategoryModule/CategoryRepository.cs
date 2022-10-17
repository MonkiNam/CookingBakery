using CookingBakery.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using CookingBakery.BakeryModules.CategoryModule.Interface;
using CookingBakery.Utils.BakeryRepository;

namespace CookingBakery.BakeryModules.CategoryModule
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly CookingBakeryContext _db;

        public CategoryRepository(CookingBakeryContext db) : base(db)
        {
            _db = db;
        }
        public ICollection<Category> GetCategoriesBy(
            Expression<Func<Category, bool>> filter = null,
            Func<IQueryable<Category>, ICollection<Category>> options = null,
            string includeProperties = null
        )
        {
            IQueryable<Category> query = DbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }

            return options != null ? options(query).ToList() : query.ToList();
        }
    }
}
