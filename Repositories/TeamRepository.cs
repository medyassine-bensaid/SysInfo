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
            // Simply retrieve all teams without including related entities
            return await _context.Teams.ToListAsync();
        }

        public async Task<Team> GetTeamByIdAsync(int id)
        {
            // Retrieve a team by its ID without including related entities
            return await _context.Teams.FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task AddTeamAsync(Team team)
        {
            // Add a new team
            await _context.Teams.AddAsync(team);
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

        // Get all teams with their leaders
        public async Task<IEnumerable<Team>> GetAllTeamsWithLeadersAsync()
        {
            return await _context.Teams
                .Include(t => t.TeamLeader) // Include the TeamLeader navigation property
                .ToListAsync();
        }

        // Find a specific team with its leader by ID
        public async Task<Team> GetTeamWithLeaderByIdAsync(int teamId)
        {
            return await _context.Teams
                .Include(t => t.TeamLeader) // Include the TeamLeader navigation property
                .FirstOrDefaultAsync(t => t.Id == teamId);
        }
    }

}
