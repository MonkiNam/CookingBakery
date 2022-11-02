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

namespace CookingBakery.Pages.EventPage
{
    public class CreateModel : PageModel
    {
        private readonly IPostService _postService;
        private readonly IWebHostEnvironment _env;
        private readonly ICategoryService _categoryService;

        public CreateModel(IPostService postService,
            IWebHostEnvironment env, ICategoryService categoryService)
        {
            _postService = postService;
            _env = env;
            _categoryService = categoryService;
        }

        public IActionResult OnGet()
        {
                ViewData["Category"] = new SelectList(_categoryService.GetCategoriesBy(x => x.Status != false), "CategoryId", "Name");
                TempData["success"] = "Page loaded!";

                return Page();
        }

        [BindProperty] public Post Post { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(IFormFile customFile)
        {
            var uid = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (uid == null) return RedirectToPage("/Authentication/Index");

            var role = User.FindFirst(ClaimTypes.Role)?.Value;
                if (!ModelState.IsValid)
                {
                    TempData["error"] = "Invalid data";
                    ViewData["Category"] = new SelectList(_categoryService.GetCategoriesBy(x => x.Status != false), "CategoryId", "Name");
                    return Page();
                }

                TempData["success"] = "Add success";
                if (customFile != null)
                {
                    string imageUrl = await UploadImage.UploadFile(customFile, _env);
                    Post.ImageUrl = imageUrl;
                }
                await _postService.AddNewPost(Post, uid);
                return RedirectToPage("Home/Index");
        }
    }
}