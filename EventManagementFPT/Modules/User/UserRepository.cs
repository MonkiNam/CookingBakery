using System.Linq;
using EventManagementFPT.Model;
using EventManagementFPT.Modules.User.Interface;
using EventManagementFPT.Utils.Repository;

namespace EventManagementFPT.Modules.User
{
    public class UserRepository : Repository<Model.User>, IUserRepository
    {
        private readonly EventManagementContext _db;

        public UserRepository(EventManagementContext db) : base(db)
        {
            _db = db;
        }

        public void FollowEvent(Model.User user, Model.Event followingEvent)
        {
            var followInfo = new FollowEvent
            {
                EventId = followingEvent.EventId,
                UserId = user.UserId
            };
            _db.FollowEvents.Add(followInfo);
            _db.SaveChanges();
        }

        public void UnfollowEvent(Model.User user, Model.Event followingEvent)
        {
            var followInfo = _db.FollowEvents.FirstOrDefault(
                item => item.EventId.Equals(followingEvent.EventId) && item.UserId.Equals(user.UserId)
            );
            if (followInfo != null) _db.FollowEvents.Remove(followInfo);
            _db.SaveChanges();
        }

        public void LikeEvent(Model.User user, Model.Event followingEvent)
        {
            var likeInfo = new EventLike
            {
                EventId = followingEvent.EventId,
                UserId = user.UserId
            };
            _db.EventLikes.Add(likeInfo);
            _db.SaveChanges();
        }

        public void UnlikeEvent(Model.User user, Model.Event followingEvent)
        {
            var likeInfo = _db.EventLikes.FirstOrDefault(
                item => item.EventId.Equals(followingEvent.EventId) && item.UserId.Equals(user.UserId)
            );
            if (likeInfo != null) _db.EventLikes.Remove(likeInfo);
            _db.SaveChanges();
        }
    }
}