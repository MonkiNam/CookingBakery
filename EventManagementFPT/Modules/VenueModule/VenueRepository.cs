using EventManagementFPT.Model;
using EventManagementFPT.Modules.VenueModule.Interface;
using EventManagementFPT.Utils.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventManagementFPT.Modules.VenueModule
{
    public class VenueRepository : Repository<Venue>, IVenueRepository
    {
        private readonly EventManagementContext _db;

        public VenueRepository(EventManagementContext db) : base(db)
        {
            _db = db;
        }
    }
}
