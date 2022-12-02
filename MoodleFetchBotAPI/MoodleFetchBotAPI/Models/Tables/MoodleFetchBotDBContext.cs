using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MoodleFetchBotAPI.Models.Tables
{
    public partial class MoodleFetchBotDBContext : DbContext
    {
        public MoodleFetchBotDBContext()
        {
        }

        public MoodleFetchBotDBContext(DbContextOptions<MoodleFetchBotDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<EfmigrationsHistory> EfmigrationsHistories { get; set; } = null!;
        public virtual DbSet<UserTable> UserTables { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql($"datasource={Environment.GetEnvironmentVariable("MYSQL_DATABASE_IP") ?? "localhost"};port=3306;database=MoodleFetchBotDB;username=root;password={Environment.GetEnvironmentVariable("DBPassword")}", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.31-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_0900_ai_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<EfmigrationsHistory>(entity =>
            {
                entity.HasKey(e => e.MigrationId)
                    .HasName("PRIMARY");

                entity.ToTable("__EFMigrationsHistory");

                entity.Property(e => e.MigrationId).HasMaxLength(150);

                entity.Property(e => e.ProductVersion).HasMaxLength(32);
            });

            modelBuilder.Entity<UserTable>(entity =>
            {
                entity.ToTable("UserTable");

                entity.Property(e => e.DiscordId).HasMaxLength(30);

                entity.Property(e => e.MoodleDomain).HasColumnType("text");

                entity.Property(e => e.MoodleToken).HasColumnType("text");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
