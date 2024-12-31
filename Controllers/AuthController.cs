using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SysInfo.Infrastructure.Data;
using SysInfo.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SysInfo.Controllers
{
    [Route("api/Auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AuthController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Administrator loginRequest)
        {
            if (loginRequest is null || string.IsNullOrEmpty(loginRequest.Email) || string.IsNullOrEmpty(loginRequest.Password))
            {
                return BadRequest("Invalid client request");
            }

            // Fetch administrator by email
            var administrator = await _context.Administrators.FirstOrDefaultAsync(a => a.Email == loginRequest.Email);

            if (administrator == null || !BCrypt.Net.BCrypt.Verify(loginRequest.Password, administrator.Password))
            {
                return Unauthorized("Invalid email or password");
            }

            // Generate JWT token
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345678901234567890123456"));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, administrator.Email)
            };

            var tokenOptions = new JwtSecurityToken(
                issuer: "https://localhost:5131",
                audience: "https://localhost:5131",
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: signinCredentials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return Ok(new AuthenticatedResponse { Token = tokenString });
        }
    }
}
