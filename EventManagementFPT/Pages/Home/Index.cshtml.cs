using System.Collections.Generic;
using EventManagementFPT.Model;
using EventManagementFPT.Modules.EventModule.Interface;
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

        public IEnumerable<Event> Event { get;set; }
        public IEnumerable<Event> NewestEvent { get;set; }
        
        public void OnGet()
        {
            Event = _eventService.GetAll();
            NewestEvent = _eventService.GetNewestEvents(3);
        }
    }
}