using System.Collections.Generic;
using EventManagementFPT.Model;
using EventManagementFPT.Modules.VenueModule.Interface;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EventManagementFPT.Pages.VenuePage
{
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
