using System.Linq;
using EventManagementFPT.Model;
using EventManagementFPT.Modules.UserModule.Interface;
using EventManagementFPT.Utils.Repository;

namespace EventManagementFPT.Modules.UserModule
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly EventManagementContext _db;

        public UserRepository(EventManagementContext db) : base(db)
        {
            _db = db;
        }

        public void FollowEvent(User user, Event followingEvent)
        {
            var followInfo = new FollowEvent
            {
                EventId = followingEvent.EventId,
                UserId = user.UserId
            };
            _db.FollowEvents.Add(followInfo);
            _db.SaveChanges();
        }

        public void UnfollowEvent(User user, Event followingEvent)
        {
            var followInfo = _db.FollowEvents.FirstOrDefault(
                item => item.EventId.Equals(followingEvent.EventId) && item.UserId.Equals(user.UserId)
            );
            if (followInfo != null) _db.FollowEvents.Remove(followInfo);
            _db.SaveChanges();
        }

        public void LikeEvent(User user, Event followingEvent)
        {
            var likeInfo = new EventLike
            {
                EventId = followingEvent.EventId,
                UserId = user.UserId
            };
            _db.EventLikes.Add(likeInfo);
            _db.SaveChanges();
        }

        public void UnlikeEvent(User user, Event followingEvent)
        {
            var likeInfo = _db.EventLikes.FirstOrDefault(
                item => item.EventId.Equals(followingEvent.EventId) && item.UserId.Equals(user.UserId)
            );
            if (likeInfo != null) _db.EventLikes.Remove(likeInfo);
            _db.SaveChanges();
        }
    }
}