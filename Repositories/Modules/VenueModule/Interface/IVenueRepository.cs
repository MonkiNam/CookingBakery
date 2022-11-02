using CookingBakery.Model;
using CookingBakery.Utils.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace CookingBakery.Modules.VenueModule.Interface
{
    public interface IVenueRepository : IRepository<Venue>
    {
        public ICollection<Venue> GetVenuesBy(
            Expression<Func<Venue, bool>> filter = null,
            Func<IQueryable<Venue>, ICollection<Venue>> options = null,
            string includeProperties = null
        );
    }
}
