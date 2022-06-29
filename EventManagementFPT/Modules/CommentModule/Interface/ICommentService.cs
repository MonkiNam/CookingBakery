using EventManagementFPT.Model;
using System;
using System.Threading.Tasks;

namespace EventManagementFPT.Modules.CommentModule.Interface
{
    public interface ICommentService
    {
        public Task RemoveAndItsChildComment(Guid? commentId);
        public Task AddNewComment(Comment newComment);
        public Task UpdateComment(Comment commentUpdate);
    }
}
