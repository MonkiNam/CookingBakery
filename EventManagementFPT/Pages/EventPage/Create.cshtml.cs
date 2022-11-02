using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CookingBakery.Models;
using Microsoft.AspNetCore.Http;
using CookingBakery.Utils;
using Microsoft.AspNetCore.Hosting;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using CookingBakery.BakeryModules.PostModule.Interface;
using CookingBakery.BakeryModules.CategoryModule.Interface;
using CookingBakery.BakeryModules.ProductModule.Interface;
using System.Collections.Generic;

namespace CookingBakery.Pages.EventPage
{
    public class CreateModel : PageModel
    {
        private readonly IPostService _postService;
        private readonly IWebHostEnvironment _env;
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;

        public CreateModel(IPostService postService,
            IWebHostEnvironment env, ICategoryService categoryService, IProductService productService)
        {
            _postService = postService;
            _env = env;
            _categoryService = categoryService; ;
            _productService = productService;
        }

        public IActionResult OnGet()
        {
            Detail = Session.GetObjectFromJson<ICollection<PostDetail>>(HttpContext.Session, "DETAIL");
            ViewData["Category"] = new SelectList(_categoryService.GetCategoriesBy(x => x.Status != false), "CategoryId", "Name");
            if (Detail != null)
            {
                var loop = _productService.GetAll();
                var pros = _productService.GetAll();
                foreach(var item in loop)
                {
                    foreach(var item2 in Detail)
                    {
                        if (item.ProductId.Equals(item2.ProductId))
                        {
                            pros.Remove(item);
                        }

                    }
                }
                ViewData["Ingredient"] = new SelectList(pros, "ProductId", "Name");

            }
            else
            {
                ViewData["Ingredient"] = new SelectList(_productService.GetAll(), "ProductId", "Name");

            }

            TempData["success"] = "Page loaded!";

            return Page();
        }

        [BindProperty] public Post Post { get; set; }
        [BindProperty] public ICollection<PostDetail> Detail { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD

        public async Task<IActionResult> OnPostAdd()
        {
            var productId = Request.Form["productId"];
            var quantity = Request.Form["quantity"];

            Product pro =  _productService.GetProductById(Guid.Parse(productId));

            PostDetail detail = new()
            {
                PostId = Post.PostId,
                ProductId = Guid.Parse(productId),
                Quantity = int.Parse(quantity),
                Product = pro
                
            };
            var check = Session.GetObjectFromJson<ICollection<PostDetail>>(HttpContext.Session, "DETAIL");
            if(check != null)
            {
                Detail = check;
            }

            Detail.Add(detail);
            Session.SetObjectAsJson(HttpContext.Session, "DETAIL", Detail);


            return OnGet();
        }

        public async Task<IActionResult> OnPostCancel(Guid id)
        {
            var details = Session.GetObjectFromJson<ICollection<PostDetail>>(HttpContext.Session, "DETAIL");
            var loop = Session.GetObjectFromJson<ICollection<PostDetail>>(HttpContext.Session, "DETAIL");
            foreach(var detail in loop)
            {
                if (detail.ProductId.Equals(id))
                {
                    details.Remove(detail);
                }
            }
            if(details.Count>0 || details != null)
            {
                Session.SetObjectAsJson(HttpContext.Session, "DETAIL", details);
            }
            else
            {
                Session.SetObjectAsJson(HttpContext.Session, "DETAIL", null);
            }

            return OnGet();
        }
        public async Task<IActionResult> OnPostCreate(IFormFile customFile, Guid categoryId)
        {
            var uid = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (uid == null) return RedirectToPage("/Authentication/Index");

            var role = User.FindFirst(ClaimTypes.Role)?.Value;
            if (!ModelState.IsValid)
            {
                TempData["error"] = "Invalid data";
                ViewData["Category"] = new SelectList(_categoryService.GetCategoriesBy(x => x.Status != false), "CategoryId", "Name");
                ViewData["Ingredient"] = new SelectList(_productService.GetAll(), "ProductId", "Name");
                return Page();
            }

            TempData["success"] = "Add success";
            if (customFile != null)
            {
                string imageUrl = await UploadImage.UploadFile(customFile, _env);
                Post.ImageUrl = imageUrl;
            }
            Post.CategoryId = categoryId;
            Detail = Session.GetObjectFromJson<ICollection<PostDetail>>(HttpContext.Session, "DETAIL");

             _postService.AddNewPost(Post, uid, Detail);
            Session.SetObjectAsJson(HttpContext.Session, "DETAIL", null);

            return RedirectToPage("/Home/Index");
        }
    }
}