using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EventManagementFPT.Model;
using EventManagementFPT.Modules.VenueModule.Interface;
using Microsoft.AspNetCore.Authorization;

namespace EventManagementFPT.Pages.VenuePage
{
    [Authorize(Roles = "Admin")]
    public class DeleteModel : PageModel
    {
        private readonly IVenueService _venueService;

        public DeleteModel(IVenueService venueService)
        {
            _venueService = venueService;
        }

        [BindProperty]
        public Venue Venue { get; set; }

        public IActionResult OnGet(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Venue = _venueService.GetVenueByID(id);

            if (Venue == null) return NotFound();
            
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null) return NotFound();

            await _venueService.DeleteVenue(id);

            return RedirectToPage("./Index");
        }
    }
}
