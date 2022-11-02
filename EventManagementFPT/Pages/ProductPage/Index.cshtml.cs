using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BussinessObject.Models;
using Repositories.BakeryModules.ProductModule.Interface;

namespace CookingBakery.Pages.ProductPage
{
    public class IndexModel : PageModel
    {
        private readonly BussinessObject.Models.CookingBakeryContext _context;
        private readonly IProductService _productService;

        public IndexModel(BussinessObject.Models.CookingBakeryContext context, IProductService productService)
        {
            _context = context;
            _productService = productService;
        }

        public IEnumerable<Product> Product { get;set; }

        public void OnGet()
        {
            Product = _productService.GetAll();
        }
    }
}
