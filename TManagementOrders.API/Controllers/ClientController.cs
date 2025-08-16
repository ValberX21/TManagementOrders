using Microsoft.AspNetCore.Mvc;
using TManagementOrders.Application.Service;
using TManagementOrders.Domain.Entities;
using TManagementOrders.Domain.Interfaces;

namespace TManagementOrders.API.Controllers
{
    public class ClientController : Controller
    {
        private readonly IBaseInterfaceService<Client> _baseService;
        private readonly ClientService _clientService;

        public ClientController(IBaseInterfaceService<Client> baseService,
                                ClientService clientService)
        {
            _baseService = baseService;
            _clientService = clientService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string? filter)
        {
            List<Client> clientFilter = await _clientService.FilterClient(filter);

            var viewModel = new ClientFilterViewModel
            {
                Filter = filter,
                Clients = clientFilter
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

            await _baseService.AddAsync(client);
            return RedirectToAction("Index");
        }

        [HttpPut]
        public async Task<IActionResult> EditClient(int id, [FromBody] Client client)
        {
            if (id != client.Id)
                return BadRequest();

            await _baseService.UpdateAsync(client);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GotoEditClintPage(int id)
        {
            var client = await _baseService.GetByIdAsync(id);
            if (client == null)
                return NotFound();

            return View("CreateUpdateClient", client);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteClient(int id)
        {
            await _baseService.DeleteAsync(id);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> SearchClient(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return BadRequest("Name cannot be empty");

            var client = await _clientService.GetByNameAsync(name);
            if (client == null)
                return NotFound();
            return Ok(client);
        }
    }
}
