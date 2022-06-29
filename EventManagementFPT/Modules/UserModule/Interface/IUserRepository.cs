using EventManagementFPT.Model;
using EventManagementFPT.Utils.Repository.Interface;

namespace EventManagementFPT.Modules.UserModule.Interface
{
    public interface IUserRepository : IRepository<User>
    {
        public void FollowEvent(FollowEvent followingEvent);
        public void UnfollowEvent(FollowEvent followingEvent);
        public void LikeEvent(EventLike _event);
        public void UnlikeEvent(EventLike _event);
    }
}
