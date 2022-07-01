#nullable disable

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventManagementFPT.Model
{
    [Table("tblUser")]
    public partial class User
    {
        public User()
        {
            TblComments = new HashSet<Comment>();
            TblEventLikes = new HashSet<EventLike>();
            TblFollowEvents = new HashSet<FollowEvent>();
            TblReports = new HashSet<Report>();
            TblUserEvents = new HashSet<UserEvent>();
        }

        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Avatar { get; set; }
        public bool? IsBlocked { get; set; }
        public RoleEnum Role { get; set; }
        public bool IsGoogleAuthenticate { get; set; }

        public virtual ICollection<Comment> TblComments { get; set; }
        public virtual ICollection<EventLike> TblEventLikes { get; set; }
        public virtual ICollection<FollowEvent> TblFollowEvents { get; set; }
        public virtual ICollection<Report> TblReports { get; set; }
        public virtual ICollection<UserEvent> TblUserEvents { get; set; }
    }
}
