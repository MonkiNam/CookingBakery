#nullable disable

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventManagementFPT.Model
{
    [Table("tblEvent")]
    public partial class Event
    {
        public Event()
        {
            TblComments = new HashSet<Comment>();
            TblEventLikes = new HashSet<EventLike>();
            TblFollowEvents = new HashSet<FollowEvent>();
            TblReports = new HashSet<Report>();
            TblUserEvents = new HashSet<UserEvent>();
        }

        public Guid EventId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Venue { get; set; }
        public bool? Status { get; set; }
        public bool? CanComment { get; set; }
        public Guid? Category { get; set; }
        public int Capacity { get; set; }

        public virtual Category CategoryNavigation { get; set; }
        public virtual ICollection<Comment> TblComments { get; set; }
        public virtual ICollection<EventLike> TblEventLikes { get; set; }
        public virtual ICollection<FollowEvent> TblFollowEvents { get; set; }
        public virtual ICollection<Report> TblReports { get; set; }
        public virtual ICollection<UserEvent> TblUserEvents { get; set; }
    }
}
