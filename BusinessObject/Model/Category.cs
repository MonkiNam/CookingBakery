#nullable disable

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CookingBakery.Model
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
        [StringLength(50, MinimumLength = 2)]
        public string Name { get; set; }
        
        [Required]
        public bool Status { get; set; }

        public virtual ICollection<Event> TblEvents { get; set; }
    }
}
