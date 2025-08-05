using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TManagementOrders.API.Helper;
using TManagementOrders.Application.Service;
using TManagementOrders.Domain.Entities;
using TManagementOrders.Domain.Interfaces;

namespace TManagementOrders.API.Controllers
{
    public class OrdersController : Controller
    {
        private readonly OrderService _ordersService;
        private readonly IBaseInterface<Order> _baseRepository;
        private readonly ClientService _clientService;
        Translate translate = new Translate();
        public OrdersController(OrderService ordersService,
                                IBaseInterface<Order> baseInterface,
                                ClientService clientService)
        {
            _ordersService = ordersService;
            _baseRepository = baseInterface;
            _clientService = clientService;
        }

        public async Task<IActionResult> Index(string? ClientFilter, string? StatusFilter)
        {
           
            IEnumerable<Order> orders = await _baseRepository.GetAllAsync();
            IEnumerable<Client> clients = await _clientService.GetAllAsync();

            var orderView = new List<OrderViewModel>();

            foreach (var order in orders)
            {
                var client = clients.FirstOrDefault(c => c.Id == order.IdClient);

                orderView.Add(new OrderViewModel
                {
                    Id = order.Id,
                    IdClient = order.IdClient,
                    ClientName = client.Name,
                    DateOrder = order.DateOrder,
                    Status = translate.TranslateStatus(order.Status),
                    Total = order.Total,
                });           
            }   


            var model = new OrderFilterViewModel
            {
                OrderItems = orderView,
                SelectedClientName = ClientFilter
            };

            return View(model);
        }


        [HttpGet]
        public IActionResult GotoCreateOrderPage()
        {
            return View("CreateOrder");
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] Order neworder)
        {          
            try
            {
                await _ordersService.AddAsync(neworder);
                return RedirectToAction("Index"); 
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View("Index"); 
            }
        }

        [HttpGet]
        public async Task<IActionResult> GotoDetailOrderPage(int Id)
        {

            try
            {
                Order order = await _ordersService.GetById(Id);
                return View("DetailOrder", order);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View("Index");
            }
        }

    }
}
