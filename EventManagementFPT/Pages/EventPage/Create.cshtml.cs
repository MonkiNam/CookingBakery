using System;
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
using EventManagementFPT.Modules.CategoryModule.Interface;
using EventManagementFPT.Modules.VenueModule.Interface;
using Microsoft.AspNetCore.Authorization;

namespace EventManagementFPT.Pages.EventPage
{
    [Authorize(Roles="Admin, Host")]
    public class CreateModel : PageModel
    {
        private readonly IEventService _eventService;
        private readonly IWebHostEnvironment _env;
        private readonly ICategoryService _categoryService;
        private readonly IVenueService _venueService;

        public CreateModel(IEventService eventService,
            IWebHostEnvironment env, ICategoryService categoryService, IVenueService venueService)
        {
            _eventService = eventService;
            _env = env;
            _categoryService = categoryService;
            _venueService = venueService;
        }

        public IActionResult OnGet()
        {
            var role = User.FindFirst(ClaimTypes.Role)?.Value;
            if (role is "Admin" or "Host")
            {
                ViewData["Category"] = new SelectList(_categoryService.GetCategoriesBy(x => x.Status != false), "CategoryId", "Name");
                ViewData["Venue"] = new SelectList(_venueService.GetVenuesBy(x => x.Status != false), "VenueId", "VenueName");
                TempData["success"] = "Page loaded!";

                return Page();
            }

            return RedirectToPage("/Home/Index");
        }

        [BindProperty] public Event Event { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(IFormFile customFile)
        {
            var uid = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (uid == null) return RedirectToPage("/Authentication/Index");

            var role = User.FindFirst(ClaimTypes.Role)?.Value;
            if (role is "Admin" or "Host")
            {
                if (!ModelState.IsValid)
                {
                    TempData["error"] = "Invalid data";
                    ViewData["Category"] = new SelectList(_categoryService.GetCategoriesBy(x => x.Status != false), "CategoryId", "Name");
                    ViewData["Venue"] = new SelectList(_venueService.GetVenuesBy(x => x.Status != false), "VenueId", "VenueName");
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

                TempData["success"] = "Add success";
                if (customFile != null)
                {
                    string imageUrl = await UploadImage.UploadFile(customFile, _env);
                    Event.ImageUrl = imageUrl;
                }
                await _eventService.AddNewEvent(Event, uid);
                return RedirectToPage("./Index");
            }

            return RedirectToPage("/Home");
        }
    }
}