using Repositories.BakeryModules.PostReactionModule.Interface;
using BussinessObject.Models;
using Repositories.Utils.BakeryRepository;
using System;
using System.Linq;

namespace Repositories.BakeryModules.PostReactionModule
{
    public class PostReactionRepository : Repository<PostReaction>, IPostReactionRepository
    {
        private readonly CookingBakeryContext _db;

        public PostReactionRepository(CookingBakeryContext db) : base(db)
        {
            _db = db;
        }

        public int CountLikeOfPost(Guid? postId)
        {
            return _db.PostReactions.Where(x => x.PostId.Equals(postId)).ToList().Count;
        }
    }
}
