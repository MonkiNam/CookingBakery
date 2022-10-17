using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace CookingBakery.Models
{
    public partial class CookingBakeryContext : DbContext
    {
        public CookingBakeryContext()
        {
        }

        public CookingBakeryContext(DbContextOptions<CookingBakeryContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<FollowInfo> FollowInfos { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<PostDetail> PostDetails { get; set; }
        public virtual DbSet<PostReaction> PostReactions { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=MSI\\MONKINAM;Database=CookingBakery;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");

                entity.Property(e => e.CategoryId)
                    .ValueGeneratedNever()
                    .HasColumnName("categoryId");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.Status).HasColumnName("status");
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.ToTable("Comment");

                entity.Property(e => e.CommentId)
                    .ValueGeneratedNever()
                    .HasColumnName("commentId");

                entity.Property(e => e.AuthorId).HasColumnName("authorId");

                entity.Property(e => e.Content)
                    .HasMaxLength(50)
                    .HasColumnName("content");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("date")
                    .HasColumnName("createdDate");

                entity.Property(e => e.ParentId).HasColumnName("parentId");

                entity.Property(e => e.PostId).HasColumnName("postId");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.InverseParent)
                    .HasForeignKey(d => d.ParentId)
                    .HasConstraintName("FK_Comment_Comment");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.PostId)
                    .HasConstraintName("FK_Comment_Post");
            });

            modelBuilder.Entity<FollowInfo>(entity =>
            {
                entity.ToTable("FollowInfo");

                entity.Property(e => e.FollowInfoId)
                    .ValueGeneratedNever()
                    .HasColumnName("followInfoId");

                entity.Property(e => e.FollowingUserId).HasColumnName("followingUserId");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.HasOne(d => d.FollowingUser)
                    .WithMany(p => p.FollowInfoFollowingUsers)
                    .HasForeignKey(d => d.FollowingUserId)
                    .HasConstraintName("FK_FollowInfo_User1");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.FollowInfoUsers)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_FollowInfo_User");
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.ToTable("Post");

                entity.Property(e => e.PostId)
                    .ValueGeneratedNever()
                    .HasColumnName("postId");

                entity.Property(e => e.AuthorId).HasColumnName("authorId");

                entity.Property(e => e.CategoryId).HasColumnName("categoryId");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("date")
                    .HasColumnName("createdDate");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description");

                entity.Property(e => e.ImageUrl).HasColumnName("imageURL");

                entity.Property(e => e.Reaction).HasColumnName("reaction");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("title");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("date")
                    .HasColumnName("updatedDate");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Post_Category");
            });

            modelBuilder.Entity<PostDetail>(entity =>
            {
                entity.HasKey(e => new { e.PostId, e.ProductId });

                entity.ToTable("PostDetail");

                entity.Property(e => e.PostId).HasColumnName("postId");

                entity.Property(e => e.ProductId).HasColumnName("productId");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.PostDetails)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PostDetail_Post");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.PostDetails)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PostDetail_Product");
            });

            modelBuilder.Entity<PostReaction>(entity =>
            {
                entity.HasKey(e => new { e.PostId, e.UserId });

                entity.ToTable("PostReaction");

                entity.Property(e => e.PostId).HasColumnName("postId");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.Property(e => e.CreatedDate).HasColumnName("createdDate");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.PostReactions)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PostReaction_Post");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.PostReactions)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PostReaction_User");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");

                entity.Property(e => e.ProductId)
                    .ValueGeneratedNever()
                    .HasColumnName("productId");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("date")
                    .HasColumnName("createdDate");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.Picture).HasColumnName("picture");

                entity.Property(e => e.Price)
                    .HasColumnType("money")
                    .HasColumnName("price");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("date")
                    .HasColumnName("updatedDate");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.UserId)
                    .ValueGeneratedNever()
                    .HasColumnName("userId");

                entity.Property(e => e.Avatar).HasColumnName("avatar");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("date")
                    .HasColumnName("createdDate");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .HasColumnName("email");

                entity.Property(e => e.Gender).HasColumnName("gender");

                entity.Property(e => e.IsBlocked).HasColumnName("isBlocked");

                entity.Property(e => e.IsGoogle).HasColumnName("isGoogle");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .HasColumnName("password");

                entity.Property(e => e.Phone)
                    .HasMaxLength(50)
                    .HasColumnName("phone");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("date")
                    .HasColumnName("updatedDate");

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .HasColumnName("username");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
