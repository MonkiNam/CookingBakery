using System;
using System.Collections.Generic;

#nullable disable

namespace BussinessObject.Models
{
    public partial class PostDetail
    {
        public Guid PostId { get; set; }
        public Guid ProductId { get; set; }
        public int? Quantity { get; set; }

        public virtual Post Post { get; set; }
        public virtual Product Product { get; set; }
    }
}
