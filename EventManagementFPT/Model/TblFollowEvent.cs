using System;
using System.Collections.Generic;

#nullable disable

namespace EventManagementFPT.Model
{
    public partial class TblFollowEvent
    {
        public Guid EventId { get; set; }
        public Guid UserId { get; set; }
        public bool? Status { get; set; }

        public virtual TblEvent Event { get; set; }
        public virtual TblUser User { get; set; }
    }
}
