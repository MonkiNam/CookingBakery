using EventManagementFPT.Model;
using EventManagementFPT.Modules.CategoryModule.Interface;
using EventManagementFPT.Modules.EventModule.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventManagementFPT.Modules.EventModule
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;
        private readonly ICategoryRepository _categoryRepository;

        public EventService(IEventRepository eventRepository, ICategoryRepository categoryRepository)
        {
            _eventRepository = eventRepository;
            _categoryRepository = categoryRepository;
        }

        public ICollection<Event> GetNewestEvents(int quantity)
        {
            var list = _eventRepository.GetAll(options: o => o.OrderBy(p => p.CreateDate).Where(x => x.Status == true).Take(quantity).ToList());
            return (list);
        }

        public ICollection<Event> GetEventsByName(string name, Func<IQueryable<Event>, ICollection<Event>> options = null,
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
            return _eventRepository.GetAll().Join(_categoryRepository.GetAll(), x => x.Category, y => y.CategoryId, (x, y) => new {
                _event = x
            }).Select(x => x._event).Where(x => x.Status == true).ToList();
        }

        public Event GetEventByID(Guid? eventID)
        {
            return _eventRepository.GetFirstOrDefaultAsync(x => x.EventId.Equals(eventID) && x.Status == true).Result;
        }

        public ICollection<Event> GetAll()
        {
            return _eventRepository.GetAll().ToList();
        }

        public async Task AddNewEvent(Event newEvent)
        {
            newEvent.CreateDate = DateTime.Now;
            newEvent.EventId = Guid.NewGuid();
            await _eventRepository.AddAsync(newEvent);
        }
        public async Task UpdateEvent(Event eventUpdate)
        {
            await _eventRepository.UpdateAsync(eventUpdate);
        }
        public async Task DeleteEvent(Guid? id)
        {
            Event eventDelete = _eventRepository.GetFirstOrDefaultAsync(x => x.EventId.Equals(id) && x.Status == true).Result;
            if (eventDelete == null) return;
            eventDelete.Status = false;
            if(eventDelete != null ) await _eventRepository.UpdateAsync(eventDelete);
        }
    }
}