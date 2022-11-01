using System;
using System.Collections.Generic;

#nullable disable

namespace CookingBakery.Models
{
    public partial class Comment
    {
        public Comment()
        {
            InverseParent = new HashSet<Comment>();
        }

        public Guid CommentId { get; set; }
        public Guid? UserId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string Content { get; set; }
        public bool? Status { get; set; }
        public Guid? ParentId { get; set; }
        public Guid? PostId { get; set; }

        public virtual User Author { get; set; }
        public virtual Comment Parent { get; set; }
        public virtual Post Post { get; set; }
        public virtual ICollection<Comment> InverseParent { get; set; }
    }
}
