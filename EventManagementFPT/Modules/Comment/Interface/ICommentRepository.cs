using EventManagementFPT.Utils.Repository.Interface;

namespace EventManagementFPT.Modules.Comment.Interface
{
    public interface ICommentRepository : IRepository<Model.Comment>
    {
        public void RemoveAndItsChildComment(Model.Comment comment);
    }
}
