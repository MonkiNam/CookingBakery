using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventManagementFPT.Model;
using EventManagementFPT.Modules.CommentModule.Interface;
using EventManagementFPT.Utils.Repository;
using Microsoft.EntityFrameworkCore;

namespace EventManagementFPT.Modules.CommentModule
{
    public class CommentRepository : Repository<Comment>, ICommentRepository
    {
        private readonly EventManagementContext _db;

        public CommentRepository(EventManagementContext db) : base(db)
        {
            _db = db;
        }

        public async Task RemoveAndItsChildComment(Comment comment)
        {
            var comments = await _db.Comments
                .Where(item => item.ParentId == comment.CommentId)
                .ToListAsync();

            if (comments.Count > 0)
            {
                _db.Comments.RemoveRange(comments);
            }

            _db.Comments.Remove(comment);
            await _db.SaveChangesAsync();
        }
        public ICollection<Comment> GetListSubComment(Guid? oriCommentID)
        {
            return _db.Comments.Where(item => item.ParentId.Equals(oriCommentID)).ToList();
        }
    }
}