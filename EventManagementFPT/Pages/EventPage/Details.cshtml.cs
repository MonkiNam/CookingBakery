using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CookingBakery.Models;
using Microsoft.AspNetCore.Authorization;
using CookingBakery.BakeryModules.PostModule.Interface;

namespace CookingBakery.Pages.EventPage
{
    public class DetailsModel : PageModel
    {
        private readonly IPostService _postService;

        public DetailsModel(IPostService postService)
        {
            _postService = postService;
        }

        public Post Post { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null) return NotFound();

            Post = await _postService.GetPostByID(id);

            if (Post == null) return NotFound();
            
            return Page();
        }
    }
}
