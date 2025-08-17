namespace TManagementOrders.Domain.Interfaces
{
    public interface IBaseInterfaceService<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task<int> AddAsync(T property);
        Task<int> UpdateAsync(T property);
        Task DeleteAsync(int id);
        Task<IEnumerable<T>> Filter(string? filter);
    }
}
