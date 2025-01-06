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

        public async Task UpdateProjectAsync(int projectId, Project projectDto)
        {
            // Load the project with its related entities
            var project = await _context.Projects
                .Include(p => p.Teams)
                .Include(p => p.Clients)
                .FirstOrDefaultAsync(p => p.Id == projectId);

            if (project == null) return;

            // Update basic properties
            project.Name = projectDto.Name;
            project.StartDate = projectDto.StartDate;
            project.EndDate = projectDto.EndDate;

            // Update Teams
            if (projectDto.Teams != null && projectDto.Teams.Any())
            {
                // Clear existing associations
                project.Teams.Clear();

                // Attach the existing Teams by fetching full team details
                foreach (var team in projectDto.Teams)
                {
                    var existingTeam = await _context.Teams
                        .FirstOrDefaultAsync(t => t.Id == team.Id); // Retrieve the full team

                    if (existingTeam != null)
                    {
                        _context.Teams.Attach(existingTeam); // Attach the full team entity
                        project.Teams.Add(existingTeam); // Associate the full team with the project
                    }
                }
            }
            else
            {
                // Remove all Teams if not provided
                project.Teams.Clear();
            }

            // Update Clients
            if (projectDto.Clients != null && projectDto.Clients.Any())
            {
                // Clear existing associations
                project.Clients.Clear();

                // Attach the existing Clients by fetching full client details
                foreach (var client in projectDto.Clients)
                {
                    var existingClient = await _context.Clients
                        .FirstOrDefaultAsync(c => c.Id == client.Id); // Retrieve the full client

                    if (existingClient != null)
                    {
                        _context.Clients.Attach(existingClient); // Attach the full client entity
                        project.Clients.Add(existingClient); // Associate the full client with the project
                    }
                }
            }
            else
            {
                // Remove all Clients if not provided
                project.Clients.Clear();
            }

            // Mark project as modified
            _context.Entry(project).State = EntityState.Modified;

            // Save changes to the database
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
