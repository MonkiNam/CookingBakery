#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventManagementFPT.Model
{
    [Table("tblEventLike")]
    public partial class EventLike
    {
        [Required]
        public Guid EventId { get; set; }
        
        [Required]
        public Guid UserId { get; set; }
        
        [Required]
        public bool? Status { get; set; }
        
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime? CreateDate { get; set; }

        public virtual Event Event { get; set; }
        public virtual User User { get; set; }
    }
}
