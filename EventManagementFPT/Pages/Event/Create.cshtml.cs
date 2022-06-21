using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using EventManagementFPT.Model;

namespace EventManagementFPT.Pages.Event
{
    public class CreateModel : PageModel
    {
        private readonly EventManagementFPT.Model.EventManagementContext _context;

        public CreateModel(EventManagementFPT.Model.EventManagementContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["Category"] = new SelectList(_context.TblCategories, "CategoryId", "Name");
            return Page();
        }

        [BindProperty]
        public TblEvent TblEvent { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.TblEvents.Add(TblEvent);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
