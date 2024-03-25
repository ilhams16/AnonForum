using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AnonForum.API.Domain;

[Table("UnlikePost")]
public partial class UnlikePost
{
    [Key]
    [Column("UnlikePostID")]
    public int UnlikePostId { get; set; }

    [Column("PostID")]
    public int PostId { get; set; }

    [Column("UserID")]
    public int UserId { get; set; }

    [ForeignKey("PostId")]
    [InverseProperty("UnlikePosts")]
    public virtual Post Post { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("UnlikePosts")]
    public virtual UserAuth User { get; set; } = null!;
}
