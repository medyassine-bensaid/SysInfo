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

        // Create Administrator Endpoint
        [HttpPost("create")]
        public async Task<IActionResult> CreateAdministrator([FromBody] Administrator admin)
        {
            if (admin == null || string.IsNullOrEmpty(admin.Email) || string.IsNullOrEmpty(admin.Password))
            {
                return BadRequest("Invalid administrator data.");
            }

            // Hash password before saving
            admin.Password = BCrypt.Net.BCrypt.HashPassword(admin.Password);

            // Save administrator to repository
            await _adminRepository.AddAdministratorAsync(admin);

            return CreatedAtAction(nameof(GetAdministratorByEmail), new { email = admin.Email }, admin);
        }

        // Get Administrator By Email
        [HttpGet("{email}")]
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
