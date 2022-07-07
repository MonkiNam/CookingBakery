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

        public void LikeEvent(EventLike likeInfo)
        {
            _db.EventLikes.Add(likeInfo);
            _db.SaveChanges();
        }

        public void UnlikeEvent(EventLike likeInfo)
        {
            _db.EventLikes.Remove(likeInfo);
            _db.SaveChanges();
        }

        public bool isExist(string email)
        {
            return _db.Users.Any(u => u.Email == email);
        }
    }
}