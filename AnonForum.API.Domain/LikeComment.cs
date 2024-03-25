using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AnonForum.API.Domain;

[Table("LikeComment")]
public partial class LikeComment
{
    [Key]
    [Column("LikeCommentID")]
    public int LikeCommentId { get; set; }

    [Column("CommentID")]
    public int CommentId { get; set; }

    [Column("UserID")]
    public int UserId { get; set; }

    [Column("PostID")]
    public int PostId { get; set; }

    [ForeignKey("CommentId")]
    [InverseProperty("LikeComments")]
    public virtual Comment Comment { get; set; } = null!;

    [ForeignKey("PostId")]
    [InverseProperty("LikeComments")]
    public virtual Post Post { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("LikeComments")]
    public virtual UserAuth User { get; set; } = null!;
}
