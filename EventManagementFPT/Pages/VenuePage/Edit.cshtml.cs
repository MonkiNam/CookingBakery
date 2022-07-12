using System;
using System.Threading.Tasks;
using EventManagementFPT.Model;
using EventManagementFPT.Modules.VenueModule.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EventManagementFPT.Pages.VenuePage
{
    public class EditModel : PageModel
    {
        private readonly EventManagementContext _context;
        private readonly IVenueService _venueService;

        public EditModel(EventManagementContext context, IVenueService venueService)
        {
            _context = context;
            _venueService = venueService;
        }

        [BindProperty]
        public Venue Venue { get; set; }

        public IActionResult OnGetAsync(Guid? id)
        {
            if (id == null) return NotFound();

            Venue = _venueService.GetVenueByID(id);

            if (Venue == null) return NotFound();
            
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            await _venueService.UpdateVenue(_context.Attach(Venue).Entity);

            return RedirectToPage("./Index");
        }
    }
}
