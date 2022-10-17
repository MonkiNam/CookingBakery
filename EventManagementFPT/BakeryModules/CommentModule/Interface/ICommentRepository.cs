using CookingBakery.Models;
using CookingBakery.Utils.BakeryRepository.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CookingBakery.BakeryModules.CommentModule.Interface
{
    public interface ICommentRepository : IRepository<Comment>
    {
        public Task RemoveAndItsChildComment(Comment comment);
        public ICollection<Comment> GetListSubComment(Guid? oriCommentID);
    }
}
