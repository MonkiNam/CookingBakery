using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CookingBakery.BakeryModules.CommentModule.Interface;
using CookingBakery.BakeryModules.PostModule.Interface;
using CookingBakery.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CookingBakery.Pages.Home
{
    public class Details : PageModel
    {
        private readonly CookingBakery.Models.CookingBakeryContext _context;
        private readonly IPostService _postService;
        private readonly ICommentService _commentService;


        public Details(CookingBakery.Models.CookingBakeryContext context, IPostService postService, ICommentService commentService)
        {
            _context = context;
            _postService = postService;
            _commentService = commentService;
        }

        public Post Post { get; set; }
        public IEnumerable<PostDetail> PostDetails { get; set; }

        public IEnumerable<Models.Comment> Comments { get; set; }

        public IQueryable<PostReaction> isLike { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Post = await _context.Posts
             .Include(x => x.Category).FirstOrDefaultAsync(m => m.PostId == id);

            PostDetails = await _context.PostDetails.Include(x => x.Product).Where(x => x.PostId.Equals(id)).ToListAsync();
            Comments = await _context.Comments.Where(x=> x.PostId.Equals(id)).ToListAsync(); 

            var loginUser = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            isLike = _context.PostReactions.Where(x => x.PostId.Equals(id) && x.UserId.Equals(loginUser));




            if (Post == null)
            {
                return NotFound();
            }
            return Page();
        }

        //public async Task<IActionResult> OnPostAsync(Guid? userId, Guid? postId)
        //{

        //}
    }
}