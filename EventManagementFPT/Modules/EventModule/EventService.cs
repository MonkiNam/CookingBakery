using EventManagementFPT.Model;
using EventManagementFPT.Modules.EventModule.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EventManagementFPT.Modules.EventModule
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;

        public EventService(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public ICollection<Event> GetEventsByName(string name, Func<IQueryable<Event>, ICollection<Event>> options = null,
            string includeProperties = null)
        {
            return _eventRepository.GetEventsBy(
                x => string.Equals(x.Name, name, StringComparison.OrdinalIgnoreCase),
                options,
                includeProperties
            );
        }

        public ICollection<Event> GetEventsByVenue(string venue)
        {
            return _eventRepository.GetEventsBy(
                x => string.Equals(x.Venue, venue, StringComparison.OrdinalIgnoreCase)
            );
        }

        public ICollection<Event> GetAll()
        {
            return _eventRepository.GetAll().ToList();
        }
    }
}