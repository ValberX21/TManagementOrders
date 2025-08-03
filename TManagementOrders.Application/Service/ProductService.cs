using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TManagementOrders.Domain.Entities;
using TManagementOrders.Domain.Interfaces;
using TManagementOrders.Infrastructure.Repositories;

namespace TManagementOrders.Application.Service
{
    public class ProductService
    {
        private readonly IBaseInterface<Product> _baseRepository;
        private readonly ProductRepository _productRepository;

        public ProductService(IBaseInterface<Product> baseRepository, ProductRepository productRepository)
        {
            _baseRepository = baseRepository;
            _productRepository = productRepository;
        }

        public Task<int> AddAsync(Product product)
        {
            return _baseRepository.AddAsync(product);
        }

        public Task<IEnumerable<Product>> GetAllAsync()
        {
            return _baseRepository.GetAllAsync();
        }

        public Task<Product?> GetByIdAsync(int id)
        {
            return _baseRepository.GetByIdAsync(id);
        }

        public Task DeleteAsync(int id)
        {
            return _baseRepository.DeleteAsync(id);
        }

        public Task<int> UpdateAsync(Product property)
        {
            return _baseRepository.UpdateAsync(property);
        }

        public async Task<Product?> GetByNameAsync(string name)
        {
            return await _productRepository.GetByNameAsync(name);
        }
    }
}
