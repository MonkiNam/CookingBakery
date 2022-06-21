using EventManagementFPT.Utils.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using EventManagementFPT.Model;

namespace EventManagementFPT.Modules.EventModule.Interface
{
    public interface IEventRepository : IRepository<Event>
    {
        public ICollection<Event> GetEventsBy(
            Expression<Func<Event, bool>> filter = null,
            Func<IQueryable<Event>, ICollection<Event>> options = null,
            string includeProperties = null
        );
    }
}