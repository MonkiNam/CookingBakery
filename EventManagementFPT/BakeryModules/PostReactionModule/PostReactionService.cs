using CookingBakery.BakeryModules.PostReactionModule.Interface;
using System;

namespace CookingBakery.BakeryModules.PostReactionModule
{
    public class PostReactionService : IPostReactionService
    {
        private IPostReactionRepository _postReactionRepository;
        public PostReactionService(IPostReactionRepository postReactionRepository)
        {
            _postReactionRepository = postReactionRepository;
        }
        public int CountLikeOfPost(Guid? postId)
        {
            var _post = _postReactionRepository.GetFirstOrDefaultAsync(x => x.PostId.Equals(postId)).Result;
            if (_post == null)
            {
                return 0;
            }
            return _postReactionRepository.CountLikeOfPost(_post.PostId);
        }
    }
}
