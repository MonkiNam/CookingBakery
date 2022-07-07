using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EventManagementFPT.Model;
using EventManagementFPT.Modules.EventModule.Interface;

namespace EventManagementFPT.Pages.EventPage
{
    public class IndexModel : PageModel
    {
        private readonly IEventService _eventService;

        public IndexModel(IEventService eventService)
        {
            _eventService = eventService;
        }

        public IList<Event> Event { get;set; }

        public void OnGet()
        {
            Event = (IList<Event>)_eventService.GetAll();
        }
    }
}
