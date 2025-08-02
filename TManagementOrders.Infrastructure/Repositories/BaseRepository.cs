using Dapper;
using TManagementOrders.Domain.Interfaces;
using TManagementOrders.Infrastructure.Data;
using static Dapper.SqlMapper;

namespace TManagementOrders.Infrastructure.Repositories
{
    public class BaseRepository<T> : IBaseInterface<T> where T : class
    {
        protected readonly DapperContext _context;

        public BaseRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<int> AddAsync(T entity)
        {
            var properties = typeof(T)
                                    .GetProperties()
                                    .Where(p => p.Name.ToLower() != "id") 
                                    .ToList();

            var columnNames = string.Join(", ", properties.Select(p => $"[{p.Name}]"));

            var parameterNames = string.Join(", ", properties.Select(p => $"@{p.Name}"));

            var sql = $"INSERT INTO [{typeof(T).Name}] ({columnNames}) VALUES ({parameterNames})";

            using var connection = _context.CreateConnection();
            return await connection.ExecuteAsync(sql, entity);
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

        public async Task<int> UpdateAsync(T entity)
        {
            var type = typeof(T);
            var tableName = $"[{type.Name}]";
            var properties = type.GetProperties()
                                 .Where(p => p.Name != "Id")
                                 .ToList();

            var setClause = string.Join(", ", properties.Select(p => $"{p.Name} = @{p.Name}"));
            var sql = $"UPDATE {tableName} SET {setClause} WHERE Id = @Id";

            using var connection = _context.CreateConnection();
            return await connection.ExecuteAsync(sql, entity);
        }

        public async Task DeleteAsync(int id)
        {
            var sql = $"DELETE FROM [{typeof(T).Name}] WHERE Id = @Id";
            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync(sql, new { Id = id });
        }
    }
}
