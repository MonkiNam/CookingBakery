using System;
using System.Linq;
using System.Threading.Tasks;
using EventManagementFPT.Model;
using EventManagementFPT.Modules.CategoryModule.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EventManagementFPT.Pages.CategoryPage
{
    [Authorize(Roles="Admin")]
    public class EditModel : PageModel
    {
        private readonly EventManagementContext _context;
        private readonly ICategoryService _categoryService;

        public EditModel(EventManagementContext context, ICategoryService categoryService)
        {
            _context = context;
            _categoryService = categoryService;
        }

        [BindProperty]
        public Category Category { get; set; }

        public IActionResult OnGetAsync(Guid? id)
        {
            if (id == null) return NotFound();

            Category = _categoryService.GetCategoryByID(id);

            if (Category == null) return NotFound();
            
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _categoryService.UpdateCategory(_context.Attach(Category).Entity);

            return RedirectToPage("./Index");
        }

        private bool CategoryExists(Guid id)
        {
            return _context.Categories.Any(e => e.CategoryId == id);
        }
    }
}
