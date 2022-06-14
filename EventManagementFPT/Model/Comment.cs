#nullable disable

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventManagementFPT.Model
{
    [Table("tblComment")]
    public partial class Comment
    {
        public Comment()
        {
            InverseParent = new HashSet<Comment>();
        }

        public Guid CommentId { get; set; }
        public Guid UserId { get; set; }
        public DateTime CreateDate { get; set; }
        public string Content { get; set; }
        public bool? Status { get; set; }
        public bool? IsParent { get; set; }
        public Guid? ParentId { get; set; }
        public Guid EventId { get; set; }

        public virtual Event Event { get; set; }
        public virtual Comment Parent { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Comment> InverseParent { get; set; }
    }
}
