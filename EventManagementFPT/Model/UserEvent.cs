#nullable disable

using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CookingBakery.Model
{
    [Table("tblUserEvent")]
    public partial class UserEvent
    {
        public Guid UserId { get; set; }

        public Guid EventId { get; set; }

        public bool IsHost { get; set; }

        public virtual Event Event { get; set; }
        public virtual User User { get; set; }
    }
}