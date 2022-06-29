using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EventManagementFPT.Model;
using EventManagementFPT.Modules.EventModule.Interface;

namespace EventManagementFPT.Pages.EventPage
{
    public class EditModel : PageModel
    {
        private readonly EventManagementFPT.Model.EventManagementContext _context;
        private readonly IEventService _eventService;

        public EditModel(EventManagementFPT.Model.EventManagementContext context, IEventService eventService)
        {
            _context = context;
            _eventService = eventService;
        }

        [BindProperty]
        public Event Event { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Event = await _context.Events
                .FirstOrDefaultAsync(m => m.EventId == id);

            if (Event == null)
            {
                return NotFound();
            }
           ViewData["Category"] = new SelectList(_context.Categories, "CategoryId", "Name");
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

            await _eventService.UpdateEvent(_context.Attach(Event).Entity);

            return RedirectToPage("./Index");
        }

        private bool EventExists(Guid id)
        {
            return _context.Events.Any(e => e.EventId == id);
        }
    }
}
