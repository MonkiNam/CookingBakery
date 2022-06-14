using System.Collections.Generic;

namespace EventManagementFPT.Modules.Event.Interface
{
    public interface IEventService
    {
        public ICollection<Model.Event> GetEventsByName(string name);
        public ICollection<Model.Event> GetEventsByVenue(string venue);
    }
}
