using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CookingBakery.Model;
using CookingBakery.Modules.EventModule.Interface;
using Microsoft.AspNetCore.Authorization;

namespace CookingBakery.Pages.EventPage
{
    [Authorize(Roles="Admin, Host")]
    public class DeleteModel : PageModel
    {
        private readonly IEventService _eventService;

        public DeleteModel(IEventService eventService)
        {
            _eventService = eventService;
        }

        [BindProperty] public Event Event { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null) return NotFound();

            Event = await _eventService.GetEventByID(id);

            if (Event == null) return NotFound();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null) return NotFound();

            await _eventService.DeleteEvent(id);

            return RedirectToPage("./Index");
        }
    }
}