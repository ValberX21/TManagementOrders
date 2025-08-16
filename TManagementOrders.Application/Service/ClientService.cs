using TManagementOrders.Domain.Entities;
using TManagementOrders.Domain.Interfaces;
using TManagementOrders.Domain.Interfaces.Repository;
using TManagementOrders.Infrastructure.Repositories;

namespace TManagementOrders.Application.Service
{
    public class ClientService : IBaseInterfaceService<Client>
    {        
        private readonly IBaseInterfaceRepository<Client> _baseInterfaceRepository;
        private readonly ClientRepository _clientRepository;       

        public ClientService(ClientRepository clientRepository, IBaseInterfaceRepository<Client> baseInterfaceSRepository)
        {
            _clientRepository = clientRepository;
            _baseInterfaceRepository = baseInterfaceSRepository;   
        }

        public async Task<int> AddAsync(Client property)
        {
            return await _baseInterfaceRepository.AddAsync(property);  
        }

        public async Task DeleteAsync(int id)
        {
            await _baseInterfaceRepository.DeleteAsync(id); 
        }

        public async Task<IEnumerable<Client>> GetAllAsync()
        {
            return await _baseInterfaceRepository.GetAllAsync();   
        }

        public async Task<Client?> GetByIdAsync(int id)
        {
            return await _baseInterfaceRepository.GetByIdAsync(id);
        }

        public async Task<int> UpdateAsync(Client client)
        {
            int rowUpdate = await _baseInterfaceRepository.UpdateAsync(client);
            return rowUpdate;
        }

        public async Task<Client> GetByNameAsync(string name)
        {
            return await _clientRepository.GetByNameAsync(name);
        }

        public async Task<List<Client>> FilterClient(string? filters)
        {
            return await _clientRepository.FilterClientAsync(filters);
        }
    }
}
