using BussinessObject.Models;
using System;
using System.Collections.Generic;

#nullable disable

namespace BussinessObject.Models
{
    public partial class Product
    {
        public Product()
        {
            PostDetails = new HashSet<PostDetail>();
        }

        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public string Picture { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool? Status { get; set; }
        public decimal Price { get; set; }

        public virtual ICollection<PostDetail> PostDetails { get; set; }
    }
}
