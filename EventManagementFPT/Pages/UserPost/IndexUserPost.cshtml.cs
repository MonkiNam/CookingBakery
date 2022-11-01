using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CookingBakery.Models;
using CookingBakery.BakeryModules.PostModule.Interface;
using System.Security.Claims;

namespace CookingBakery.Pages.UserPost
{
    public class IndexModel : PageModel
    {
        private readonly IPostService _postService;

        public IndexModel(IPostService postService)
        {
            _postService = postService;
        }

        public IEnumerable<Post> Post { get;set; }

        public async Task OnGetAsync()
        {
            Post = (ICollection<Post>)_postService.GetAll();
            Post = Post.Where(x => x.AuthorId == Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value));
            string test;
        }
    }
}
