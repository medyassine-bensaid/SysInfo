using Microsoft.AspNetCore.Mvc;
using SysInfo.Models;
using SysInfo.Repositories;

namespace SysInfo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdministratorController : ControllerBase
    {
        private readonly IAdministratorRepository _adminRepository;

        public AdministratorController(IAdministratorRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }

        [HttpGet("login/{email}")]
        public async Task<ActionResult<Administrator>> GetAdministratorByEmail(string email)
        {
            var admin = await _adminRepository.GetAdministratorByEmailAsync(email);
            if (admin == null)
            {
                return NotFound($"Administrator with email {email} not found.");
            }
            return Ok(admin);
        }
    }

}
