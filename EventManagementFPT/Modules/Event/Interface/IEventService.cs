using System.Collections.Generic;

namespace EventManagementFPT.Modules.Event.Interface
{
    public interface IEventService
    {
        public ICollection<TblEvent> GetEventsByName(string name, Func<IQueryable<TblEvent>, ICollection<TblEvent>> options = null,
            string includeProperties = null);
        public ICollection<TblEvent> GetEventsByVenue(string venue);
        public ICollection<TblEvent> GetAll();
    }
}
