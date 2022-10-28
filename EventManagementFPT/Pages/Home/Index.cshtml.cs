 
using CookingBakery.BakeryModules.PostModule.Interface;
using CookingBakery.Models;
using CookingBakery.Utils;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CookingBakery.Pages.Home
{
    public class Index : PageModel
    {
        private readonly IPostService _postService;
        private readonly CookingBakeryContext _context;

        public Index(IPostService postService, CookingBakeryContext context)
        {
            _postService = postService;
            _context = context;
        }

        public PaginatedList<Post> Post { get; set; }
        public IEnumerable<Post> NewestPost { get; set; }
        public string SearchName { get; set; }
        public Guid? CategoryId { get; set; }

        public void OnGetAsync(
            string currentSearchName, string txtSearchName,
            Guid? currentFilterCategory, Guid? filterCategory,
            int? pageIndex
        )
        {
            NewestPost = _postService.GetNewestPosts(3);

            if (txtSearchName != null || filterCategory != null)
            {
                pageIndex = 1;
            }
            else
            {
                txtSearchName = currentSearchName;
                filterCategory = currentFilterCategory;
            }
            var test = _postService.GetAll().ToList();

            var posts = _postService.GetAll().Where(o => (bool)o.Status).AsQueryable();

            if (!string.IsNullOrEmpty(txtSearchName))
            {
                SearchName = txtSearchName;
                posts = posts.Where(o => o.Title.ToLower().Contains(txtSearchName.ToLower()));
            }

            if (filterCategory != null)
            {
                CategoryId = (Guid)filterCategory;
                posts = posts.Where(o => o.CategoryId == filterCategory);
            }

            Post = PaginatedList<Post>.Create(posts, pageIndex ?? 1, 5);

            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "Name");
        }
    }
}