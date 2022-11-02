using Repositories.BakeryModules.PostDetailModule.Interface;
using BussinessObject.Models;
using System.Collections.Generic;
using System.Linq;
using System;
using Repositories.Utils.BakeryRepository;

namespace Repositories.BakeryModules.PostDetailModule
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
