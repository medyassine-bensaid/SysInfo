namespace SysInfo.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Threading.Tasks;
   
    using global::SysInfo.Repositories;
    using global::SysInfo.Models;

    namespace SysInfo.Controllers
    {
        [Route("api/[controller]")]
        [ApiController]
        public class ClientController : ControllerBase
        {
            private readonly IClientRepository _clientRepository;

            public ClientController(IClientRepository clientRepository)
            {
                _clientRepository = clientRepository;
            }

            // GET: api/Client
            [HttpGet]
            public async Task<ActionResult<IEnumerable<Client>>> GetAllClients()
            {
                var clients = await _clientRepository.GetAllClientsAsync();
                return Ok(clients);
            }

            // GET: api/Client/{id}
            [HttpGet("{id}")]
            public async Task<ActionResult<Client>> GetClientById(int id)
            {
                var client = await _clientRepository.GetClientByIdAsync(id);
                if (client == null)
                    return NotFound();

                return Ok(client);
            }

            // POST: api/Client
            [HttpPost]
            public async Task<ActionResult> AddClient(Client client)
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                await _clientRepository.AddClientAsync(client);
                return CreatedAtAction(nameof(GetClientById), new { id = client.Id }, client);
            }

            // PUT: api/Client/{id}
            [HttpPut("{id}")]
            public async Task<IActionResult> UpdateClient(int id, Client client)
            {
                if (id != client.Id)
                    return BadRequest("Client ID mismatch");

                await _clientRepository.UpdateClientAsync(client);
                return NoContent();
            }

            // DELETE: api/Client/{id}
            [HttpDelete("{id}")]
            public async Task<IActionResult> DeleteClient(int id)
            {
                var client = await _clientRepository.GetClientByIdAsync(id);
                if (client == null)
                    return NotFound();

                await _clientRepository.DeleteClientAsync(id);
                return NoContent();
            }

            // GET: api/Client/byType/{clientType}
            [HttpGet("byType/{clientType}")]
            public async Task<ActionResult<IEnumerable<Client>>> GetClientsByType(string clientType)
            {
                var clients = await _clientRepository.GetClientsByTypeAsync(clientType);
                return Ok(clients);
            }

            // GET: api/Client/byEmail/{email}
            [HttpGet("byEmail/{email}")]
            public async Task<ActionResult<Client>> GetClientByEmail(string email)
            {
                var client = await _clientRepository.GetClientByEmailAsync(email);
                if (client == null)
                    return NotFound();

                return Ok(client);
            }
        }
    }

}
