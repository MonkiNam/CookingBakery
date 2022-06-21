using EventManagementFPT.Model;
using EventManagementFPT.Utils.Repository.Interface;

namespace EventManagementFPT.Modules.UserModule.Interface
{
    public interface IUserRepository : IRepository<User>
    {
        public void FollowEvent(User user, Event followingEvent);
        public void UnfollowEvent(User user, Event followingEvent);
        public void LikeEvent(User user, Event followingEvent);
        public void UnlikeEvent(User user, Event followingEvent);
    }
}
