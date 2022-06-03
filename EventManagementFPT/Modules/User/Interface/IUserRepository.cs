using EventManagementFPT.Model;
using EventManagementFPT.Utils.Repository.Interface;

namespace EventManagementFPT.Modules.User.Interface
{
    public interface IUserRepository : IRepository<TblUser>
    {
        public void FollowEvent(TblUser user, TblEvent followingEvent);
        public void UnfollowEvent(TblUser user, TblEvent followingEvent);
        public void LikeEvent(TblUser user, TblEvent followingEvent);
        public void UnlikeEvent(TblUser user, TblEvent followingEvent);
    }
}
