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
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Client>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Client?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Client property)
        {
            throw new NotImplementedException();
        }
    }
}
