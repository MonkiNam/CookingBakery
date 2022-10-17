using System;

namespace CookingBakery.BakeryModules.PostReactionModule.Interface
{
    public interface IPostReactionService
    {
        public int CountLikeOfPost(Guid? postId);
    }
}
