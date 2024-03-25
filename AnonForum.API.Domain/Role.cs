using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AnonForum.API.Domain;

public partial class Role
{
    [Key]
    [Column("RoleID")]
    public int RoleId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string RoleName { get; set; } = null!;

    [ForeignKey("RoleID")]
    [InverseProperty("Roles")]
    public ICollection<UserAuth> Users { get; set; }
}
