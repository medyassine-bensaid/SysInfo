using Microsoft.EntityFrameworkCore;
using SysInfo.Infrastructure.Data;
using SysInfo.Models;
using SysInfo.Repositories;

namespace SysInfo.Services
{
    public class AdministratorRepository : IAdministratorRepository
    {
        private readonly AppDbContext _context;

        public AdministratorRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAdministratorAsync(Administrator admin)
        {
            // Add the administrator to the database
            await _context.Administrators.AddAsync(admin);
            await _context.SaveChangesAsync();
        }
        public async Task<Administrator> GetAdministratorByEmailAsync(string email)
        {
            return await _context.Administrators.FirstOrDefaultAsync(a => a.Email == email);
        }
    }

}
