
using Microsoft.EntityFrameworkCore;
using SysInfo.Infrastructure.Data;
using SysInfo.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SysInfo.Repositories
{

    public class ProjectRepository : IProjectRepository
    {
        private readonly AppDbContext _context;

        public ProjectRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Project>> GetAllProjectsAsync()
        {
            // Simply retrieve all projects without including related entities
            return await _context.Projects.ToListAsync();
        }

        public async Task<Project> GetProjectByIdAsync(int id)
        {
            // Retrieve a project by its ID without including related entities
            return await _context.Projects.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task AddProjectAsync(Project project)
        {
            // Add a new project
            await _context.Projects.AddAsync(project);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProjectAsync(Project project)
        {
            // Update an existing project
            _context.Projects.Update(project);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProjectAsync(int id)
        {
            // Delete a project by its ID
            var project = await _context.Projects.FindAsync(id);
            if (project != null)
            {
                _context.Projects.Remove(project);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Project>> GetProjectsByTeamIdAsync(int teamId)
        {
            // Retrieve projects by team ID
            return await _context.Projects.Where(p => p.TeamId == teamId).ToListAsync();
        }

        public async Task<IEnumerable<Project>> GetProjectsByClientIdAsync(int clientId)
        {
            // Retrieve projects by client ID
            return await _context.Projects.Where(p => p.ClientId == clientId).ToListAsync();
        }
    }

}
