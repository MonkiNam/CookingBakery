using System;
using System.Collections.Generic;

#nullable disable

namespace BussinessObject.Models
{
    public partial class Post
    {
        public Post()
        {
            Comments = new HashSet<Comment>();
            PostDetails = new HashSet<PostDetail>();
            PostReactions = new HashSet<PostReaction>();
        }

        public Guid PostId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? Reaction { get; set; }
        public Guid AuthorId { get; set; }
        public bool? Status { get; set; }
        public Guid CategoryId { get; set; }

        public virtual Category Category { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<PostDetail> PostDetails { get; set; }
        public virtual ICollection<PostReaction> PostReactions { get; set; }
    }
}
