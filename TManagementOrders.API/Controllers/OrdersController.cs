using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TManagementOrders.API.Helper;
using TManagementOrders.Application.Service;
using TManagementOrders.Domain.Entities;
using TManagementOrders.Domain.Enums;
using TManagementOrders.Domain.Interfaces;

namespace TManagementOrders.API.Controllers
{
    public class OrdersController : Controller
    {
        private readonly OrderService _ordersService;
        private readonly IBaseInterface<Order> _baseRepository;
        private readonly ClientService _clientService;
        private readonly ProductService _productService;

        Translate translate = new Translate();

        public OrdersController(OrderService ordersService,
                                IBaseInterface<Order> baseInterface,
                                ClientService clientService,
                                ProductService productService)
        {
            _ordersService = ordersService;
            _baseRepository = baseInterface;
            _clientService = clientService;
            _productService = productService;
        }

        public async Task<IActionResult> Index(string? ClientFilter, string? StatusFilter)
        {
            IEnumerable<Order> orders = await _baseRepository.GetAllAsync();
            IEnumerable<Client> clients = await _clientService.GetAllAsync();

            var orderViews = orders.Select(order =>
            {
                var client = clients.FirstOrDefault(c => c.Id == order.IdClient);
                return new OrderViewModel
                {
                    Id = order.Id,
                    IdClient = order.IdClient,
                    ClientName = client?.Name ?? "Desconhecido",
                    DateOrder = order.DateOrder,
                    Total = order.Total,
                    Status = order.Status.ToString()
                };
            }).ToList();

            if (!string.IsNullOrEmpty(ClientFilter))
                orderViews = orderViews
                    .Where(o => o.ClientName.Contains(ClientFilter, StringComparison.OrdinalIgnoreCase))
                    .ToList();

            if (!string.IsNullOrEmpty(StatusFilter))
                orderViews = orderViews
                    .Where(o => o.Status.Equals(StatusFilter, StringComparison.OrdinalIgnoreCase))
                    .ToList();

            var model = new OrderFilterViewModel
            {
                SelectedClientName = ClientFilter,
                SelectedStatus = StatusFilter,
                OrderItems = orderViews
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
                IEnumerable<Client> clients = await _clientService.GetAllAsync();
                IEnumerable<Product> products = await _productService.GetAllAsync();

                Order order = await _ordersService.GetById(Id);

                Client clientName = clients.FirstOrDefault(c => c.Id == order.IdClient);

                var model = new OrderDetailViewModel
                {
                    Id = order.Id,
                    IdClient = order.IdClient,
                    ClientName = clientName.Name,
                    DateOrder = order.DateOrder,
                    Total = order.Total,
                    Status = order.Status,
                    OrderItems = order.OrderItems.Select(item =>
                    {
                        var product = products.FirstOrDefault(p => p.Id == item.IdProduct);

                        return new OrderItemDetailViewModel
                        {
                            Id = item.Id,
                            IdProduct = item.IdProduct,
                            ProductName = product?.Name ?? "Produto não encontrado",
                            Quantity = item.Quantity,
                            UnitPrice = item.UnitPrice
                        };
                    }).ToList()
                };
                return View("DetailOrder", model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateStatus(int orderId, StatusOrder newStatus)
        {
            bool updateResult = await _ordersService.UpdateStatusAsync(orderId, newStatus);
           
            if(updateResult)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Failed to update order status.");
                return View("Index");   
            }
        }

    }
}
