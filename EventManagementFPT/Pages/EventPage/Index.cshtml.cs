using System;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BussinessObject.Models;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repositories.Utils;
using Repositories.BakeryModules.PostModule.Interface;

namespace CookingBakery.Pages.EventPage
{
    public class IndexModel : PageModel
    {
        private readonly IPostService _postService;

        public IndexModel(IPostService postService)
        {
            _postService = postService;
        }

        public PaginatedList<Post> Post { get;set; }

        public void OnGet(int? pageIndex)
        {
            var role = User.FindFirst(ClaimTypes.Role)?.Value;
            var uid = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            var posts = _postService.GetAll().AsQueryable();
            Post = PaginatedList<Post>.Create(posts, pageIndex ?? 1, 5);
        }
    }
}
