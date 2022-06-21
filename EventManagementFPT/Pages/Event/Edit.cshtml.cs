using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EventManagementFPT.Model;

namespace EventManagementFPT.Pages.Event
{
    public class EditModel : PageModel
    {
        private readonly EventManagementFPT.Model.EventManagementContext _context;

        public EditModel(EventManagementFPT.Model.EventManagementContext context)
        {
            _context = context;
        }

        [BindProperty]
        public TblEvent TblEvent { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TblEvent = await _context.TblEvents
                .Include(t => t.CategoryNavigation).FirstOrDefaultAsync(m => m.EventId == id);

            if (TblEvent == null)
            {
                return NotFound();
            }
           ViewData["Category"] = new SelectList(_context.TblCategories, "CategoryId", "Name");
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

            _context.Attach(TblEvent).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TblEventExists(TblEvent.EventId))
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

        private bool TblEventExists(Guid id)
        {
            return _context.TblEvents.Any(e => e.EventId == id);
        }
    }
}
