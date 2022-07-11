using EventManagementFPT.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventManagementFPT.Modules.EventModule.Interface
{
    public interface IEventService
    {
        public ICollection<Event> GetEventsByName(string name, Func<IQueryable<Event>, ICollection<Event>> options = null,
            string includeProperties = null);
        public ICollection<Event> GetEventsByVenue(Guid venue);
        public ICollection<Event> GetNewestEvents(int quantity);
        public ICollection<Event> GetAll();
        public ICollection<Event> GetEventsByCategory(Guid? categoryID);
        public Task<Event> GetEventByID(Guid? ID);
        public Task AddNewEvent(Event newEvent, string uid);
        public Task UpdateEvent(Event eventUpdate);
        public Task DeleteEvent(Guid? ID);
    }
}
