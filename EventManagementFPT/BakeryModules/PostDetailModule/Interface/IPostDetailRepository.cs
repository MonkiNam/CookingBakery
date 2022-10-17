using CookingBakery.Models;
using CookingBakery.Utils.BakeryRepository.Interface;
using System;
using System.Collections.Generic;

namespace CookingBakery.BakeryModules.PostDetailModule.Interface
{
    public interface IPostDetailRepository : IRepository<PostDetail>
    {
        public ICollection<PostDetail> GetPostDetailByPostId(Guid postId);
    }
}
