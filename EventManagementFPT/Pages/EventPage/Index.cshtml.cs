using System;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CookingBakery.Model;
using CookingBakery.Modules.EventModule.Interface;
using CookingBakery.Utils;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CookingBakery.Pages.EventPage
{
    [Authorize(Roles="Admin, Host")]
    public class IndexModel : PageModel
    {
        private readonly IEventService _eventService;

        public IndexModel(IEventService eventService)
        {
            _eventService = eventService;
        }

        public PaginatedList<Event> Event { get;set; }

        public void OnGet(int? pageIndex)
        {
            var role = User.FindFirst(ClaimTypes.Role)?.Value;
            var uid = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            var events = _eventService.GetAll().AsQueryable();
            if (role == "Host")
            {
                events = events.Where(o => o.UserEvents.Any(u => u.UserId == Guid.Parse(uid) && u.IsHost));
            }
            Event = PaginatedList<Event>.Create(events, pageIndex ?? 1, 5);
        }
    }
}
