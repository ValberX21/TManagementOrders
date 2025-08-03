using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TManagementOrders.Domain.Entities;
using TManagementOrders.Domain.Interfaces;

namespace TManagementOrders.Application.Service
{
    public class ProductService
    {
        private readonly IBaseInterface<Product> _productRepository;
        public ProductService(IBaseInterface<Product> baseRepository)
        {
            _productRepository = baseRepository;
        }

        public Task<int> AddAsync(Product product)
        {
            return _productRepository.AddAsync(product);
        }

        public Task<IEnumerable<Product>> GetAllAsync()
        {
            return _productRepository.GetAllAsync();
        }

        public Task<Product?> GetByIdAsync(int id)
        {
            return _productRepository.GetByIdAsync(id);
        }

        public Task DeleteAsync(int id)
        {
            return _productRepository.DeleteAsync(id);
        }

        public Task<int> UpdateAsync(Product property)
        {
            return _productRepository.UpdateAsync(property);
        }
    }
}
