using System;
using System.Collections.Generic;

#nullable disable

namespace EventManagementFPT.Model
{
    public partial class TblEvent
    {
        public TblEvent()
        {
            TblComments = new HashSet<TblComment>();
            TblEventLikes = new HashSet<TblEventLike>();
            TblFollowEvents = new HashSet<TblFollowEvent>();
            TblReports = new HashSet<TblReport>();
            TblUserEvents = new HashSet<TblUserEvent>();
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

        public virtual TblCategory CategoryNavigation { get; set; }
        public virtual ICollection<TblComment> TblComments { get; set; }
        public virtual ICollection<TblEventLike> TblEventLikes { get; set; }
        public virtual ICollection<TblFollowEvent> TblFollowEvents { get; set; }
        public virtual ICollection<TblReport> TblReports { get; set; }
        public virtual ICollection<TblUserEvent> TblUserEvents { get; set; }
    }
}
