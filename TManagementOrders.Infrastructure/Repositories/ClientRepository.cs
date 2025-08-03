using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TManagementOrders.Domain.Entities;
using TManagementOrders.Domain.Interfaces;
using TManagementOrders.Infrastructure.Data;

namespace TManagementOrders.Infrastructure.Repositories
{
    public class ClientRepository
    {
        protected readonly DapperContext _context;

        public ClientRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<Client> GetByNameAsync(string name)
        {
            var sql = "SELECT * FROM Client WHERE Name LIKE @Name";
            using var connection = _context.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<Client>(sql, new { Name = $"%{name}%" });
        }

    }
}
