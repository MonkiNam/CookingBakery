using System;
using System.Collections.Generic;

#nullable disable

namespace EventManagementFPT.Model
{
    public partial class TblCategory
    {
        public TblCategory()
        {
            TblEvents = new HashSet<TblEvent>();
        }

        public Guid CategoryId { get; set; }
        public string Name { get; set; }
        public bool? Status { get; set; }

        public virtual ICollection<TblEvent> TblEvents { get; set; }
    }
}
