using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BussinessObject.Models;
using CookingBakery.Modules.CategoryModule.Interface;
using Microsoft.AspNetCore.Authorization;

namespace CookingBakery.Pages.CategoryPage
{
    [Authorize(Roles="Admin")]
    public class DetailsModel : PageModel
    {
        private readonly ICategoryService _categoryService;

        public DetailsModel(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public Category Category { get; set; }

        public IActionResult OnGetAsync(Guid? id)
        {
            if (id == null) return NotFound();

            Category = _categoryService.GetCategoryByID(id);

            if (Category == null) return NotFound();
            
            return Page();
        }
    }
}
