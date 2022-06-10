using EventManagementFPT.Model;
using EventManagementFPT.Modules.Comment.Interface;

namespace EventManagementFPT.Modules.Comment
{
    public class CommentService : ICommentService
    {
        ICommentRepository commentRepository;
        public CommentService(ICommentRepository _commentRepository)
        {
            commentRepository = _commentRepository;
        }
        public void RemoveAndItsChildComment(string commentID)
        {
            TblComment commentRemoved = commentRepository.GetByID(commentID);
            if (commentRemoved == null)
            {
                return; //wait for define pop up error message
            }
            commentRepository.RemoveAndItsChildComment(commentRemoved);
            return; // wait for define pop up success message
        }
    }
}
