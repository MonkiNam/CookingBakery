using CookingBakery.Models;
using System;
using System.Threading.Tasks;

namespace CookingBakery.BakeryModules.CommentModule.Interface
{
    public interface ICommentService
    {
        public Task RemoveAndItsChildComment(Guid? commentId);
        public Task<Comment> AddNewComment(Comment newComment);
        public Task UpdateComment(Comment commentUpdate);
    }
}
