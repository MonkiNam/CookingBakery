using System.Threading.Tasks;
using EventManagementFPT.Model;
using EventManagementFPT.Modules.VenueModule.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EventManagementFPT.Pages.VenuePage
{
    [Authorize(Roles = "Admin")]
    public class CreateModel : PageModel
    {
        private readonly IVenueService _venueService;
        public CreateModel(IVenueService venueService)
        {
            _venueService = venueService;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Venue Venue { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _venueService.AddNewVenue(Venue);

            return RedirectToPage("./Index");
        }
    }
}
