namespace TManagementOrders.Domain.Interfaces.Repository
{
    public interface IBaseInterfaceRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task<int> AddAsync(T property);
        Task<int> UpdateAsync(T property);
        Task DeleteAsync(int id);
    }
}
