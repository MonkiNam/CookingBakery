using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EventManagementFPT.Model;
using EventManagementFPT.Modules.CategoryModule.Interface;
using Microsoft.AspNetCore.Authorization;

namespace EventManagementFPT.Pages.CategoryPage
{
    [Authorize(Roles="Admin")]
    public class IndexModel : PageModel
    {
        private readonly ICategoryService _categoryService;

        public IndexModel(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public IList<Category> Category { get;set; }

        public void OnGet()
        {
            Category = _categoryService.GetAll().ToList();
        }
    }
}
