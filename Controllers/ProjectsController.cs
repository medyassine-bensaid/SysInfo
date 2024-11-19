namespace SysInfo.Controllers
{
    using global::SysInfo.Models;
    using global::SysInfo.Repositories;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectsController(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        // GET: api/Projects
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Project>>> GetAllProjects()
        {
            var projects = await _projectRepository.GetAllProjectsAsync();
            return Ok(projects);
        }

        // GET: api/Projects/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Project>> GetProjectById(int id)
        {
            var project = await _projectRepository.GetProjectByIdAsync(id);
            if (project == null)
            {
                return NotFound($"Project with ID {id} not found.");
            }
            return Ok(project);
        }

        // POST: api/Projects
        [HttpPost]
        public async Task<ActionResult> AddProject([FromBody] Project project)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _projectRepository.AddProjectAsync(project);
            return CreatedAtAction(nameof(GetProjectById), new { id = project.Id }, project);
        }

        // PUT: api/Projects/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProject(int id, [FromBody] Project project)
        {
            if (id != project.Id)
            {
                return BadRequest("Project ID mismatch.");
            }

            var existingProject = await _projectRepository.GetProjectByIdAsync(id);
            if (existingProject == null)
            {
                return NotFound($"Project with ID {id} not found.");
            }

            await _projectRepository.UpdateProjectAsync(project);
            return NoContent();
        }

        // DELETE: api/Projects/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProject(int id)
        {
            var existingProject = await _projectRepository.GetProjectByIdAsync(id);
            if (existingProject == null)
            {
                return NotFound($"Project with ID {id} not found.");
            }

            await _projectRepository.DeleteProjectAsync(id);
            return NoContent();
        }

        // GET: api/Projects/team/{teamId}
        [HttpGet("team/{teamId}")]
        public async Task<ActionResult<IEnumerable<Project>>> GetProjectsByTeamId(int teamId)
        {
            var projects = await _projectRepository.GetProjectsByTeamIdAsync(teamId);
            if (projects == null || !projects.Any())
            {
                return NotFound($"No projects found for Team ID {teamId}.");
            }
            return Ok(projects);
        }

        // GET: api/Projects/client/{clientId}
        [HttpGet("client/{clientId}")]
        public async Task<ActionResult<IEnumerable<Project>>> GetProjectsByClientId(int clientId)
        {
            var projects = await _projectRepository.GetProjectsByClientIdAsync(clientId);
            if (projects == null || !projects.Any())
            {
                return NotFound($"No projects found for Client ID {clientId}.");
            }
            return Ok(projects);
        }
    }

}
