using CookingBakery.BakeryModules.PostDetailModule.Interface;
using CookingBakery.Models;
using System.Collections.Generic;
using System.Linq;
using System;
using CookingBakery.Utils.BakeryRepository;

namespace CookingBakery.BakeryModules.PostDetailModule
{
    public class PostDetailRepository : Repository<PostDetail>, IPostDetailRepository
    {
        private readonly CookingBakeryContext _db;

        public PostDetailRepository(CookingBakeryContext db) : base(db)
        {
            _db = db;
        }

        public ICollection<PostDetail> GetPostDetailByPostId(Guid postId) => _db.PostDetails.Where(x => x.PostId == postId).ToList();
    }
}
