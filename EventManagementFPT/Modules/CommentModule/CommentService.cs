using System.Threading.Tasks;
using EventManagementFPT.Modules.CommentModule.Interface;

namespace EventManagementFPT.Modules.CommentModule
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
            var commentRemoved = await _commentRepository.GetByIdAsync(commentId);

            if (commentRemoved == null) return; //wait for define pop up error message

            _commentRepository.RemoveAndItsChildComment(commentRemoved);
        }
    }
}