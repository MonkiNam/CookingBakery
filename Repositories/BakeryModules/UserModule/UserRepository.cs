using Repositories.BakeryModules.UserModule.Interface;
using BussinessObject.Models;
using Repositories.Utils.BakeryRepository;
using System.Linq;

namespace Repositories.BakeryModules.UserModule
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
            var post = _db.Posts.Where(x => x.PostId.Equals(likeInfo.PostId)).FirstOrDefault();
            post.Reaction = post.Reaction + 1;
            _db.Posts.Attach(post);
            _db.Entry(post).Property(x => x.Reaction).IsModified = true;
            _db.PostReactions.Add(likeInfo);
            _db.SaveChanges();
        }

        public void UnlikePost(PostReaction likeInfo)
        {
            var post = _db.Posts.Where(x => x.PostId.Equals(likeInfo.PostId)).FirstOrDefault();
            post.Reaction = post.Reaction - 1;
            _db.Posts.Attach(post);
            _db.Entry(post).Property(x => x.Reaction).IsModified = true;
            _db.PostReactions.Remove(likeInfo);
            _db.SaveChanges();
        }

        public bool isExist(string email)
        {
            return _db.Users.Any(u => u.Email == email);
        }
    }
}
