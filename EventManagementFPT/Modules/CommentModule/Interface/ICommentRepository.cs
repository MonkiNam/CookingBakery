using EventManagementFPT.Model;
using EventManagementFPT.Utils.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventManagementFPT.Modules.CommentModule.Interface
{
    public interface ICommentRepository : IRepository<Comment>
    {
        public Task RemoveAndItsChildComment(Comment comment);
        public ICollection<Comment> GetListSubComment(Guid? oriCommentID);
    }
}
