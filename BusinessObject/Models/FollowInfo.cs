
using System;
using System.Collections.Generic;

#nullable disable

namespace BussinessObject.Models
{
    public partial class FollowInfo
    {
        public Guid FollowInfoId { get; set; }
        public Guid? UserId { get; set; }
        public Guid? FollowingUserId { get; set; }

        public virtual User FollowingUser { get; set; }
        public virtual User User { get; set; }
    }
}
