using BussinessObject.Models;
using Repositories.Utils.BakeryRepository.Interface;
using System;

namespace Repositories.BakeryModules.PostReactionModule.Interface
{
    public interface IPostReactionRepository : IRepository<PostReaction>
    {
        public int CountLikeOfPost(Guid? postId);
    }
}
