using EventManagementFPT.Utils.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace EventManagementFPT.Modules.Event.Interface
{
    public interface IEventRepository : IRepository<Model.Event>
    {
        public ICollection<Model.Event> GetEventsBy(
            Expression<Func<Model.Event, bool>> filter = null,
            Func<IQueryable<Model.Event>, ICollection<Model.Event>> options = null,
            string includeProperties = null
        );
    }
}