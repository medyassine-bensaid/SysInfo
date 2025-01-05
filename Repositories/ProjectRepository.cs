using Microsoft.EntityFrameworkCore;
using SysInfo.Infrastructure.Data;
using SysInfo.Models;
using SysInfo.ViewModel;
using System.Collections.Generic;
using System.Linq;
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
            return await _context.Projects
                .Include(p => p.Teams)
                .Include(p => p.Clients)
                .ToListAsync();
        }

        public async Task<Project> GetProjectByIdAsync(int id)
        {
            return await _context.Projects
                .Include(p => p.Teams)
                .Include(p => p.Clients)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task AddProjectAsync(ProjectDto projectDto)
        {
            // Map DTO to Project Entity
            var project = new Project
            {
                Name = projectDto.Name,
                StartDate = projectDto.StartDate,
                EndDate = projectDto.EndDate
            };

            // Attach Teams by IDs
            if (projectDto.TeamIds != null && projectDto.TeamIds.Any())
            {
                project.Teams = await _context.Teams
                    .Where(t => projectDto.TeamIds.Contains(t.Id))
                    .ToListAsync();
            }

            // Attach Clients by IDs
            if (projectDto.ClientIds != null && projectDto.ClientIds.Any())
            {
                project.Clients = await _context.Clients
                    .Where(c => projectDto.ClientIds.Contains(c.Id))
                    .ToListAsync();
            }

            // Add and save project
            await _context.Projects.AddAsync(project);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProjectAsync(int projectId, ProjectDto projectDto)
        {
            var project = await _context.Projects
                .Include(p => p.Teams)
                .Include(p => p.Clients)
                .FirstOrDefaultAsync(p => p.Id == projectId);

            if (project == null) return;

            project.Name = projectDto.Name;
            project.StartDate = projectDto.StartDate;
            project.EndDate = projectDto.EndDate;

            // Update Teams
            project.Teams.Clear();
            project.Teams = await _context.Teams
                .Where(t => projectDto.TeamIds.Contains(t.Id))
                .ToListAsync();

            // Update Clients
            project.Clients.Clear();
            project.Clients = await _context.Clients
                .Where(c => projectDto.ClientIds.Contains(c.Id))
                .ToListAsync();

            await _context.SaveChangesAsync();
        }

        public async Task DeleteProjectAsync(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project != null)
            {
                _context.Projects.Remove(project);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Project>> GetProjectsByTeamIdAsync(int teamId)
        {
            return await _context.Projects
                .Where(p => p.Teams.Any(t => t.Id == teamId))
                .ToListAsync();
        }

        public async Task<IEnumerable<Project>> GetProjectsByClientIdAsync(int clientId)
        {
            return await _context.Projects
                .Where(p => p.Clients.Any(c => c.Id == clientId))
                .ToListAsync();
        }
    }
}
