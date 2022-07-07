using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EventManagementFPT.Model;
using EventManagementFPT.Modules.VenueModule.Interface;

namespace EventManagementFPT.Pages.VenuePage
{
    public class IndexModel : PageModel
    {
        private readonly EventManagementFPT.Model.EventManagementContext _context;
        private readonly IVenueService _venueService;

        public IndexModel(EventManagementFPT.Model.EventManagementContext context, IVenueService venueService)
        {
            _venueService = venueService;
            _context = context;
        }

        public IList<Venue> Venue { get;set; }

        public async Task OnGetAsync()
        {
            Venue = (IList<Venue>)_venueService.GetAll();
        }
    }
}
