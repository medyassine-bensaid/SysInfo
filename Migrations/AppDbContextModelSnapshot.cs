﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SysInfo.Infrastructure.Data;

#nullable disable

namespace SysInfo.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("ClientProject", b =>
                {
                    b.Property<int>("ClientsId")
                        .HasColumnType("int");

                    b.Property<int>("ProjectsId")
                        .HasColumnType("int");

                    b.HasKey("ClientsId", "ProjectsId");

                    b.HasIndex("ProjectsId");

                    b.ToTable("ProjectClients", (string)null);
                });

            modelBuilder.Entity("ProjectTeam", b =>
                {
                    b.Property<int>("ProjectsId")
                        .HasColumnType("int");

                    b.Property<int>("TeamsId")
                        .HasColumnType("int");

                    b.HasKey("ProjectsId", "TeamsId");

                    b.HasIndex("TeamsId");

                    b.ToTable("ProjectTeams", (string)null);
                });

            modelBuilder.Entity("SysInfo.Models.Administrator", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Administrators");
                });

            modelBuilder.Entity("SysInfo.Models.Client", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClientType")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<int>("PhoneNumber")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("SysInfo.Models.Project", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int?>("Id"));

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("SysInfo.Models.Team", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Members")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<int?>("TeamLeaderId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TeamLeaderId");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("SysInfo.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Fonction")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<int>("PhoneNumber")
                        .HasColumnType("int");

                    b.Property<string>("Profile")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("TeamUser", b =>
                {
                    b.Property<int>("TeamMembersId")
                        .HasColumnType("int");

                    b.Property<int>("TeamsId")
                        .HasColumnType("int");

                    b.HasKey("TeamMembersId", "TeamsId");

                    b.HasIndex("TeamsId");

                    b.ToTable("TeamMembers", (string)null);
                });

            modelBuilder.Entity("ClientProject", b =>
                {
                    b.HasOne("SysInfo.Models.Client", null)
                        .WithMany()
                        .HasForeignKey("ClientsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SysInfo.Models.Project", null)
                        .WithMany()
                        .HasForeignKey("ProjectsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProjectTeam", b =>
                {
                    b.HasOne("SysInfo.Models.Project", null)
                        .WithMany()
                        .HasForeignKey("ProjectsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SysInfo.Models.Team", null)
                        .WithMany()
                        .HasForeignKey("TeamsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SysInfo.Models.Team", b =>
                {
                    b.HasOne("SysInfo.Models.User", "TeamLeader")
                        .WithMany("LedTeams")
                        .HasForeignKey("TeamLeaderId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("TeamLeader");
                });

            modelBuilder.Entity("TeamUser", b =>
                {
                    b.HasOne("SysInfo.Models.User", null)
                        .WithMany()
                        .HasForeignKey("TeamMembersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SysInfo.Models.Team", null)
                        .WithMany()
                        .HasForeignKey("TeamsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SysInfo.Models.User", b =>
                {
                    b.Navigation("LedTeams");
                });
#pragma warning restore 612, 618
        }
    }
}
