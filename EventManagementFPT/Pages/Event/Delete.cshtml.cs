using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EventManagementFPT.Model;

namespace EventManagementFPT.Pages.Event
{
    public class DeleteModel : PageModel
    {
        private readonly EventManagementFPT.Model.EventManagementContext _context;

        public DeleteModel(EventManagementFPT.Model.EventManagementContext context)
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
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TblEvent = await _context.TblEvents.FindAsync(id);

            if (TblEvent != null)
            {
                _context.TblEvents.Remove(TblEvent);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
