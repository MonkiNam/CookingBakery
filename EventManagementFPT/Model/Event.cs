#nullable disable

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventManagementFPT.Model
{
    [Table("tblEvent")]
    public partial class Event
    {
        public Event()
        {
            Comments = new HashSet<Comment>();
            EventLikes = new HashSet<EventLike>();
            Reports = new HashSet<Report>();
            UserEvents = new HashSet<UserEvent>();
        }
        [Required]
        public Guid EventId { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        [Required]
        [StringLength(500, MinimumLength = 10)]
        public string Description { get; set; }
        
        [MaxLength]
        [Column(TypeName = "nvarchar(MAX)")]
        public string ImageUrl { get; set; }
        
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime CreateDate { get; set; }
        
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime StartDate { get; set; }
        
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime EndDate { get; set; }
        
        [Required]
        public Guid VenueId { get; set; }
        
        public bool Status { get; set; }
        
        public bool CanComment { get; set; }
        
        [Required]
        [Column("CategoryId")]
        public Guid? Category { get; set; }
        
        [Required]
        [Range(5, int.MaxValue, ErrorMessage = "At least 5 people to create event")]
        public int Capacity { get; set; }

        public virtual Category CategoryNavigation { get; set; }
        public virtual Venue Venue { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<EventLike> EventLikes { get; set; }
        public virtual ICollection<Report> Reports { get; set; }
        public virtual ICollection<UserEvent> UserEvents { get; set; }
    }
}