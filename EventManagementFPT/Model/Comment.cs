#nullable disable

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        [Required]
        public Guid CommentId { get; set; }
        [Required]
        public Guid UserId { get; set; }
        
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime CreateDate { get; set; }
        
        [Required]
        [StringLength(500, MinimumLength = 1)]
        public string Content { get; set; }
        
        public bool? Status { get; set; }
        
        public bool IsParent { get; set; }
        
        public Guid? ParentId { get; set; }
        
        [Required]
        public Guid EventId { get; set; }

        public virtual Event Event { get; set; }
        public virtual Comment Parent { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Comment> InverseParent { get; set; }
    }
}
