using System;
using System.Collections.Generic;

#nullable disable

namespace EventManagementFPT.Model
{
    public partial class TblUser
    {
        public TblUser()
        {
            TblComments = new HashSet<TblComment>();
            TblEventLikes = new HashSet<TblEventLike>();
            TblFollowEvents = new HashSet<TblFollowEvent>();
            TblReports = new HashSet<TblReport>();
            TblUserEvents = new HashSet<TblUserEvent>();
        }

        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Avatar { get; set; }
        public bool? IsBlocked { get; set; }

        public virtual ICollection<TblComment> TblComments { get; set; }
        public virtual ICollection<TblEventLike> TblEventLikes { get; set; }
        public virtual ICollection<TblFollowEvent> TblFollowEvents { get; set; }
        public virtual ICollection<TblReport> TblReports { get; set; }
        public virtual ICollection<TblUserEvent> TblUserEvents { get; set; }
    }
}
