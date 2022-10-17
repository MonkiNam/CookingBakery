using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CookingBakery.Model;
using CookingBakery.Modules.VenueModule.Interface;
using Microsoft.AspNetCore.Authorization;

namespace CookingBakery.Pages.VenuePage
{
    [Authorize(Roles = "Admin")]
    public class DetailsModel : PageModel
    {
        private readonly IVenueService _venueService;

        public DetailsModel(IVenueService venueService)
        {
            _venueService = venueService;
        }

        public Venue Venue { get; set; }

        public IActionResult OnGet(Guid? id)
        {
            if (id == null) return NotFound();

            Venue = _venueService.GetVenueByID(id);

            if (Venue == null) return NotFound();
            
            return Page();
        }
    }
}
