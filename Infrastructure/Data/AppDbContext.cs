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
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Administrator> Administrators { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Team relationships
            modelBuilder.Entity<Team>()
         .HasOne(t => t.TeamLeader) // Navigation property for the leader
         .WithMany(u => u.LedTeams) // Reverse navigation for LedTeams
         .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete
            modelBuilder.Entity<Team>()
                .HasMany(t => t.TeamMembers)
                .WithMany(u => u.Teams);

            // Project relationships
            modelBuilder.Entity<Project>()
                .HasOne(p => p.Team)
                .WithMany()
                .HasForeignKey(p => p.TeamId);

            modelBuilder.Entity<Project>()
                .HasOne(p => p.Client)
                .WithMany(c => c.Projects)
                .HasForeignKey(p => p.ClientId);

            // Feedback relationships
            modelBuilder.Entity<Feedback>()
                .HasOne(f => f.Client)
                .WithMany(c => c.Feedbacks)
                .HasForeignKey(f => f.ClientId);

            modelBuilder.Entity<Feedback>()
                .HasOne(f => f.Project)
                .WithMany()
                .HasForeignKey(f => f.ProjectId);
        }

    }

}
