using CookingBakery.BakeryModules.UserModule.Interface;
using CookingBakery.Models;
using CookingBakery.Utils.BakeryRepository;
using System.Linq;

namespace CookingBakery.BakeryModules.UserModule
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly CookingBakeryContext _db;

        public UserRepository(CookingBakeryContext db) : base(db)
        {
            _db = db;
        }

        public void LikePost(PostReaction likeInfo)
        {
            _db.PostReactions.Add(likeInfo);
            _db.SaveChanges();
        }

        public void UnlikePost(PostReaction likeInfo)
        {
            _db.PostReactions.Remove(likeInfo);
            _db.SaveChanges();
        }

        public bool isExist(string email)
        {
            return _db.Users.Any(u => u.Email == email);
        }
    }
}
