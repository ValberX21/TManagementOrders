using Microsoft.AspNetCore.Mvc;
using TManagementOrders.Application.Service;
using TManagementOrders.Domain.Entities;

namespace TManagementOrders.API.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductService _productService;

        public ProductController(ProductService productService)
        {

            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string? filter)
        {
            var clients = await _productService.GetAllAsync();

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
            await _productService.DeleteAsync(id);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] Product product)
        {
            if (ModelState.IsValid)
            {
                await _productService.AddAsync(product);
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

            await _productService.UpdateAsync(product);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GotoEditProductPage(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null)
                return NotFound();

            return View("CreateUpdateProduct", product);
        }
    }
}
