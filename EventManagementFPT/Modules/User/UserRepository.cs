using EventManagementFPT.Model;
using EventManagementFPT.Modules.User.Interface;
using EventManagementFPT.Utils.Repository;
using System.Linq;

namespace EventManagementFPT.Modules.User
{
    public class UserRepository : Repository<TblUser>, IUserRepository
    {
        private readonly EventManagementContext Db;
        public UserRepository(EventManagementContext db) : base(db)
        {
            this.Db = db;
        }
        public void FollowEvent(TblUser user, TblEvent followingEvent)
        {
            TblFollowEvent followInfo = new TblFollowEvent()
            {
                EventId = followingEvent.EventId,
                UserId = user.UserId
            };
            this.Db.TblFollowEvents.Add(followInfo);
            this.Db.SaveChanges();
        }
        public void UnfollowEvent(TblUser user, TblEvent followingEvent)
        {
            TblFollowEvent followInfo = this.Db.TblFollowEvents.FirstOrDefault(item => item.EventId.Equals(followingEvent.EventId) && item.UserId.Equals(user.UserId));
            this.Db.TblFollowEvents.Remove(followInfo);
            this.Db.SaveChanges();
        }
        public void LikeEvent(TblUser user, TblEvent followingEvent)
        {
            TblEventLike likeInfo = new TblEventLike()
            {
                EventId = followingEvent.EventId,
                UserId = user.UserId
            };
            this.Db.TblEventLikes.Add(likeInfo);
            this.Db.SaveChanges();
        }
        public void UnlikeEvent(TblUser user, TblEvent followingEvent)
        {
            TblEventLike likeInfo = this.Db.TblEventLikes.FirstOrDefault(item => item.EventId.Equals(followingEvent.EventId) && item.UserId.Equals(user.UserId));
            this.Db.TblEventLikes.Remove(likeInfo);
            this.Db.SaveChanges();
        }
    }
}
