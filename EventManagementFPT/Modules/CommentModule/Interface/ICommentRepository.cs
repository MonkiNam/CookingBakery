using EventManagementFPT.Model;
using EventManagementFPT.Utils.Repository.Interface;

namespace EventManagementFPT.Modules.CommentModule.Interface
{
    public interface ICommentRepository : IRepository<Comment>
    {
        public void RemoveAndItsChildComment(Comment comment);
    }
}
