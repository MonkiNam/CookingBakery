using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EventManagementFPT.Model;
using EventManagementFPT.Modules.VenueModule.Interface;

namespace EventManagementFPT.Pages.VenuePage
{
    public class DeleteModel : PageModel
    {
        private readonly IVenueService _venueService;

        public DeleteModel(IVenueService venueService)
        {
            _venueService = venueService;
        }

        [BindProperty]
        public Venue Venue { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Venue = _venueService.GetVenueByID(id);

            if (Venue == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            await _venueService.DeleteVenue(id);

            return RedirectToPage("./Index");
        }
    }
}
