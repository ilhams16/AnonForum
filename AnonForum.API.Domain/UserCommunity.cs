using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AnonForum.API.Domain;

[Keyless]
[Table("UserCommunity")]
public partial class UserCommunity
{
    [Column("UserID")]
    public int UserId { get; set; }

    [Column("CommunityID")]
    public int CommunityId { get; set; }

    [ForeignKey("CommunityId")]
    public virtual Community Community { get; set; } = null!;

    [ForeignKey("UserId")]
    public virtual UserAuth User { get; set; } = null!;
}
