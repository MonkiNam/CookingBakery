using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using EventManagementFPT.Model;
using EventManagementFPT.Modules.CategoryModule.Interface;

namespace EventManagementFPT.Pages.CategoryPage
{
    public class CreateModel : PageModel
    {
        private readonly EventManagementFPT.Model.EventManagementContext _context;
        private readonly ICategoryService _categoryService;

        public CreateModel(EventManagementFPT.Model.EventManagementContext context, ICategoryService categoryService)
        {
            _context = context;
            _categoryService = categoryService;
        }

        public IActionResult OnGet()
        {
            TempData["success"] = "Page loaded!";
            return Page();
        }

        [BindProperty]
        public Category Category { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _categoryService.AddNewCategory(Category);

            return RedirectToPage("./Index");
        }
    }
}
