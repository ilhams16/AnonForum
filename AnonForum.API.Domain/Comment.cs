using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AnonForum.API.Domain;

public partial class Comment
{
    [Key]
    [Column("CommentID")]
    public int CommentId { get; set; }

    [Column("PostID")]
    public int PostId { get; set; }

    [Column("UserID")]
    public int UserId { get; set; }

    [Column(TypeName = "ntext")]
    public string CommentText { get; set; } = null!;

    public DateTime TimeStamp { get; set; }

    public int? TotalLikes { get; set; }

    public int? TotalDislikes { get; set; }

    [InverseProperty("Comment")]
    public virtual ICollection<LikeComment> LikeComments { get; set; } = new List<LikeComment>();

    [ForeignKey("PostId")]
    [InverseProperty("Comments")]
    public virtual Post Post { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("Comments")]
    public virtual UserAuth User { get; set; } = null!;
}
