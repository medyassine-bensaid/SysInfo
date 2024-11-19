using global::SysInfo.Models;
using global::SysInfo.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace SysInfo.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class TeamsController : ControllerBase
    {
        private readonly ITeamRepository _repository;

        public TeamsController(ITeamRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IEnumerable<Team>> GetAllTeams()
        {
            return await _repository.GetAllTeamsAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Team>> GetTeamById(int id)
        {
            var team = await _repository.GetTeamByIdAsync(id);
            if (team == null) return NotFound();
            return team;
        }

        [HttpPost]
        public async Task<IActionResult> AddTeam(Team team)
        {
            await _repository.AddTeamAsync(team);
            return CreatedAtAction(nameof(GetTeamById), new { id = team.Id }, team);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTeam(int id, Team team)
        {
            if (id != team.Id) return BadRequest();
            await _repository.UpdateTeamAsync(team);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeam(int id)
        {
            await _repository.DeleteTeamAsync(id);
            return NoContent();
        }


    }

}
