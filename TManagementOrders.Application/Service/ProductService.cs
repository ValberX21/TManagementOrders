using TManagementOrders.Domain.Entities;
using TManagementOrders.Domain.Interfaces;
using TManagementOrders.Domain.Interfaces.Repository;
using TManagementOrders.Infrastructure.Repositories;

namespace TManagementOrders.Application.Service
{
    public class ProductService: IBaseInterfaceService<Product> 
    {
        private readonly IBaseInterfaceRepository<Product> _baseRepository;
        private readonly ProductRepository _productRepository;

        public ProductService(ProductRepository productRepository,
                              IBaseInterfaceRepository<Product> baseRepository)
        {
            _baseRepository = baseRepository;
            _productRepository = productRepository;
        }

        public async Task<int> AddAsync(Product property)
        {
            return await _baseRepository.AddAsync(property);
        }

        public async Task DeleteAsync(int id)
        {
            await _baseRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _baseRepository.GetAllAsync();
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _baseRepository.GetByIdAsync(id);
        }

        public async Task<int> UpdateAsync(Product property)
        {
            int rowUpdate = await _baseRepository.UpdateAsync(property);
            return rowUpdate;
        }

        public async Task<Product> GetByNameAsync(string name)
        {
            return await _productRepository.GetByNameAsync(name);   
        }
    }
}
