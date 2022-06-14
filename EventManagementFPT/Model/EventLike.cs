#nullable disable

using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventManagementFPT.Model
{
    [Table("tblEventLike")]
    public partial class EventLike
    {
        public Guid EventId { get; set; }
        public Guid UserId { get; set; }
        public bool? Status { get; set; }
        public DateTime? CreateDate { get; set; }

        public virtual Event Event { get; set; }
        public virtual User User { get; set; }
    }
}
