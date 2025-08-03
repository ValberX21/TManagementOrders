using Dapper;
using TManagementOrders.Domain.Entities;
using TManagementOrders.Infrastructure.Data;

namespace TManagementOrders.Infrastructure.Repositories
{
    public class ProductRepository
    {
        protected readonly DapperContext _context;

        public ProductRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<Product> GetByNameAsync(string name)
        {
            var sql = "SELECT * FROM Product WHERE Name LIKE @Name";
            using var connection = _context.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<Product>(sql, new { Name = $"%{name}%" });
        }
    }
}
