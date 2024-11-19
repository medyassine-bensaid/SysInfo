using SysInfo.Models;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace SysInfo.Repositories
{
   
    public interface ITeamRepository
    {
        Task<IEnumerable<Team>> GetAllTeamsAsync();
        Task<Team> GetTeamByIdAsync(int id);
        Task AddTeamAsync(Team team);
        Task UpdateTeamAsync(Team team);
        Task DeleteTeamAsync(int id);
        Task<IEnumerable<Team>> GetAllTeamsWithLeadersAsync();
        Task<Team> GetTeamWithLeaderByIdAsync(int teamId);
    }

}
