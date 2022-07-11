#nullable disable

using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EventManagementFPT.Model
{
    public partial class EventManagementContext : DbContext
    {
        public EventManagementContext()
        {
        }

        public EventManagementContext(DbContextOptions<EventManagementContext> options) : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<Venue> Venues { get; set; }
        public virtual DbSet<EventLike> EventLikes { get; set; }
        public virtual DbSet<Report> Reports { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserEvent> UserEvents { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json");
                var configuration = builder.Build();
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.CategoryId);

                entity.ToTable("tblCategory");

                entity.Property(e => e.CategoryId).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.HasKey(e => e.CommentId);

                entity.ToTable("tblComment");

                entity.HasIndex(e => e.EventId, "IX_tblComment_EventId");

                entity.HasIndex(e => e.ParentId, "IX_tblComment_ParentId");

                entity.HasIndex(e => e.UserId, "IX_tblComment_UserId");

                entity.Property(e => e.CommentId).ValueGeneratedNever();

                entity.Property(e => e.Content)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.HasOne(d => d.Event)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.EventId);

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.InverseParent)
                    .HasForeignKey(d => d.ParentId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<Event>(entity =>
            {
                entity.HasKey(e => e.EventId);

                entity.ToTable("tblEvent");

                entity.HasIndex(e => e.Category, "IX_tblEvent_CategoryNavigationCategoryId");

                entity.HasIndex(e => e.VenueId, "IX_tblEvent_VenueId");

                entity.Property(e => e.EventId).ValueGeneratedNever();

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.Name).IsRequired();

                entity.HasOne(d => d.CategoryNavigation)
                    .WithMany(p => p.TblEvents)
                    .HasForeignKey(d => d.Category);

                entity.HasOne(d => d.Venue)
                    .WithMany(p => p.Events)
                    .HasForeignKey(d => d.VenueId);
            });

            modelBuilder.Entity<EventLike>(entity =>
            {
                entity.HasKey(e => new { e.EventId, e.UserId });

                entity.ToTable("tblEventLike");

                entity.HasIndex(e => e.UserId, "IX_tblEventLike_UserId");

                entity.HasOne(d => d.Event)
                    .WithMany(p => p.EventLikes)
                    .HasForeignKey(d => d.EventId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.EventLikes)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<Report>(entity =>
            {
                entity.HasKey(e => e.ReportId);

                entity.ToTable("tblReport");

                entity.HasIndex(e => e.Author, "IX_tblReport_AuthorNavigationUserId");

                entity.HasIndex(e => e.EventId, "IX_tblReport_EventId");

                entity.Property(e => e.ReportId).ValueGeneratedNever();

                entity.Property(e => e.Content)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.Name).IsRequired();

                entity.HasOne(d => d.Event)
                    .WithMany(p => p.Reports)
                    .HasForeignKey(d => d.EventId);

                entity.HasOne(d => d.AuthorNavigation)
                    .WithMany(p => p.Reports)
                    .HasForeignKey(d => d.Author);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.ToTable("tblUser");

                entity.Property(e => e.UserId).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Password).HasMaxLength(30);
            });

            modelBuilder.Entity<UserEvent>(entity =>
            {
                entity.HasKey(e => new { e.EventId, e.UserId });

                entity.ToTable("tblUserEvent");

                entity.HasIndex(e => e.UserId, "IX_tblUserEvent_UserId");

                entity.HasOne(d => d.Event)
                    .WithMany(p => p.UserEvents)
                    .HasForeignKey(d => d.EventId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserEvents)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<Venue>(entity =>
            {
                entity.HasKey(e => e.VenueId);

                entity.ToTable("tblVenue");

                entity.Property(e => e.VenueId).ValueGeneratedNever();

                entity.Property(e => e.VenueName)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}