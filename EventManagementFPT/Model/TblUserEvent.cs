using System;
using System.Collections.Generic;

#nullable disable

namespace EventManagementFPT.Model
{
    public partial class TblUserEvent
    {
        public Guid UserId { get; set; }
        public Guid EventId { get; set; }

        public virtual TblEvent Event { get; set; }
        public virtual TblUser User { get; set; }
    }
}
