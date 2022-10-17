using CookingBakery.Model;
using CookingBakery.Modules.EventLikeModule.Interface;
using CookingBakery.Utils.Repository;
using System;
using System.Linq;

namespace CookingBakery.Modules.EventLikeModule
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