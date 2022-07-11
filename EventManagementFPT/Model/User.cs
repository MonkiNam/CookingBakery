#nullable disable

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventManagementFPT.Model
{
    [Table("tblUser")]
    public partial class User
    {

        public User()
        {
            TblComments = new HashSet<Comment>();
            TblEventLikes = new HashSet<EventLike>();
            TblReports = new HashSet<Report>();
            TblUserEvents = new HashSet<UserEvent>();
        }

        public User(string name, string email, string avatar, bool googleAuthen, RoleEnum role)
        {
            Name = name;
            Email = email;
            Avatar = avatar;
            Role = role;
            IsGoogleAuthenticate = googleAuthen;
        }
        [Required]
        public Guid UserId { get; set; }
        
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [StringLength(30, MinimumLength = 5)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Entered phone format is not valid.")]
        public string PhoneNumber { get; set; }

        [Column(TypeName = "nvarchar(MAX)")]
        public string Avatar { get; set; }
        
        public bool? IsBlocked { get; set; }
        
        [Required]
        public RoleEnum Role { get; set; }
        
        public bool IsGoogleAuthenticate { get; set; }

        public virtual ICollection<Comment> TblComments { get; set; }
        public virtual ICollection<EventLike> TblEventLikes { get; set; }
        public virtual ICollection<Report> TblReports { get; set; }
        public virtual ICollection<UserEvent> TblUserEvents { get; set; }
    }
}
