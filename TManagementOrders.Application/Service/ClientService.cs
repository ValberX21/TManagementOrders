using TManagementOrders.Domain.Entities;
using TManagementOrders.Domain.Interfaces;
using TManagementOrders.Infrastructure.Repositories;

namespace TManagementOrders.Application.Service
{
    public class ClientService 
    {
        private readonly IBaseInterface<Client> _baseRepository;
        private readonly ClientRepository _clientRepository;

        public ClientService(IBaseInterface<Client> baseRepository, ClientRepository clientRepository)
        {           
            _baseRepository = baseRepository;
            _clientRepository = clientRepository;
        }

        public async Task<int> AddAsync(Client client)
        {
            return await _baseRepository.AddAsync(client);
        }

        public Task DeleteAsync(int id)
        {
            return _baseRepository.DeleteAsync(id);
        }

        public Task<IEnumerable<Client>> GetAllAsync()
        {
            return _baseRepository.GetAllAsync();
        }

        public async Task<Client?> GetByIdAsync(int id)
        {
            return await _baseRepository.GetByIdAsync(id);
        }

        public async Task<int> UpdateAsync(Client entity)
        {
            int rowUpdate = await _baseRepository.UpdateAsync(entity);
            return rowUpdate;
        }

        public async Task<Client?> GetByNameAsync(string name)
        {
            return await _clientRepository.GetByNameAsync(name);
        }
    }
}
