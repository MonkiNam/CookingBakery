using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CookingBakery.Models;
using Microsoft.AspNetCore.Authorization;

namespace CookingBakery.Pages.UserPost
{
    [Authorize(Roles = "User")]

    public class EditModel : PageModel
    {
        private readonly CookingBakery.Models.CookingBakeryContext _context;

        public EditModel(CookingBakery.Models.CookingBakeryContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Post Post { get; set; }

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
           ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "Name");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Post).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PostExists(Post.PostId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool PostExists(Guid id)
        {
            return _context.Posts.Any(e => e.PostId == id);
        }
    }
}
