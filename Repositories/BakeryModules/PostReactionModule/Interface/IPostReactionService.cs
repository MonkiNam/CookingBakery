using System;

namespace Repositories.BakeryModules.PostReactionModule.Interface
{
    public interface IPostReactionService
    {
        public int CountLikeOfPost(Guid? postId);
    }
}
