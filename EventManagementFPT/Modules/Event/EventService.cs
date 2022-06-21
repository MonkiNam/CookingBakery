using EventManagementFPT.Modules.Event.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EventManagementFPT.Modules.Event
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;

        public EventService(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public ICollection<Model.Event> GetEventsByName(string name)
        {
            return _eventRepository.GetEventsBy(
                x => string.Equals(x.Name, name, StringComparison.OrdinalIgnoreCase)
            );
        }

        public ICollection<Model.Event> GetEventsByVenue(string venue)
        {
            return _eventRepository.GetEventsBy(
                x => string.Equals(x.Venue, venue, StringComparison.OrdinalIgnoreCase)
            );
        }
    }
}