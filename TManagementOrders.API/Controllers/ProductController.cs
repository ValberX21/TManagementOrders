using Microsoft.AspNetCore.Mvc;
using TManagementOrders.Application.Service;
using TManagementOrders.Domain.Entities;
using TManagementOrders.Domain.Interfaces;

namespace TManagementOrders.API.Controllers
{
    public class ProductController : Controller
    {
        private readonly IBaseInterfaceService<Product> _baseService;
        private readonly ProductService _productService;

        public ProductController(IBaseInterfaceService<Product> baseService,
                                 ProductService productService )
        {
            _baseService = baseService;
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string? filter)
        {
            var clients = await _baseService.GetAllAsync();

            var filteredClients = string.IsNullOrWhiteSpace(filter)
                ? clients.ToList()
                : clients
                    .Where(c =>
                        (!string.IsNullOrEmpty(c.Name) && c.Name.Contains(filter.Trim(), StringComparison.OrdinalIgnoreCase)))
                    .ToList();

            var viewModel = new ProductFilterViewModel
            {
                Filter = filter,
                Products = filteredClients
            };

            return View(viewModel);
        }


        [HttpDelete]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _baseService.DeleteAsync(id);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] Product product)
        {
            if (ModelState.IsValid)
            {
                await _baseService.AddAsync(product);
                return RedirectToAction("Index");
            }
            return View(product);
        }

        [HttpGet]
        public IActionResult GotoCreateProductPage()
        {
            return View("CreateUpdateProduct");
        }

        [HttpPut]
        public async Task<IActionResult> EditProduct(int id, [FromBody] Product product)
        {
            if (id != product.Id)
                return BadRequest();

            await _baseService.UpdateAsync(product);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GotoEditProductPage(int id)
        {
            var product = await _baseService.GetByIdAsync(id);
            if (product == null)
                return NotFound();

            return View("CreateUpdateProduct", product);
        }

        [HttpGet]
        public async Task<IActionResult> SearchProduct(string name)
        {
            var product = await _productService.GetByNameAsync(name);
            
            if (product == null)
                return NotFound();
            return Ok(product);
        }
    }
}
