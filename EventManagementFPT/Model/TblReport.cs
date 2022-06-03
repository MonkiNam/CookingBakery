using System;
using System.Collections.Generic;

#nullable disable

namespace EventManagementFPT.Model
{
    public partial class TblReport
    {
        public Guid ReportId { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public DateTime CreateDate { get; set; }
        public bool? Type { get; set; }
        public Guid? Author { get; set; }
        public Guid? EventId { get; set; }

        public virtual TblUser AuthorNavigation { get; set; }
        public virtual TblEvent Event { get; set; }
    }
}
