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
        public ICollection<TblEvent> GetEventsByName(string name);
        public ICollection<TblEvent> GetEventsByVenue(string venue);
    }
}
