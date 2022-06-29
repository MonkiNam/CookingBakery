using EventManagementFPT.Model;
using EventManagementFPT.Modules.CategoryModule.Interface;
using EventManagementFPT.Utils.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventManagementFPT.Modules.CategoryModule
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly EventManagementContext _db;

        public CategoryRepository(EventManagementContext db) : base(db)
        {
            _db = db;
        }
    }
}
