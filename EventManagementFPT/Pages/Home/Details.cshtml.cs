using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using EventManagementFPT.Model;
using EventManagementFPT.Modules.EventLikeModule.Interface;
using EventManagementFPT.Modules.EventModule.Interface;
using EventManagementFPT.Modules.UserEventModule.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EventManagementFPT.Pages.Home
{
    public class Details : PageModel
    {
        private readonly IEventService _eventService;
        private readonly IUserEventService _userEventService;
        private readonly IEventLikeService _eventLikeService;
        private readonly EventManagementContext _context;

        public Details(IEventService eventService,IUserEventService userEventService, IEventLikeService eventLikeService, EventManagementContext context)
        {
            _eventService = eventService;
            _userEventService = userEventService;
            _eventLikeService = eventLikeService;
            _context = context;
        }

        public Event Event { get; set; }
        public ICollection<User> UserEvent { get; set; }
        public int EventLike { get; set; }
        public bool IsLikeEvent { get; set; }
        public User HostUser { get; set; }

        public async Task<IActionResult> OnGet(Guid id)
        {
            var uid = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            Event = await _eventService.GetEventByID(id);
            UserEvent = _userEventService.GetUserGoingOfEvent(id);
            EventLike = _eventLikeService.CountLikeOfEvent(id);
            if (uid != null)
            {
                var userId = Guid.Parse(uid);
                IsLikeEvent = _context.EventLikes.Any(o => o.UserId == userId && o.EventId == id);
            }
            HostUser = _context.UserEvents.FirstOrDefault(o => o.IsHost == true && o.EventId == id)?.User;
            
            return Page();
        }
    }
}