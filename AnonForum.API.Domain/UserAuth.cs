using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AnonForum.API.Domain;

[Table("UserAuth")]
[Index("UserId", Name = "UQ_UserAuth", IsUnique = true)]
public partial class UserAuth
{
    [Key]
    [Column("UserID")]
    public int UserId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Username { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string Email { get; set; } = null!;

    [StringLength(60)]
    [Unicode(false)]
    public string Password { get; set; } = null!;

    [StringLength(50)]
    public string Nickname { get; set; } = null!;

    [Unicode(false)]
    public string? UserImage { get; set; }

    [InverseProperty("User")]
    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    [InverseProperty("User")]
    public virtual ICollection<LikeComment> LikeComments { get; set; } = new List<LikeComment>();

    [InverseProperty("User")]
    public virtual ICollection<LikePost> LikePosts { get; set; } = new List<LikePost>();

    [InverseProperty("User")]
    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();

    [InverseProperty("User")]
    public virtual ICollection<UnlikePost> UnlikePosts { get; set; } = new List<UnlikePost>();

    [ForeignKey("UserID")]
    [InverseProperty("Users")]
    public virtual ICollection<Role> Roles { get; set; } = new List<Role>();
}
