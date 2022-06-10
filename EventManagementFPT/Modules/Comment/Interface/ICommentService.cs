using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventManagementFPT.Modules.Comment.Interface
{
    public interface ICommentService
    {
        public void RemoveAndItsChildComment(string commentID);
    }
}
