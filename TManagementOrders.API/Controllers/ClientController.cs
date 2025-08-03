using Microsoft.AspNetCore.Mvc;
using TManagementOrders.Application.Service;
using TManagementOrders.Domain.Entities;

namespace TManagementOrders.API.Controllers
{
    public class ClientController : Controller
    {
        private readonly ClientService _clientService;

        public ClientController(ClientService clientService)
        {
          
            _clientService = clientService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string? filter)
        {
            var clients = await _clientService.GetAllAsync();

            var filteredClients = string.IsNullOrWhiteSpace(filter)
                ? clients.ToList()
                : clients
                    .Where(c =>
                        (!string.IsNullOrEmpty(c.Name) && c.Name.Contains(filter.Trim(), StringComparison.OrdinalIgnoreCase)) ||
                        (!string.IsNullOrEmpty(c.Email) && c.Email.Contains(filter.Trim(), StringComparison.OrdinalIgnoreCase)))
                    .ToList();

            var viewModel = new ClientFilterViewModel
            {
                Filter = filter,
                Clients = filteredClients
            };

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult GotoCreateClintPage()
        {
            return View("CreateUpdateClient");
        }

        [HttpPost]
        public async Task<IActionResult> CreateClient([FromBody] Client client)
        {
            if (!ModelState.IsValid)
                return View(client); 
            
            await _clientService.AddAsync(client);           
            return RedirectToAction("Index");
        }

        [HttpPut]
        public async Task<IActionResult> EditClient(int id, [FromBody] Client client)
        {
            if (id != client.Id)
                return BadRequest();

            await _clientService.UpdateAsync(client);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GotoEditClintPage(int id)
        {
            var client = await _clientService.GetByIdAsync(id);
            if(client == null)
                return NotFound();

            return View("CreateUpdateClient", client);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteClient(int id)
        {
            await _clientService.DeleteAsync(id);
            return Ok();
        }
    }
}
