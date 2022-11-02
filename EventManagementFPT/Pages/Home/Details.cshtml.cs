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
using Microsoft.AspNetCore.Http;
using CookingBakery.BakeryModules.UserModule.Interface;
using CookingBakery;


namespace CookingBakery.Pages.Home
{
    public class Details : PageModel
    {
        private readonly CookingBakery.Models.CookingBakeryContext _context;
        private readonly IPostService _postService;
        private readonly ICommentService _commentService;
        private readonly IUserService _userService;


        public Details(CookingBakery.Models.CookingBakeryContext context, IPostService postService, ICommentService commentService, IUserService userService)
        {
            _context = context;
            _postService = postService;
            _commentService = commentService;
            _userService = userService;
        }
        [BindProperty]
        public  Post Post { get; set; } 
        [BindProperty]
        public  Comment ReplyComment { get; set; }
        [BindProperty]
        public IEnumerable<PostDetail> PostDetails { get; set; }
        [BindProperty]
        public IEnumerable<Models.Comment> Comments { get; set; }
        public PostReaction isLike { get; set; }

        [BindProperty]
        public string Role { get; set; }


        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Post = await _postService.GetPostByID(id);

            PostDetails = await _context.PostDetails.Include(x => x.Product).Where(x => x.PostId.Equals(Post.PostId)).ToListAsync();
            Comments =  _commentService.GetListCommentByPostId(id);

            var loginUser = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            Role = User.FindFirst(ClaimTypes.Role)?.Value;


            isLike = _context.PostReactions.Where(x => x.PostId.Equals(Post.PostId) && x.UserId.Equals(loginUser)).FirstOrDefault();




            if (Post == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostLike()
        {
             _userService.LikePost(Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value), Post.PostId);
            return RedirectToPage(new { id = Post.PostId.ToString()});


        }
        public async Task<IActionResult> OnPostUnlike()
        {
             _userService.UnlikePost(Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value), Post.PostId);
            return RedirectToPage(new { id = Post.PostId.ToString() });




        }

        public async Task<IActionResult> OnPostReply(Guid cmtId)
        {
            ReplyComment = _context.Comments.FirstOrDefault(x=>x.CommentId.Equals(cmtId));
            Session.SetObjectAsJson(HttpContext.Session, "REPLY", ReplyComment);
             return await OnGetAsync(Post.PostId);



        }

        public async Task<IActionResult> OnPostCancel()
        {
            Session.SetObjectAsJson(HttpContext.Session, "REPLY", null);
            ReplyComment = null;
            return await OnGetAsync(Post.PostId);

        }

        public async Task<IActionResult> OnPostAdd(string message)
        {
            Comment reply = Session.GetObjectFromJson<Comment>(HttpContext.Session, "REPLY");
            Comment cmt = new()
            {
                UserId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value),
                Content = message,
                CreatedDate = DateTime.Now,
                ParentId = reply?.CommentId,
                PostId = Post.PostId,
                Status = true,
                CommentId = Guid.NewGuid()
                
            };
             await _commentService.AddNewComment(cmt);

            return RedirectToPage(new { id = Post.PostId.ToString() });





        }
    }
}