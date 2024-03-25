using System;
using System.Collections.Generic;
using AnonForum.API.Domain;
using Microsoft.EntityFrameworkCore;

namespace AnonForum.API.Data;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<Community> Communities { get; set; }

    public virtual DbSet<LikeComment> LikeComments { get; set; }

    public virtual DbSet<LikePost> LikePosts { get; set; }

    public virtual DbSet<Post> Posts { get; set; }

    public virtual DbSet<PostCategory> PostCategories { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<UnlikeComment> UnlikeComments { get; set; }

    public virtual DbSet<UnlikePost> UnlikePosts { get; set; }

    public virtual DbSet<UserAuth> UserAuths { get; set; }

    public virtual DbSet<UserCommunity> UserCommunities { get; set; }

    //public virtual DbSet<UsersRoles> UsersRoles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=BSINB23L005\\BSISQLEXPRESS;Initial Catalog=AnonForum;Integrated Security=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Comment>(entity =>
        {
            entity.Property(e => e.TimeStamp).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.TotalDislikes).HasComputedColumnSql("([dbo].[GetUnlikeComment]([PostID],[CommentID]))", false);
            entity.Property(e => e.TotalLikes).HasComputedColumnSql("([dbo].[GetLikeComment]([PostID],[CommentID]))", false);

            entity.HasOne(d => d.Post).WithMany(p => p.Comments)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Comments_Posts");

            entity.HasOne(d => d.User).WithMany(p => p.Comments)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Comments_UserAuth");
        });

        modelBuilder.Entity<LikeComment>(entity =>
        {
            entity.HasOne(d => d.Comment).WithMany(p => p.LikeComments)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LikeComment_Comments");

            entity.HasOne(d => d.Post).WithMany(p => p.LikeComments)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LikeComment_Posts");

            entity.HasOne(d => d.User).WithMany(p => p.LikeComments)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LikeComment_UserAuth");
        });

        modelBuilder.Entity<LikePost>(entity =>
        {
            entity.HasOne(d => d.Post).WithMany(p => p.LikePosts)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LikePost_Posts");

            entity.HasOne(d => d.User).WithMany(p => p.LikePosts)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LikePost_UserAuth");
        });

        modelBuilder.Entity<Post>(entity =>
        {
            entity.Property(e => e.TimeStamp).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.TotalDislikes).HasComputedColumnSql("([dbo].[GetUnlikePost]([PostID]))", false);
            entity.Property(e => e.TotalLikes).HasComputedColumnSql("([dbo].[GetLikePost]([PostID]))", false);

            entity.HasOne(d => d.PostCategory).WithMany(p => p.Posts)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Posts_PostCategory");

            entity.HasOne(d => d.User).WithMany(p => p.Posts)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Posts_UserAuth");
        });

        modelBuilder.Entity<PostCategory>(entity =>
        {
            entity.Property(e => e.Name).IsFixedLength();

            modelBuilder.Entity<UnlikeComment>(entity =>
            {
                entity.Property(e => e.UnlikeCommentId).ValueGeneratedOnAdd();

                entity.HasOne(d => d.Comment).WithMany()
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UnlikeComment_Comments");

                entity.HasOne(d => d.Post).WithMany()
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UnlikeComment_Posts");

                entity.HasOne(d => d.User).WithMany()
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UnlikeComment_UserAuth");
            });

            modelBuilder.Entity<UnlikePost>(entity =>
            {
                entity.HasOne(d => d.Post).WithMany(p => p.UnlikePosts)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UnlikePost_Posts");

                entity.HasOne(d => d.User).WithMany(p => p.UnlikePosts)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UnlikePost_UserAuth");
            });

            modelBuilder.Entity<UserAuth>(entity =>
            {
                entity.Property(e => e.Password).IsFixedLength();
            });

            modelBuilder.Entity<UserCommunity>(entity =>
            {
                entity.HasOne(d => d.Community).WithMany()
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserCommunity_Community");

                entity.HasOne(d => d.User).WithMany()
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserCommunity_UserAuth");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasMany(d => d.Users).WithMany(p => p.Roles)
                    .UsingEntity<Dictionary<string, object>>(
                        "UsersRoles",
                        r => r.HasOne<UserAuth>().WithMany()
                            .HasForeignKey("UserID")
                            .OnDelete(DeleteBehavior.ClientSetNull)
                            .HasConstraintName("FK_UsersRoles_Users"),
                        l => l.HasOne<Role>().WithMany()
                            .HasForeignKey("RoleId")
                            .OnDelete(DeleteBehavior.ClientSetNull)
                            .HasConstraintName("FK_UsersRoles_Roles"),
                        j =>
                        {
                            j.HasKey("RoleID", "UserID");
                            j.ToTable("UsersRoles");
                            j.IndexerProperty<int>("RoleID").HasColumnName("RoleID");
                            j.IndexerProperty<int>("UserID").HasMaxLength(50);
                        });

            //    modelBuilder.Entity<UsersRole>(entity =>
            //{
            //    entity.HasOne(d => d.Roles).WithMany()
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_UsersRoles_Roles");

            //    entity.HasOne(d => d.Users).WithMany()
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_UsersRoles_UserAuth");
            //});

                OnModelCreatingPartial(modelBuilder);
            });
        }); }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
