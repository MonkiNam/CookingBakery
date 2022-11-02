using System;
using System.Threading.Tasks;
using BussinessObject.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Repositories.BakeryModules.PostModule.Interface;

namespace CookingBakery.Pages.EventPage
{
    public class EditModel : PageModel
    {
        private readonly CookingBakeryContext _context;
        private readonly IPostService _postService;
        private readonly IWebHostEnvironment _env;
        private readonly ICategoryService _categoryService;

        public EditModel(CookingBakeryContext context, IPostService postService, IWebHostEnvironment env, ICategoryService categoryService)
        {
            _context = context;
            _postService = postService;
            _env = env;
            _categoryService = categoryService;
        }

        [BindProperty]
        public Post Post { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null) return NotFound();

            Post = await _postService.GetPostByID(id);

            if (Post == null) return NotFound();

            ViewData["Category"] = new SelectList(_categoryService.GetCategoriesBy(x => x.Status != false), "CategoryId", "Name");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(IFormFile customFile)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Category"] = new SelectList(_categoryService.GetCategoriesBy(x => x.Status != false), "CategoryId", "Name");
                return Page();
            }

            if (customFile != null)
            {
                string NewImageUrl = await UploadImage.UploadFile(customFile, _env);
                Post.ImageUrl = NewImageUrl;
            }

            await _postService.UpdatePost(_context.Attach(Post).Entity);

            return RedirectToPage("./Index");
        }
    }
}
