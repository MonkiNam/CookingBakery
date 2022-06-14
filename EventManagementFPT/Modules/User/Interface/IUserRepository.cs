using EventManagementFPT.Utils.Repository.Interface;

namespace EventManagementFPT.Modules.User.Interface
{
    public interface IUserRepository : IRepository<Model.User>
    {
        public void FollowEvent(Model.User user, Model.Event followingEvent);
        public void UnfollowEvent(Model.User user, Model.Event followingEvent);
        public void LikeEvent(Model.User user, Model.Event followingEvent);
        public void UnlikeEvent(Model.User user, Model.Event followingEvent);
    }
}
