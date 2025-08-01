namespace TManagementOrders.Domain.Interfaces
{
    public interface IBaseInterface<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task<int> AddAsync(T property);
        Task UpdateAsync(T property);
        Task DeleteAsync(int id);
    }
}
