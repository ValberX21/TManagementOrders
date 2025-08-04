using Dapper;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using TManagementOrders.Domain.Entities;
using TManagementOrders.Infrastructure.Data;
using static Dapper.SqlMapper;

namespace TManagementOrders.Infrastructure.Repositories
{
    public class OrderRepository
    {
        protected readonly DapperContext _context;

        public OrderRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<Orders> CreateOrder(Orders order)
        {
            using var connection = _context.CreateConnection();
            connection.Open();
            using var transaction = connection.BeginTransaction();

            try
            {
                var sqlInsertOrder = @"
                INSERT INTO [Order] (IdClient, DataPedido, Total, Status)
                VALUES (@IdClient, @DataPedido, @Total, @Status);
                SELECT CAST(SCOPE_IDENTITY() as int);";                

                int generatedId = await connection.ExecuteScalarAsync<int>(sqlInsertOrder, new
                {
                    order.IdClient,
                    order.DataPedido,
                    order.Total,
                    order.Status 
                    }, transaction: transaction
                );

                // This block is to insert itens of order
                var sqlInsertOrderItem = @"
                INSERT INTO [OrderItem] (IdOrder, IdProduct, Quantity, UnitPrice)
                VALUES (@IdOrder, @IdProduct, @Quantity, @UnitPrice);";

                foreach (var item in order.OrderItems)
                {
                    item.IdOrder = generatedId;

                    await connection.ExecuteAsync(
                        sqlInsertOrderItem,
                        new
                        {
                            item.IdOrder,
                            item.IdProduct,
                            item.Quantity,
                            item.UnitPrice
                        },
                         transaction: transaction
                    );
                }
                transaction.Commit();
                return order;

            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw new Exception("An error occurred while creating the order.", ex);
            }                  
        }
    }
}
