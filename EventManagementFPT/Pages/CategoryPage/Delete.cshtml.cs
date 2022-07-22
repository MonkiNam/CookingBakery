using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EventManagementFPT.Model;
using EventManagementFPT.Modules.CategoryModule.Interface;
using Microsoft.AspNetCore.Authorization;

namespace EventManagementFPT.Pages.CategoryPage
{
    [Authorize(Roles="Admin")]
    public class DeleteModel : PageModel
    {
        private readonly ICategoryService _categoryService;

        public DeleteModel(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [BindProperty]
        public Category Category { get; set; }

        public IActionResult OnGet(Guid? id)
        {
            if (id == null) return NotFound();

            Category = _categoryService.GetCategoryByID(id);

            if (Category == null) return NotFound();
            
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null) return NotFound();

            await _categoryService.DeleteCategory(id);

            return RedirectToPage("./Index");
        }
    }
}
