using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AnonForum.API.Domain;

[Keyless]
[Table("UnlikeComment")]
public partial class UnlikeComment
{
    [Column("UnlikeCommentID")]
    public int UnlikeCommentId { get; set; }

    [Column("CommentID")]
    public int CommentId { get; set; }

    [Column("UserID")]
    public int UserId { get; set; }

    [Column("PostID")]
    public int PostId { get; set; }

    [ForeignKey("CommentId")]
    public virtual Comment Comment { get; set; } = null!;

    [ForeignKey("PostId")]
    public virtual Post Post { get; set; } = null!;

    [ForeignKey("UserId")]
    public virtual UserAuth User { get; set; } = null!;
}
