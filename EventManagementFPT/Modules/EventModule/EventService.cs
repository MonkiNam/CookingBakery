using CookingBakery.Model;
using CookingBakery.Modules.CategoryModule.Interface;
using CookingBakery.Modules.EventModule.Interface;
using CookingBakery.Modules.UserEventModule;
using CookingBakery.Modules.UserEventModule.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CookingBakery.Modules.EventModule
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUserEventService _userEventService;

        public EventService(IEventRepository eventRepository, ICategoryRepository categoryRepository, IUserEventService userEventService)
        {
            _eventRepository = eventRepository;
            _categoryRepository = categoryRepository;
            _userEventService = userEventService;
        }

        public ICollection<Event> GetNewestEvents(int quantity)
        {
            var list = _eventRepository.GetAll(options: o =>
                o.OrderByDescending(p => p.CreateDate).Where(x => x.Status == true).Take(quantity).ToList());
            return (list);
        }

        public ICollection<Event> GetEventsByName(string name,
            Func<IQueryable<Event>, ICollection<Event>> options = null,
            string includeProperties = null)
        {
            return _eventRepository.GetEventsBy(
                x => string.Equals(x.Name, name, StringComparison.OrdinalIgnoreCase) && x.Status == true,
                options,
                includeProperties
            );
        }

        public ICollection<Event> GetEventsByVenue(Guid venueId)
        {
            return _eventRepository.GetEventsBy(x => x.VenueId == venueId && x.Status == true);
        }

        public ICollection<Event> GetEventsByCategory(Guid? categoryID)
        {
            return _eventRepository
                .GetAll()
                .Join(
                    _categoryRepository.GetAll(),
                    x => x.Category,
                    y => y.CategoryId,
                    (x, y) => new {_event = x}
                )
                .Select(x => x._event)
                .Where(x => x.Status)
                .ToList();
        }

        public async Task<Event> GetEventByID(Guid? eventID)
        {
            return await _eventRepository.GetFirstOrDefaultAsync(
                x => x.EventId.Equals(eventID),
                includeProperties: "Venue,CategoryNavigation,Comments"
            );
        }

        public ICollection<Event> GetAll()
        {
            ICollection<Event> events = _eventRepository.GetAll(includeProperties: "Venue,CategoryNavigation,UserEvents");
            if (events != null) return events.ToList();
            return null;
        }

        public async Task AddNewEvent(Event newEvent, string uid)
        {
            Guid _uid = Guid.Parse(uid);
            newEvent.CreateDate = DateTime.Now;
            newEvent.EventId = Guid.NewGuid();
          

            await _eventRepository.AddAsync(newEvent);
            await _userEventService.GoingAnEvent(_uid, newEvent.EventId, true);
        }

        public async Task UpdateEvent(Event eventUpdate)
        {
            await _eventRepository.UpdateAsync(eventUpdate);
        }

        public async Task DeleteEvent(Guid? id)
        {
            Event eventDelete = await _eventRepository.GetFirstOrDefaultAsync(
                x => x.EventId.Equals(id) && x.Status == true
            );
            if (eventDelete == null) return;
            eventDelete.Status = false;
            if (eventDelete != null) await _eventRepository.UpdateAsync(eventDelete);
        }
    }
}