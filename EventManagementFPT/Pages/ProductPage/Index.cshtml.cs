using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CookingBakery.Models;

namespace CookingBakery.Pages.ProductPage
{
    public class IndexModel : PageModel
    {
        private readonly CookingBakery.Models.CookingBakeryContext _context;

        public IndexModel(CookingBakery.Models.CookingBakeryContext context)
        {
            _context = context;
        }

        public IList<Product> Product { get;set; }

        public async Task OnGetAsync()
        {
            Product = await _context.Products.ToListAsync();
        }
    }
}
