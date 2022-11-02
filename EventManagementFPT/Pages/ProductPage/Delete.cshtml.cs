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

    public class DeleteModel : PageModel
    {

        private readonly CookingBakery.Models.CookingBakeryContext _context;

        public DeleteModel(CookingBakery.Models.CookingBakeryContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Product = await _context.Products.FindAsync(id);
            var tmp = _context.PostDetails.FirstOrDefault(x => x.ProductId.Equals(id));

            if (Product != null && tmp == null)
            {
                _context.Products.Remove(Product);
                await _context.SaveChangesAsync();
            }
            else
            {
                TempData["error"] = "Product is in a post!";
            }

            return RedirectToPage("./Index");
        }
    }
}
