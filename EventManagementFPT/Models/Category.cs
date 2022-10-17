using System;
using System.Collections.Generic;

#nullable disable

namespace CookingBakery.Models
{
    public partial class Category
    {
        public Category()
        {
            Posts = new HashSet<Post>();
        }

        public Guid CategoryId { get; set; }
        public string Name { get; set; }
        public bool? Status { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
    }
}
