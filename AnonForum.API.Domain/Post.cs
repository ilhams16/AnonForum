using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AnonForum.API.Domain;

public partial class Post
{
    [Key]
    [Column("PostID")]
    public int PostId { get; set; }

    [Column("UserID")]
    public int UserId { get; set; }

    [StringLength(50)]
    public string Title { get; set; } = null!;

    [Column(TypeName = "ntext")]
    public string PostText { get; set; } = null!;

    public DateTime TimeStamp { get; set; }

    [Unicode(false)]
    public string? Image { get; set; }

    [Column("PostCategoryID")]
    public int PostCategoryId { get; set; }

    public int? TotalLikes { get; set; }

    public int? TotalDislikes { get; set; }

    [Column("CommunityID")]
    public int? CommunityId { get; set; }

    [InverseProperty("Post")]
    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    [InverseProperty("Post")]
    public virtual ICollection<LikeComment> LikeComments { get; set; } = new List<LikeComment>();

    [InverseProperty("Post")]
    public virtual ICollection<LikePost> LikePosts { get; set; } = new List<LikePost>();

    [ForeignKey("PostCategoryId")]
    [InverseProperty("Posts")]
    public virtual PostCategory PostCategory { get; set; } = null!;

    [InverseProperty("Post")]
    public virtual ICollection<UnlikePost> UnlikePosts { get; set; } = new List<UnlikePost>();

    [ForeignKey("UserId")]
    [InverseProperty("Posts")]
    public virtual UserAuth User { get; set; } = null!;
}
