using EventManagementFPT.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EventManagementFPT.Modules.EventModule.Interface
{
    public interface IEventService
    {
        public ICollection<Event> GetEventsByName(string name, Func<IQueryable<Event>, ICollection<Event>> options = null,
            string includeProperties = null);
        public ICollection<Event> GetEventsByVenue(string venue);
        public ICollection<Event> GetAll();
    }
}
