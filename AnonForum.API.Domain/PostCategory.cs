using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AnonForum.API.Domain;

[Table("PostCategory")]
public partial class PostCategory
{
    [Key]
    [Column("PostCategoryID")]
    public int PostCategoryId { get; set; }

    [StringLength(10)]
    public string Name { get; set; } = null!;

    [InverseProperty("PostCategory")]
    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();
}
