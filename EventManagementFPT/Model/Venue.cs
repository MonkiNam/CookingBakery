using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventManagementFPT.Model
{
    [Table("tblVenue")]
    public class Venue
    {
        public Venue()
        {
            Events = new HashSet<Event>();
        }

        [Required]
        public Guid VenueId { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string VenueName { get; set; }

        public bool Status { get; set; }

        public virtual ICollection<Event> Events { get; set; }
    }
}