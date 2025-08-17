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

        public async Task DecreaseStockAsync(int productId, int quantity)
        {
            var sql = @"
                        UPDATE Product
                        SET Quantity = Quantity - @Quantity
                        WHERE Id = @ProductId AND Quantity >= @Quantity";

            using var connection = _context.CreateConnection();
            var rowsAffected = await connection.ExecuteAsync(sql, new
            {
                ProductId = productId,
                Quantity = quantity
            });

            if (rowsAffected == 0)
                throw new Exception($"Not enough quantity in stock for Product ID {productId}");
        }

        public async Task<IEnumerable<Product>> Filter(string? filter)
        {
            var sql = "SELECT * FROM Product";
            if (!string.IsNullOrWhiteSpace(filter))
            {
                sql += " WHERE Name LIKE @Filter";
            }
            using var connection = _context.CreateConnection();
            return await connection.QueryAsync<Product>(sql, new { Filter = $"%{filter}%" });

        }
    }
}
