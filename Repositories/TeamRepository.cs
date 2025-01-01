using Microsoft.EntityFrameworkCore;
using SysInfo.Infrastructure.Data;
using SysInfo.Models;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace SysInfo.Repositories
{
    
    public class TeamRepository : ITeamRepository
    {
        private readonly AppDbContext _context;

        public TeamRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Team>> GetAllTeamsAsync()
{
    return await _context.Teams
        .Include(t => t.TeamLeader) // Include the TeamLeader
        .Include(t => t.TeamMembers) // Include the TeamMembers
        .Select(t => new Team
        {
            Id = t.Id,
            Name = t.Name,
            Members = t.Members,
            TeamLeaderId = t.TeamLeaderId,
            TeamLeader = t.TeamLeader != null
                ? new User
                {
                    Id = t.TeamLeader.Id,
                    FirstName = t.TeamLeader.FirstName,
                    LastName = t.TeamLeader.LastName
                }
                : null,
            // Include TeamMembers but avoid deep navigation properties that may cause cycles
            TeamMembers = t.TeamMembers != null
                ? t.TeamMembers.Select(m => new User
                {
                    Id = m.Id,
                    FirstName = m.FirstName,
                    LastName = m.LastName
                }).ToList()
                : null,
            Projects = t.Projects // Include Projects if necessary
        })
        .ToListAsync();
}

        

        public async Task<Team> GetTeamByIdAsync(int id)
        {
            // Retrieve a team by its ID without including related entities
            return await _context.Teams.FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task AddTeamAsync(Team team)
        {
            if (team.TeamLeaderId.HasValue)
            {
                var existingUser = new User { Id = team.TeamLeaderId.Value };
                _context.Users.Attach(existingUser); // Attach the existing TeamLeader
                team.TeamLeader = existingUser;     // Associate the TeamLeader
            }

            if (team.TeamMembers != null && team.TeamMembers.Any())
            {
                var existingTeamMembers = team.TeamMembers
                    .Select(member => new User { Id = member.Id }) // Create User objects with only the Id
                    .ToList();

                foreach (var member in existingTeamMembers)
                {
                    _context.Users.Attach(member); // Attach each existing User
                }

                team.TeamMembers = existingTeamMembers; // Associate the TeamMembers with the team
            }

            _context.Teams.Add(team); // Add the team to the context
            await _context.SaveChangesAsync();
        }


        public async Task UpdateTeamAsync(Team team)
        {
            // Update an existing team
            _context.Teams.Update(team);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTeamAsync(int id)
        {
            // Delete a team by its ID
            var team = await _context.Teams.FindAsync(id);
            if (team != null)
            {
                _context.Teams.Remove(team);
                await _context.SaveChangesAsync();
            }
        }

  
       
    }

}
