using BussinessObject.Models;
using Repositories.Utils.BakeryRepository.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repositories.BakeryModules.CommentModule.Interface
{
    public interface ICommentRepository : IRepository<Comment>
    {
        public Task RemoveAndItsChildComment(Comment comment);
        public ICollection<Comment> GetListSubComment(Guid? oriCommentID);
    }
}
