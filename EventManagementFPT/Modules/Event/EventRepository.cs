using EventManagementFPT.Model;
using EventManagementFPT.Modules.Event.Interface;
using EventManagementFPT.Utils.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace EventManagementFPT.Modules.Event
{
    public class EventRepository : Repository<TblEvent>, IEventRepository
    {
        private readonly EventManagementContext Db;
        public EventRepository(EventManagementContext db) : base(db)
        {
            this.Db = db;
        }
        public ICollection<TblEvent> GetEventsBy(Expression<Func<TblEvent, bool>> filter = null,
            Func<IQueryable<TblEvent>, ICollection<TblEvent>> options = null,
            string includeProperties = null)
        {
            IQueryable<TblEvent> query = DbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }

            if (options != null)
            {
                return options(query).ToList();
            }

            return query.ToList();
        }
    }
}
