#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventManagementFPT.Model
{
    [Table("tblReport")]
    public partial class Report
    {
        [Required]
        public Guid ReportId { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        [Required]
        [StringLength(500, MinimumLength = 2)]
        public string Content { get; set; }
        
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime CreateDate { get; set; }
        
        [Required]
        public bool? Type { get; set; }
        
        [Required]
        [Column("UserId")]
        public Guid? Author { get; set; }
        
        [Required]
        public Guid? EventId { get; set; }

        public virtual User AuthorNavigation { get; set; }
        public virtual Event Event { get; set; }
    }
}
