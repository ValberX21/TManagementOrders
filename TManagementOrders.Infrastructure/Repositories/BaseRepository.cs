using Dapper;
using TManagementOrders.Domain.Interfaces;
using TManagementOrders.Infrastructure.Data;

namespace TManagementOrders.Infrastructure.Repositories
{
    public class BaseRepository<T> : IBaseInterface<T> where T : class
    {
        protected readonly DapperContext _context;

        public BaseRepository(DapperContext context)
        {
            _context = context;
        }

        public Task<int> AddAsync(T property)
        {
            var sql = $"INSERT INTO [{typeof(T).Name}] VALUES (@Property)";
            using var connection = _context.CreateConnection();
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var sql = $"SELECT * FROM [{typeof(T).Name}]";
            using var connection = _context.CreateConnection();
            return await connection.QueryAsync<T>(sql);
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            var sql = $"SELECT * FROM [{typeof(T).Name}] WHERE Id = @Id";
            using var connection = _context.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<T>(sql, new { Id = id });
        }

        public Task UpdateAsync(T property)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(int id)
        {
            var sql = $"DELETE FROM [{typeof(T).Name}] WHERE Id = @Id";
            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync(sql, new { Id = id });
        }
    }
}
