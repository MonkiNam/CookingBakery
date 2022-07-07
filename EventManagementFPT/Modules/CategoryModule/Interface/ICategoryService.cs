using EventManagementFPT.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventManagementFPT.Modules.CategoryModule.Interface
{
    public interface ICategoryService 
    {
        public Task AddNewCategory(Category newCategory);
        public Task UpdateCategory(Category categoryUpdate);
        public Task DeleteCategory(Guid? ID);
        public ICollection<Category> GetAll();
    }
}
