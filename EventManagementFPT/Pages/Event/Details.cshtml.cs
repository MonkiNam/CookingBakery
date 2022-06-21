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
    public class DetailsModel : PageModel
    {
        private readonly EventManagementFPT.Model.EventManagementContext _context;

        public DetailsModel(EventManagementFPT.Model.EventManagementContext context)
        {
            _context = context;
        }

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
    }
}
