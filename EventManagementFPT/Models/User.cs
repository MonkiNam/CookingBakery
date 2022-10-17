using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;

#nullable disable

namespace CookingBakery.Models
{
    public partial class User
    {
        public User()
        {
            FollowInfoFollowingUsers = new HashSet<FollowInfo>();
            FollowInfoUsers = new HashSet<FollowInfo>();
            PostReactions = new HashSet<PostReaction>();
        }
        public User(string name, string email, string avatar, bool googleAuthen, RoleEnum role)
        {
            Name = name;
            Email = email;
            Avatar = avatar;
            Role = role;
            IsGoogle = googleAuthen;
        }

        public Guid UserId { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public bool? Gender { get; set; }
        public string Avatar { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool? IsBlocked { get; set; }
        [Required]
        public RoleEnum Role { get; set; }
        public bool IsGoogle { get; set; }

        public virtual ICollection<FollowInfo> FollowInfoFollowingUsers { get; set; }
        public virtual ICollection<FollowInfo> FollowInfoUsers { get; set; }
        public virtual ICollection<PostReaction> PostReactions { get; set; }
    }
}
