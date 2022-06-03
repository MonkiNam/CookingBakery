using EventManagementFPT.Model;
using EventManagementFPT.Utils.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace EventManagementFPT.Modules.Event.Interface
{
    public interface IEventRepository : IRepository<TblEvent>
    {
        public ICollection<TblEvent> GetEventsBy(
            Expression<Func<TblEvent, bool>> filter = null,
            Func<IQueryable<TblEvent>, ICollection<TblEvent>> options = null,
            string includeProperties = null
            );
    }
}
