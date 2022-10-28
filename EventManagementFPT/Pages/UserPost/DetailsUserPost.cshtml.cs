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
    public class DetailsModel : PageModel
    {
        private readonly CookingBakery.Models.CookingBakeryContext _context;

        public DetailsModel(CookingBakery.Models.CookingBakeryContext context)
        {
            _context = context;
        }

        public Post Post { get; set; }
        public IEnumerable<PostDetail> PostDetails { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Post = await _context.Posts
             .Include(x=>x.Category).FirstOrDefaultAsync(m => m.PostId == id);

            PostDetails = await _context.PostDetails.Include(x=>x.Product).Where(x => x.PostId.Equals(id)).ToListAsync();

            if (Post == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
