using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using CookingBakery.Model;
using CookingBakery.Modules.EventModule.Interface;
using CookingBakery.Utils.Repository;
using Microsoft.EntityFrameworkCore;

namespace CookingBakery.Modules.EventModule
{
    public class EventRepository : Repository<Event>, IEventRepository
    {
        private readonly EventManagementContext _db;

        public EventRepository(EventManagementContext db) : base(db)
        {
            _db = db;
        }

        public ICollection<Event> GetEventsBy(Expression<Func<Event, bool>> filter = null,
            Func<IQueryable<Event>, ICollection<Event>> options = null,
            string includeProperties = null)
        {
            IQueryable<Event> query = DbSet;
            
            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }

            return options != null ? options(query).ToList() : query.ToList();
        }
    }
}