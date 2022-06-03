using EventManagementFPT.Model;
using EventManagementFPT.Modules.Comment.Interface;
using EventManagementFPT.Utils.Repository;
using System.Collections.Generic;
using System.Linq;

namespace EventManagementFPT.Modules.Comment
{
    public class CommentRepository : Repository<TblComment>, ICommentRepository
    {
        private readonly EventManagementContext Db;
        public CommentRepository(EventManagementContext db) : base(db)
        {
            this.Db = db;
        }
        public void RemoveAndItsChildComment(TblComment comment)
        {
            List<TblComment> comments = this.Db.TblComments.Where(item => item.ParentId == comment.CommentId).ToList();            
            if (comments.Count > 0)
            {
                this.Db.TblComments.RemoveRange(comments);
            }
            this.Db.TblComments.Remove(comment);
            this.Db.SaveChanges();
        }
    }
}
