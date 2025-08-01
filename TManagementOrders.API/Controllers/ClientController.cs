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

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(Client client)
        {
            if (!ModelState.IsValid)
                return View(client);

            await _clientService.AddAsync(client);
            return RedirectToAction(nameof(Index));
        }

        // GET: Client
        //public async Task<IActionResult> Index()
        //{
        //    var clients = await _clientService.GetAllAsync();
        //    return View(clients); // Views/Client/Index.cshtml
        //}

        //// GET: Client/Details/5
        //public async Task<IActionResult> Details(int id)
        //{
        //    var client = await _clientService.GetByIdAsync(id);
        //    if (client == null) return NotFound();

        //    return View(client); // Views/Client/Details.cshtml
        //}

        //// GET: Client/Create
        //public IActionResult Create()
        //{
        //    return View(); // Views/Client/Create.cshtml
        //}


        //// GET: Client/Edit/5
        //public async Task<IActionResult> Edit(int id)
        //{
        //    var client = await _clientService.GetByIdAsync(id);
        //    if (client == null) return NotFound();

        //    return View(client); // Views/Client/Edit.cshtml
        //}

        //// POST: Client/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, Client client)
        //{
        //    if (id != client.Id)
        //        return NotFound();

        //    if (!ModelState.IsValid)
        //        return View(client);

        //    var updated = await _clientService.UpdateAsync(client);
        //    if (!updated) return NotFound();

        //    return RedirectToAction(nameof(Index));
        //}

        //// GET: Client/Delete/5
        //public async Task<IActionResult> Delete(int id)
        //{
        //    var client = await _clientService.GetByIdAsync(id);
        //    if (client == null) return NotFound();

        //    return View(client); // Views/Client/Delete.cshtml
        //}

        //// POST: Client/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    await _clientService.DeleteAsync(id);
        //    return RedirectToAction(nameof(Index));
        //}
    }
}
