using SysInfo.Models;
using SysInfo.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace SysInfo.Repositories
{
    
    public interface IProjectRepository
    {
        Task<IEnumerable<Project>> GetAllProjectsAsync();
        Task<Project> GetProjectByIdAsync(int id);
        Task AddProjectAsync(ProjectDto project);
        Task UpdateProjectAsync(int projectId, ProjectDto projectDto);
        Task DeleteProjectAsync(int id);
        Task<IEnumerable<Project>> GetProjectsByTeamIdAsync(int teamId);
       // Task<IEnumerable<Project>> GetProjectsByClientIdAsync(int clientId);
    }

}
