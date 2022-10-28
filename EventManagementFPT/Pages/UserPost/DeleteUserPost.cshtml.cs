using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CookingBakery.Models;

namespace CookingBakery.Pages.UserPost
{
    public class DeleteModel : PageModel
    {
        private readonly CookingBakery.Models.CookingBakeryContext _context;

        public DeleteModel(CookingBakery.Models.CookingBakeryContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Post Post { get; set; }
        public IEnumerable<PostDetail> PostDetail { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Post = await _context.Posts
                .Include(p => p.Category).FirstOrDefaultAsync(m => m.PostId == id);

            if (Post == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Post = await _context.Posts.FindAsync(id);
            PostDetail =  await _context.PostDetails.Where(x => x.PostId.Equals(id)).ToListAsync();

            if (Post != null )
            {
                _context.Posts.Remove(Post);
                foreach(var item in PostDetail)
                {
                    _context.PostDetails.Remove(item);
                }
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
