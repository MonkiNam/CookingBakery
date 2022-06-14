using System.Threading.Tasks;

namespace EventManagementFPT.Modules.Comment.Interface
{
    public interface ICommentService
    {
        public Task RemoveAndItsChildComment(string commentId);
    }
}
