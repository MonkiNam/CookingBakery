using EventManagementFPT.Model;
using EventManagementFPT.Modules.EventLikeModule.Interface;
using EventManagementFPT.Utils.Repository;
using System;
using System.Linq;

namespace EventManagementFPT.Modules.EventLikeModule
{
    public class EventLikeRepository : Repository<EventLike>, IEventLikeRepository
    {
        private readonly EventManagementContext _db;

        public EventLikeRepository(EventManagementContext db) : base(db)
        {
            _db = db;
        }

        public int CountLikeOfEvent(Guid? eventID)
        {
            return _db.EventLikes.Where(x => x.EventId.Equals(eventID)).ToList().Count;
        }
    }
}