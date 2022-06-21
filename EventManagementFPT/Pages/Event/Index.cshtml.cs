using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EventManagementFPT.Model;
using EventManagementFPT.Modules.Event.Interface;

namespace EventManagementFPT.Pages.Event
{
    public class IndexModel : PageModel
    {
        private readonly IEventService eventService;

        public IndexModel(IEventService _eventService)
        {
            eventService = _eventService;
        }

        public IList<TblEvent> TblEvent { get;set; }

        public void OnGet()
        {
            TblEvent = eventService.GetAll().ToList();
        }
    }
}
