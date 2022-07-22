using Microsoft.AspNetCore.Mvc.RazorPages;
using EventManagementFPT.Model;
using EventManagementFPT.Modules.EventModule.Interface;
using EventManagementFPT.Utils;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace EventManagementFPT.Pages.EventPage
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
            var events = _eventService.GetAll().AsQueryable();
            Event = PaginatedList<Event>.Create(events, pageIndex ?? 1, 5);
        }
    }
}
