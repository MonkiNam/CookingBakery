using System.Collections.Generic;
using CookingBakery.Model;
using CookingBakery.Modules.VenueModule.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CookingBakery.Pages.VenuePage
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly IVenueService _venueService;

        public IndexModel(IVenueService venueService)
        {
            _venueService = venueService;
        }

        public IList<Venue> Venue { get;set; }

        public void OnGetAsync()
        {
            Venue = (IList<Venue>)_venueService.GetAll();
        }
    }
}
