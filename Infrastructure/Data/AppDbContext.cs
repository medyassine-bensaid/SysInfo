using Microsoft.EntityFrameworkCore;
using SysInfo.Models;
namespace SysInfo.Infrastructure.Data
{


    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Client> Clients { get; set; }
        public DbSet<User> Users { get; set; }

        public DbSet<Team> Teams { get; set; }
        public DbSet<Project> Projects { get; set; }
        //public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Administrator> Administrators { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //    base.OnModelCreating(modelBuilder);

            //    // Team relationships
            //    //modelBuilder.Entity<Team>()
            //    //    .HasOne(t => t.TeamLeader) // Team leader navigation
            //    //    .WithMany(u => u.LedTeams)
            //    //    .HasForeignKey(t => t.TeamLeaderId) // Add foreign key
            //    //    .OnDelete(DeleteBehavior.SetNull); // Prevent cascade delete

            //    modelBuilder.Entity<Team>()
            //.HasOne(t => t.TeamLeader) // Navigation property
            //.WithMany(u => u.Teams) // Inverse navigation
            //.HasForeignKey(t => t.TeamLeaderId) // Foreign key
            //.OnDelete(DeleteBehavior.SetNull); // Allow NULL on delete

            //    //modelBuilder.Entity<Team>()
            //    //    .HasMany(t => t.TeamMembers)
            //    //    .WithMany(u => u.Teams)
            //    //    .UsingEntity<Dictionary<string, object>>(
            //    //        "TeamUser", // Define join table name
            //    //        j => j.HasOne<User>().WithMany().HasForeignKey("UserId"),
            //    //        j => j.HasOne<Team>().WithMany().HasForeignKey("TeamId")
            //    //    );

            //    // Project relationships
            //    modelBuilder.Entity<Project>()
            //        .HasOne(p => p.Team)
            //        .WithMany()
            //        .HasForeignKey(p => p.TeamId)
            //        .OnDelete(DeleteBehavior.Cascade); // Add delete behavior if needed

            //    modelBuilder.Entity<Project>()
            //        .HasOne(p => p.Client)
            //        .WithMany(c => c.Projects)
            //        .HasForeignKey(p => p.ClientId)
            //        .OnDelete(DeleteBehavior.SetNull);



            //// Feedback relationships
            //modelBuilder.Entity<Feedback>()
            //    .HasOne(f => f.Client)
            //    .WithMany(c => c.Feedbacks)
            //    .HasForeignKey(f => f.ClientId);

            //modelBuilder.Entity<Feedback>()
            //    .HasOne(f => f.Project)
            //    .WithMany()
            //    .HasForeignKey(f => f.ProjectId);

            // Employee-Team Relationships
            modelBuilder.Entity<Team>()
                .HasOne(t => t.TeamLeader)
                .WithMany(e => e.LedTeams)
                .HasForeignKey(t => t.TeamLeaderId)
                .HasPrincipalKey(e => e.Id)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Team>()
                .HasMany(t => t.TeamMembers)
                .WithMany(e => e.Teams)
                .UsingEntity(j => j.ToTable("TeamMembers"));

            // Client-Project Relationship
            modelBuilder.Entity<Project>()
                .HasMany(p => p.Clients)
                .WithMany(c => c.Projects)
                .UsingEntity(j => j.ToTable("ProjectClients"));

            // Project-Team Relationship
            modelBuilder.Entity<Project>()
                .HasMany(p => p.Teams)
                .WithMany(t => t.Projects)
                .UsingEntity(j => j.ToTable("ProjectTeams"));
                


            base.OnModelCreating(modelBuilder);

        }

    }

}
