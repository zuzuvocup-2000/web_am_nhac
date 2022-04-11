using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace webdemo.Models
{
    public partial class music_dataContext : DbContext
    {
        public music_dataContext()
        {
        }

        public music_dataContext(DbContextOptions<music_dataContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Album> Albums { get; set; } = null!;
        public virtual DbSet<Music> Musics { get; set; } = null!;
        public virtual DbSet<Router> Routers { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=localhost;user=root;password=root;database=music_data;connection timeout=20;persist security info=False;port=3307;allow user variables=True;connect timeout=120", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.28-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8_general_ci")
                .HasCharSet("utf8");

            modelBuilder.Entity<Album>(entity =>
            {
                entity.ToTable("album");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Image)
                    .HasColumnType("text")
                    .HasColumnName("image");

                entity.Property(e => e.Memberid).HasColumnName("memberid");

                entity.Property(e => e.Name)
                    .HasColumnType("text")
                    .HasColumnName("name")
                    .UseCollation("utf8mb4_0900_ai_ci")
                    .HasCharSet("utf8mb4");

                entity.Property(e => e.Publish).HasColumnName("publish");

                entity.Property(e => e.Url)
                    .HasColumnType("text")
                    .HasColumnName("url")
                    .UseCollation("utf8mb4_0900_ai_ci")
                    .HasCharSet("utf8mb4");
            });

            modelBuilder.Entity<Music>(entity =>
            {
                entity.ToTable("music");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Album).HasColumnName("album");

                entity.Property(e => e.Artist)
                    .HasColumnType("text")
                    .HasColumnName("artist")
                    .UseCollation("utf8mb4_0900_ai_ci")
                    .HasCharSet("utf8mb4");

                entity.Property(e => e.Image)
                    .HasColumnType("text")
                    .HasColumnName("image");

                entity.Property(e => e.Memberid)
                    .HasColumnType("text")
                    .HasColumnName("memberid");

                entity.Property(e => e.Music1)
                    .HasColumnType("text")
                    .HasColumnName("music")
                    .UseCollation("utf8mb4_0900_ai_ci")
                    .HasCharSet("utf8mb4");

                entity.Property(e => e.Name)
                    .HasColumnType("text")
                    .HasColumnName("name")
                    .UseCollation("utf8mb4_0900_ai_ci")
                    .HasCharSet("utf8mb4");

                entity.Property(e => e.Publish).HasColumnName("publish");

                entity.Property(e => e.Theloai)
                    .HasColumnType("text")
                    .HasColumnName("theloai");

                entity.Property(e => e.Url)
                    .HasColumnType("text")
                    .HasColumnName("url")
                    .UseCollation("utf8mb4_0900_ai_ci")
                    .HasCharSet("utf8mb4");

                entity.Property(e => e.Viewed).HasColumnName("viewed");
            });

            modelBuilder.Entity<Router>(entity =>
            {
                entity.ToTable("router");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Module)
                    .HasMaxLength(45)
                    .HasColumnName("module")
                    .UseCollation("utf8mb4_0900_ai_ci")
                    .HasCharSet("utf8mb4");

                entity.Property(e => e.Objectid).HasColumnName("objectid");

                entity.Property(e => e.Url)
                    .HasColumnType("text")
                    .HasColumnName("url")
                    .UseCollation("utf8mb4_0900_ai_ci")
                    .HasCharSet("utf8mb4");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Coin)
                    .HasColumnName("coin")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Email)
                    .HasColumnType("text")
                    .HasColumnName("email");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");

                entity.Property(e => e.Password)
                    .HasMaxLength(255)
                    .HasColumnName("password");

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasDefaultValueSql("'0'");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
