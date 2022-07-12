using System.Collections.Generic;
using System.Linq;
using EventManagementFPT.Model;
using EventManagementFPT.Modules.EventModule.Interface;
using EventManagementFPT.Utils;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EventManagementFPT.Pages.Home
{
    public class Index : PageModel
    {
        private readonly IEventService _eventService;

        public Index(IEventService eventService)
        {
            _eventService = eventService;
        }

        public PaginatedList<Event> Event { get;set; }
        public IEnumerable<Event> NewestEvent { get;set; }
        
        public void OnGetAsync(int? pageIndex)
        {
            var events = _eventService.GetAll().Where(o => o.Status).AsQueryable();
            Event = PaginatedList<Event>.Create(events, pageIndex ?? 1, 10);
            NewestEvent = _eventService.GetNewestEvents(3);
        }
    }
}