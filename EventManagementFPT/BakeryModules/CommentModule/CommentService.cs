using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CookingBakery.BakeryModules.CommentModule.Interface;
using CookingBakery.Models;

namespace CookingBakery.BakeryModules.CommentModule
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;

        public CommentService(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public async Task RemoveAndItsChildComment(Guid? commentId)
        {
            var commentRemoved = await _commentRepository.GetFirstOrDefaultAsync(x => x.CommentId.Equals(commentId));

            if (commentRemoved == null) return; //wait for define pop up error message

            await _commentRepository.RemoveAndItsChildComment(commentRemoved);
        }

        public ICollection<Comment> GetListSubComment(Guid? oriCommentID)
        {
            return _commentRepository.GetListSubComment(oriCommentID);
        }

        public async Task<Comment> AddNewComment(Comment newComment)
        {
            await _commentRepository.AddAsync(newComment);
            return await GetCommentById(newComment.CommentId);
        }

        private async Task<Comment> GetCommentById(Guid? commentId)
        {
            return await _commentRepository
                .GetFirstOrDefaultAsync(
                    x => x.CommentId.Equals(commentId),
                    includeProperties: "User"
                );
        }

        public async Task UpdateComment(Comment commentUpdate)
        {
            await _commentRepository.UpdateAsync(commentUpdate);
        }

        public ICollection<Comment> GetListCommentByPostId(Guid? postId)
        {
            return _commentRepository.GetAll(includeProperties: "User").Where(x => x.PostId.Equals(postId)).ToList();
        }
    }
}