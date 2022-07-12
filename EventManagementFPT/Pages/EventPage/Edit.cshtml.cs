using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using EventManagementFPT.Model;
using EventManagementFPT.Modules.EventModule.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;

namespace EventManagementFPT.Pages.EventPage
{
    public class EditModel : PageModel
    {
        private readonly EventManagementFPT.Model.EventManagementContext _context;
        private readonly IEventService _eventService;
        private readonly IWebHostEnvironment _env;

        public EditModel(EventManagementFPT.Model.EventManagementContext context, IEventService eventService, IWebHostEnvironment env)
        {
            _context = context;
            _eventService = eventService;
            _env = env;
        }

        [BindProperty]
        public Event Event { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Event = await _eventService.GetEventByID(id);

            if (Event == null)
            {
                return NotFound();
            }
            ViewData["Venue"] = new SelectList(_context.Venues, "VenueId", "VenueName");
            ViewData["Category"] = new SelectList(_context.Categories, "CategoryId", "Name");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(IFormFile customFile)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Venue"] = new SelectList(_context.Venues, "VenueId", "VenueName");
                ViewData["Category"] = new SelectList(_context.Categories, "CategoryId", "Name");
                return Page();
            }
            if(customFile != null)
            {
                string NewImageUrl = await Utils.UploadImage.UploadFile(customFile, _env);
                Event.ImageUrl = NewImageUrl;
            }

            await _eventService.UpdateEvent(_context.Attach(Event).Entity);

            return RedirectToPage("./Index");
        }
    }
}
