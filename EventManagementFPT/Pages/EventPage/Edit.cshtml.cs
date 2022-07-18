using System;
using System.Threading.Tasks;
using EventManagementFPT.Model;
using EventManagementFPT.Modules.CategoryModule.Interface;
using EventManagementFPT.Modules.EventModule.Interface;
using EventManagementFPT.Modules.VenueModule.Interface;
using EventManagementFPT.Utils;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EventManagementFPT.Pages.EventPage
{
    public class EditModel : PageModel
    {
        private readonly EventManagementContext _context;
        private readonly IEventService _eventService;
        private readonly IWebHostEnvironment _env;
        private readonly ICategoryService _categoryService;
        private readonly IVenueService _venueService;

        public EditModel(EventManagementContext context, IEventService eventService, IWebHostEnvironment env, ICategoryService categoryService, IVenueService venueService)
        {
            _context = context;
            _eventService = eventService;
            _env = env;
            _categoryService = categoryService;
            _venueService = venueService;
        }

        [BindProperty]
        public Event Event { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null) return NotFound();

            Event = await _eventService.GetEventByID(id);

            if (Event == null) return NotFound();
            
            ViewData["Venue"] = new SelectList(_venueService.GetVenuesBy(x => x.Status != false), "VenueId", "VenueName");
            ViewData["Category"] = new SelectList(_categoryService.GetCategoriesBy(x => x.Status != false), "CategoryId", "Name");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(IFormFile customFile)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Venue"] = new SelectList(_venueService.GetVenuesBy(x => x.Status != false), "VenueId", "VenueName");
                ViewData["Category"] = new SelectList(_categoryService.GetCategoriesBy(x => x.Status != false), "CategoryId", "Name");
                return Page();
            }
            if (Event.StartDate < (DateTime.Now - TimeSpan.FromMinutes(15)) || Event.EndDate < (DateTime.Now - TimeSpan.FromMinutes(15)))
            {
                TempData["error"] = "Start date and End date cannot be less than now";
                ViewData["Category"] = new SelectList(_categoryService.GetCategoriesBy(x => x.Status != false), "CategoryId", "Name");
                ViewData["Venue"] = new SelectList(_venueService.GetVenuesBy(x => x.Status != false), "VenueId", "VenueName");
                return Page();
            }

            if (DateTime.Compare(Event.StartDate, Event.EndDate) >= 0)
            {
                TempData["error"] = "Start date cannot be greater than end date";
                ViewData["Category"] = new SelectList(_categoryService.GetCategoriesBy(x => x.Status != false), "CategoryId", "Name");
                ViewData["Venue"] = new SelectList(_venueService.GetVenuesBy(x => x.Status != false), "VenueId", "VenueName");
                return Page();
            }
            if (customFile != null)
            {
                string NewImageUrl = await UploadImage.UploadFile(customFile, _env);
                Event.ImageUrl = NewImageUrl;
            }

            await _eventService.UpdateEvent(_context.Attach(Event).Entity);

            return RedirectToPage("./Index");
        }
    }
}
