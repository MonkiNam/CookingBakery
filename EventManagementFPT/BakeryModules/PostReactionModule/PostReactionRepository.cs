using CookingBakery.BakeryModules.PostReactionModule.Interface;
using CookingBakery.Models;
using CookingBakery.Utils.BakeryRepository;
using System;
using System.Linq;

namespace CookingBakery.BakeryModules.PostReactionModule
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
