using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CookingBakery.BakeryModules.CommentModule.Interface;
using CookingBakery.Models;
using CookingBakery.Utils.BakeryRepository;
using Microsoft.EntityFrameworkCore;

namespace CookingBakery.BakeryModules.CommentModule
{
    public class CommentRepository : Repository<Comment>, ICommentRepository
    {
        private readonly CookingBakeryContext _db;

        public CommentRepository(CookingBakeryContext db) : base(db)
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