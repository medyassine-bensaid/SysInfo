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
                            LastName = t.TeamLeader.LastName,
                            Email = t.TeamLeader.Email,
                            Fonction = t.TeamLeader.Fonction,
                            Profile = t.TeamLeader.Profile
                        }
                        : null,
                    // Include TeamMembers but avoid deep navigation properties that may cause cycles
                    TeamMembers = t.TeamMembers != null
                        ? t.TeamMembers.Select(m => new User
                        {
                            Id = m.Id,
                            FirstName = m.FirstName,
                            LastName = m.LastName,
                            Email = m.Email,
                            Fonction = m.Fonction,
                            Profile = m.Profile
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
            // Attach the existing team to the context
            var existingTeam = await _context.Teams
                .Include(t => t.TeamLeader) // Include the TeamLeader
                .Include(t => t.TeamMembers) // Include the TeamMembers
                .FirstOrDefaultAsync(t => t.Id == team.Id);

            if (existingTeam == null)
            {
                throw new InvalidOperationException($"Team with ID {team.Id} does not exist.");
            }

            // Update TeamLeader if provided
            if (team.TeamLeaderId.HasValue)
            {
                // Attach the new TeamLeader (if changed)
                if (existingTeam.TeamLeader?.Id != team.TeamLeaderId)
                {
                    var newTeamLeader = new User { Id = team.TeamLeaderId.Value };
                    _context.Users.Attach(newTeamLeader); // Attach the new TeamLeader
                    existingTeam.TeamLeader = newTeamLeader; // Associate the new TeamLeader
                }
            }
            else
            {
                // Remove the TeamLeader if not provided
                existingTeam.TeamLeader = null;
            }

            // Update TeamMembers
            if (team.TeamMembers != null && team.TeamMembers.Any())
            {
                // Detach all current TeamMembers
                existingTeam.TeamMembers.Clear();

                // Attach the existing TeamMembers by fetching full user details
                foreach (var member in team.TeamMembers)
                {
                    var existingMember = await _context.Users
                        .FirstOrDefaultAsync(u => u.Id == member.Id); // Retrieve the full user

                    if (existingMember != null)
                    {
                        _context.Users.Attach(existingMember); // Attach the full user entity
                        existingTeam.TeamMembers.Add(existingMember); // Associate the full user with the team
                    }
                }
            }
            else
            {
                // Remove all TeamMembers if not provided
                existingTeam.TeamMembers.Clear();
            }


            // Update other properties of the team
            existingTeam.Name = team.Name;

            // Mark the team entity as modified
            _context.Entry(existingTeam).State = EntityState.Modified;

            // Save changes to the database
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
