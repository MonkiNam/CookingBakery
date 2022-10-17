using CookingBakery.Models;
using CookingBakery.Utils.BakeryRepository.Interface;

namespace CookingBakery.BakeryModules.UserModule.Interface
{
    public interface IUserRepository : IRepository<User>
    {
        public void LikePost(PostReaction _post);
        public void UnlikePost(PostReaction _post);
        public bool isExist(string email);
    }
}
