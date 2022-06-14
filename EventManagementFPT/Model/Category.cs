#nullable disable

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventManagementFPT.Model
{
    [Table("tblCategory")]
    public partial class Category
    {
        public Category()
        {
            TblEvents = new HashSet<Event>();
        }

        public Guid CategoryId { get; set; }
        public string Name { get; set; }
        public bool? Status { get; set; }

        public virtual ICollection<Event> TblEvents { get; set; }
    }
}
