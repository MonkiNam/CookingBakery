using System;
using System.Collections.Generic;

#nullable disable

namespace EventManagementFPT.Model
{
    public partial class TblComment
    {
        public TblComment()
        {
            InverseParent = new HashSet<TblComment>();
        }

        public Guid CommentId { get; set; }
        public Guid UserId { get; set; }
        public DateTime CreateDate { get; set; }
        public string Content { get; set; }
        public bool? Status { get; set; }
        public bool? IsParent { get; set; }
        public Guid? ParentId { get; set; }
        public Guid EventId { get; set; }

        public virtual TblEvent Event { get; set; }
        public virtual TblComment Parent { get; set; }
        public virtual TblUser User { get; set; }
        public virtual ICollection<TblComment> InverseParent { get; set; }
    }
}
