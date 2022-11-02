using BussinessObject.Models;
using System;
using System.Collections.Generic;

#nullable disable

namespace BussinessObject.Models
{
    public partial class PostReaction
    {
        public Guid PostId { get; set; }
        public Guid UserId { get; set; }
        public bool? Status { get; set; }
        public DateTime? CreatedDate { get; set; }

        public virtual Post Post { get; set; }
        public virtual User User { get; set; }
    }
}
