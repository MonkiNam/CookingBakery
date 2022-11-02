using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CookingBakery.Models;
using Microsoft.AspNetCore.Authorization;

namespace CookingBakery.Pages.ProductPage
{
    [Authorize(Roles = "Admin")]

    public class DetailsModel : PageModel
    {
        private readonly CookingBakery.Models.CookingBakeryContext _context;

        public DetailsModel(CookingBakery.Models.CookingBakeryContext context)
        {
            _context = context;
        }

        public Product Product { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Product = await _context.Products.FirstOrDefaultAsync(m => m.ProductId == id);

            if (Product == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
