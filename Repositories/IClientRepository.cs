using SysInfo.Models;

namespace SysInfo.Repositories
{
    public interface IClientRepository
    {
        Task<IEnumerable<Client>> GetAllClientsAsync();
        Task<Client> GetClientByIdAsync(int id);
        Task AddClientAsync(Client client);
        Task UpdateClientAsync(Client client);
        Task DeleteClientAsync(int id);
        Task<IEnumerable<Client>> GetClientsByTypeAsync(string clientType);
        Task<Client> GetClientByEmailAsync(string email); 
    }
}
