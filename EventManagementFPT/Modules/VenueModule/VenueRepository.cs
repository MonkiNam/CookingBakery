using CookingBakery.Model;
using CookingBakery.Modules.VenueModule.Interface;
using CookingBakery.Utils.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CookingBakery.Modules.VenueModule
{
    public class VenueRepository : Repository<Venue>, IVenueRepository
    {
        private readonly EventManagementContext _db;

        public VenueRepository(EventManagementContext db) : base(db)
        {
            _db = db;
        }
        public ICollection<Venue> GetVenuesBy(Expression<Func<Venue, bool>> filter = null,
            Func<IQueryable<Venue>, ICollection<Venue>> options = null,
            string includeProperties = null)
        {
            IQueryable<Venue> query = DbSet;

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
