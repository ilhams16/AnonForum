using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AnonForum.API.Domain;

[Keyless]
public partial class UsersRoles
{
    [Column("UserID")]
    public int UserId { get; set; }

    [Column("RoleID")]
    public int RoleId { get; set; }

    [ForeignKey("RoleId")]
    public virtual Role Roles { get; set; } = null!;

    [ForeignKey("UserId")]
    public virtual UserAuth Users { get; set; } = null!;
}
