using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CookingBakery.Models;
using CookingBakery.BakeryModules.PostModule.Interface;
using Microsoft.AspNetCore.Authorization;

namespace CookingBakery.Pages.EventPage
{
    [Authorize(Roles = "Admin")]

    public class DeleteModel : PageModel
    {
        private readonly IPostService _postService;

        public DeleteModel(IPostService postService)
        {
            _postService = postService;
        }

        [BindProperty] public Post Post { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null) return NotFound();

            Post = await _postService.GetPostByID(id);

            if (Post == null) return NotFound();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null) return NotFound();

            await _postService.DeletePost(id);

            return RedirectToPage("./Index");
        }
    }
}