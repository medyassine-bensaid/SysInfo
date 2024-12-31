using SysInfo.Models;

namespace SysInfo.Repositories
{
    public interface IAdministratorRepository
    {
        Task<Administrator> GetAdministratorByEmailAsync(string email);

        Task AddAdministratorAsync(Administrator admin);

    }

}
