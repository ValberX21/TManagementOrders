using Microsoft.AspNetCore.Mvc;
using TManagementOrders.Application.Service;
using TManagementOrders.Domain.Entities;

namespace TManagementOrders.API.Controllers
{
    public class OrdersController : Controller
    {
        private readonly OrderService _ordersService;

        public OrdersController(OrderService ordersService)
        {
            _ordersService = ordersService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GotoCreateOrderPage()
        {
            return View("CreateOrder");
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] Orders neworder)
        {
            var x =  _ordersService.AddAsync(neworder);
            // Here you would typically handle the order creation logic
            // For now, we will just return a success message
            return Ok("Order created successfully!");
        }
    }
}
