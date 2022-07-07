using EventManagementFPT.Model;
using EventManagementFPT.Modules.CategoryModule.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventManagementFPT.Modules.CategoryModule
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public ICollection<Category> GetAll()
        {
            return _categoryRepository.GetAll().ToList();
        }
        public async Task AddNewCategory(Category newCategory)
        {
            newCategory.CategoryId = Guid.NewGuid();
            await _categoryRepository.AddAsync(newCategory);
        }
        public async Task UpdateCategory(Category categoryUpdate)
        {
            await _categoryRepository.UpdateAsync(categoryUpdate);
        }
        public async Task DeleteCategory(Guid? id)
        {
            Category categoryDelete = _categoryRepository.GetFirstOrDefaultAsync(x => x.CategoryId.Equals(id)).Result;
            if (categoryDelete != null) await _categoryRepository.RemoveAsync(categoryDelete);
        }
    }
}
