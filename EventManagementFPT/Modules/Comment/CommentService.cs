using System.Threading.Tasks;
using EventManagementFPT.Modules.Comment.Interface;

namespace EventManagementFPT.Modules.Comment
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;

        public CommentService(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public async Task RemoveAndItsChildComment(string commentId)
        {
            var commentRemoved = await _commentRepository.GetById(commentId);
            
            if (commentRemoved == null) return; //wait for define pop up error message

            _commentRepository.RemoveAndItsChildComment(commentRemoved);
        }
    }
}