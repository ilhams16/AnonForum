using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AnonForum.API.Domain;

[Table("Community")]
public partial class Community
{
    [Key]
    [Column("CommunityID")]
    public int CommunityId { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string CommunityName { get; set; } = null!;
}
