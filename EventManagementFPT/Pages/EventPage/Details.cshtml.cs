using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EventManagementFPT.Model;
using EventManagementFPT.Modules.EventModule.Interface;

namespace EventManagementFPT.Pages.EventPage
{
    public class DetailsModel : PageModel
    {
        private readonly EventManagementFPT.Model.EventManagementContext _context;
        private readonly IEventService _eventService;

        public DetailsModel(EventManagementFPT.Model.EventManagementContext context, IEventService eventService)
        {
            _context = context;
            _eventService = eventService;
        }

        public Event Event { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Event = _eventService.GetEventByID(id);

            if (Event == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
