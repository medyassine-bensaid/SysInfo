﻿using Microsoft.EntityFrameworkCore;
using SysInfo.Infrastructure.Data;
using SysInfo.Models;

namespace SysInfo.Repositories
{
    public class AdministratorRepository : IAdministratorRepository
    {
        private readonly AppDbContext _context;

        public AdministratorRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Administrator> GetAdministratorByEmailAsync(string email)
        {
            return await _context.Administrators.FirstOrDefaultAsync(a => a.Email == email);
        }
    }

}
