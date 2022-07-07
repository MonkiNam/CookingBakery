using EventManagementFPT.Model;
using EventManagementFPT.Modules.UserEventModule.Interface;
using EventManagementFPT.Utils.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EventManagementFPT.Modules.UserEventModule
{
    public class UserEventRepository : Repository<UserEvent>, IUserEventRepository
    {
        private readonly EventManagementContext _db;

        public UserEventRepository(EventManagementContext db) : base(db)
        {
            _db = db;
        }

        public ICollection<UserEvent> GetUserEventsBy(Expression<Func<UserEvent, bool>> filter = null,
            Func<IQueryable<UserEvent>, ICollection<UserEvent>> options = null,
            string includeProperties = null)
        {
            IQueryable<UserEvent> query = DbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            return options != null ? options(query).ToList() : query.ToList();
        }
    }
}
