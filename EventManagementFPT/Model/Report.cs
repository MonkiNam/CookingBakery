#nullable disable

using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventManagementFPT.Model
{
    [Table("tblReport")]
    public partial class Report
    {
        public Guid ReportId { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public DateTime CreateDate { get; set; }
        public bool? Type { get; set; }
        public Guid? Author { get; set; }
        public Guid? EventId { get; set; }

        public virtual User AuthorNavigation { get; set; }
        public virtual Event Event { get; set; }
    }
}
