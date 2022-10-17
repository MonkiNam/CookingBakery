using CookingBakery.Models;
using CookingBakery.Utils.BakeryRepository.Interface;
using System;

namespace CookingBakery.BakeryModules.PostReactionModule.Interface
{
    public interface IPostReactionRepository : IRepository<PostReaction>
    {
        public int CountLikeOfPost(Guid? postId);
    }
}
