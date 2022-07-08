using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using EventManagementFPT.Model;
using EventManagementFPT.Modules.EventModule.Interface;

namespace EventManagementFPT.Pages.EventPage
{
    public class CreateModel : PageModel
    {
        private readonly EventManagementFPT.Model.EventManagementContext _context;
        private readonly IEventService _eventService;

        public CreateModel(EventManagementFPT.Model.EventManagementContext context, IEventService eventService)
        {
            _context = context;
            _eventService = eventService;
        }

        public IActionResult OnGet()
        {
            ViewData["Category"] = new SelectList(_context.Categories, "CategoryId", "Name");
            ViewData["Venue"] = new SelectList(_context.Venues, "VenueId", "VenueName");
            TempData["success"] = "Page loaded!";

            return Page();
        }

        [BindProperty]
        public Event Event { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (DateTime.Compare(Event.StartDate, Event.EndDate) >= 0)
            {
                return Page();
            }
            await _eventService.AddNewEvent(Event);

            return RedirectToPage("./Index");
        }
    }
}
