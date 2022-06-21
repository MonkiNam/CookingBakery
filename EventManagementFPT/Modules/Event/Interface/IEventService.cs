using EventManagementFPT.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

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
