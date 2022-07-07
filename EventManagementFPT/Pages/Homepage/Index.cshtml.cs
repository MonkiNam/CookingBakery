using EventManagementFPT.Model;
using EventManagementFPT.Modules.EventModule.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace EventManagementFPT.Pages.Homepage
{
    public class IndexModel : PageModel
    {   


        private readonly IEventService _eventservice;

        public IndexModel(IEventService EventService)
        {
            _eventservice = EventService;
        }

        public List<Event> EventList { get; set; }


        public void OnGet()
        {
            //Lay 3 event moi nhat

            //Lay het
            EventList = (List<Event>)_eventservice.GetAll();
        }
    }
}
