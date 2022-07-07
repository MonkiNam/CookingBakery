using EventManagementFPT.Model;
using EventManagementFPT.Utils.Repository.Interface;

namespace EventManagementFPT.Modules.UserModule.Interface
{
    public interface IUserRepository : IRepository<User>
    {
        public void LikeEvent(EventLike _event);
        public void UnlikeEvent(EventLike _event);
        public bool isExist(string email);
    }
}
