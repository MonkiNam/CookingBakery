using System.Threading.Tasks;

namespace EventManagementFPT.Modules.CommentModule.Interface
{
    public interface ICommentService
    {
        public Task RemoveAndItsChildComment(string commentId);
    }
}
