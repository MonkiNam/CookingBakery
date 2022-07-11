using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using EventManagementFPT.Model;
using EventManagementFPT.Modules.EventModule.Interface;
using Microsoft.AspNetCore.Http;
using EventManagementFPT.Utils;
using Microsoft.AspNetCore.Hosting;
using System.Security.Claims;

namespace EventManagementFPT.Pages.EventPage
{
    public class CreateModel : PageModel
    {
        private readonly EventManagementFPT.Model.EventManagementContext _context;
        private readonly IEventService _eventService;
        private readonly IWebHostEnvironment _env;

        public CreateModel(EventManagementFPT.Model.EventManagementContext context, IEventService eventService, IWebHostEnvironment env)
        {
            _context = context;
            _eventService = eventService;
            _env = env; 
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
        public async Task<IActionResult> OnPostAsync(IFormFile customFile)
        {
            if (!ModelState.IsValid)
            {
                TempData["error"] = "Invalid data";
                ViewData["Category"] = new SelectList(_context.Categories, "CategoryId", "Name");
                ViewData["Venue"] = new SelectList(_context.Venues, "VenueId", "VenueName");
                return Page();
            }
            if (DateTime.Compare(Event.StartDate, Event.EndDate) >= 0)
            {
                TempData["error"] = "Start date cannot be greater than end date";
                ViewData["Category"] = new SelectList(_context.Categories, "CategoryId", "Name");
                ViewData["Venue"] = new SelectList(_context.Venues, "VenueId", "VenueName");
                return Page();
            }
            TempData["success"] = "Add success";
            string imageUrl = await UploadImage.UploadFile(customFile, _env);
            Event.ImageUrl = imageUrl;
            await _eventService.AddNewEvent(Event, User.FindFirst(ClaimTypes.NameIdentifier).Value);
            return RedirectToPage("./Index");
            //return RedirectToPage("./Index");
        }
    }
}
