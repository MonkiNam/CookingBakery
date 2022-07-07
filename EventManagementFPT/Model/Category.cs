#nullable disable

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        [Required]
        public Guid CategoryId { get; set; }
        [Required]
        [MinLength(1)]
        public string Name { get; set; }
        [Required]
        public bool? Status { get; set; }

        public virtual ICollection<Event> TblEvents { get; set; }
    }
}
