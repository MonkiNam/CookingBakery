using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BussinessObject.Models;
using Repositories.BakeryModules.ProductModule.Interface;

namespace CookingBakery.Pages.ProductPage
{
    public class CreateModel : PageModel
    {
        private readonly CookingBakeryContext _context;
        private readonly IProductService _productService;

        public CreateModel(BussinessObject.Models.CookingBakeryContext context, IProductService productService)
        {
            _context = context;
            _productService = productService;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Product Product { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            await _productService.AddNewProduct(Product);

            return RedirectToPage("./Index");
        }
    }
}
