using EventManagementFPT.Model;
using EventManagementFPT.Modules.FollowEventModule.Interface;
using EventManagementFPT.Utils.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventManagementFPT.Modules.FollowEventModule
{
    public class FollowEventRepository : Repository<FollowEvent>, IFollowEventRepository
    {
        private readonly EventManagementContext _db;

        public FollowEventRepository(EventManagementContext db) : base(db)
        {
            _db = db;
        }
    }
}
