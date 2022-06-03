using EventManagementFPT.Model;
using EventManagementFPT.Utils.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventManagementFPT.Modules.Comment.Interface
{
    public interface ICommentRepository : IRepository<TblComment>
    {
        public void RemoveAndItsChildComment(TblComment comment);
    }
}
