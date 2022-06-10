using EventManagementFPT.Model;
using EventManagementFPT.Modules.Event.Interface;
using System;
using System.Collections.Generic;

namespace EventManagementFPT.Modules.Event
{
    public class EventService : IEventService
    {
        IEventRepository eventRepository;
        public EventService(IEventRepository _eventRepository)
        {
            eventRepository = _eventRepository;
        }
        public ICollection<TblEvent> GetEventsByName(string name) => eventRepository.GetEventsBy(x => string.Equals(x.Name, name, StringComparison.OrdinalIgnoreCase));
        public ICollection<TblEvent> GetEventsByVenue(string venue) => eventRepository.GetEventsBy(x => string.Equals(x.Venue, venue, StringComparison.OrdinalIgnoreCase));
    }
}
