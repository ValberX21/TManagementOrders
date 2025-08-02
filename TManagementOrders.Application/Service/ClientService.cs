using TManagementOrders.Domain.Entities;
using TManagementOrders.Domain.Interfaces;

namespace TManagementOrders.Application.Service
{
    public class ClientService 
    {
        private readonly IBaseInterface<Client> _clientRepository;

        public ClientService(IBaseInterface<Client> baseRepository)
        {
            _clientRepository = baseRepository;
        }

        public Task<int> AddAsync(Client client)
        {
            return _clientRepository.AddAsync(client);
        }

        public Task DeleteAsync(int id)
        {
            return _clientRepository.DeleteAsync(id);
        }

        public Task<IEnumerable<Client>> GetAllAsync()
        {
            return _clientRepository.GetAllAsync();
        }

        public async Task<Client?> GetByIdAsync(int id)
        {
            return await _clientRepository.GetByIdAsync(id);
        }

        public async Task<int> UpdateAsync(Client entity)
        {
            int rowUpdate = await _clientRepository.UpdateAsync(entity);
            return rowUpdate;
        }
    }
}
