using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AnonForum.API.Domain;

[Table("LikePost")]
public partial class LikePost
{
    [Key]
    [Column("LikePostID")]
    public int LikePostId { get; set; }

    [Column("PostID")]
    public int PostId { get; set; }

    [Column("UserID")]
    public int UserId { get; set; }

    [ForeignKey("PostId")]
    [InverseProperty("LikePosts")]
    public virtual Post Post { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("LikePosts")]
    public virtual UserAuth User { get; set; } = null!;
}
