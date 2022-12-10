﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MoodleFetchBotAPI.Models.Tables;

#nullable disable

namespace MoodleFetchBotAPI.Migrations
{
    [DbContext(typeof(MoodleFetchBotDBContext))]
    partial class MoodleFetchBotDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseCollation("utf8mb4_0900_ai_ci")
                .HasAnnotation("ProductVersion", "6.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.HasCharSet(modelBuilder, "utf8mb4");

            modelBuilder.Entity("MoodleFetchBotAPI.Models.Tables.EfmigrationsHistory", b =>
                {
                    b.Property<string>("MigrationId")
                        .HasMaxLength(150)
                        .HasColumnType("varchar(150)");

                    b.Property<string>("ProductVersion")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("varchar(32)");

                    b.HasKey("MigrationId")
                        .HasName("PRIMARY");

                    b.ToTable("__EFMigrationsHistory", (string)null);
                });

            modelBuilder.Entity("MoodleFetchBotAPI.Models.Tables.ServerList", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.Property<string>("GuildId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "UserId" }, "FK_ServerToUser");

                    b.ToTable("ServerList", (string)null);
                });

            modelBuilder.Entity("MoodleFetchBotAPI.Models.Tables.UserTable", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("DiscordId")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)");

                    b.Property<string>("MoodleDomain")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("MoodleToken")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("UserTable", (string)null);
                });

            modelBuilder.Entity("MoodleFetchBotAPI.Models.Tables.ServerList", b =>
                {
                    b.HasOne("MoodleFetchBotAPI.Models.Tables.UserTable", "User")
                        .WithMany("ServerLists")
                        .HasForeignKey("UserId")
                        .IsRequired()
                        .HasConstraintName("FK_ServerToUser");

                    b.Navigation("User");
                });

            modelBuilder.Entity("MoodleFetchBotAPI.Models.Tables.UserTable", b =>
                {
                    b.Navigation("ServerLists");
                });
#pragma warning restore 612, 618
        }
    }
}
