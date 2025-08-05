using Dapper;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using TManagementOrders.Domain.Entities;
using TManagementOrders.Domain.Enums;
using TManagementOrders.Domain.Interfaces;
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

        public async Task<Order> CreateOrder(Order order)
        {
            using var connection = _context.CreateConnection();
            connection.Open();
            using var transaction = connection.BeginTransaction();

            try
            {
                var sqlInsertOrder = @"
                INSERT INTO [Order] (IdClient, DateOrder, Total, Status)
                VALUES (@IdClient, @DateOrder, @Total, @Status);
                SELECT CAST(SCOPE_IDENTITY() as int);";                

                int generatedId = await connection.ExecuteScalarAsync<int>(sqlInsertOrder, new
                {
                    order.IdClient,
                    order.DateOrder,
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
                order.Id = generatedId;
                transaction.Commit();
                return order;

            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw new Exception("An error occurred while creating the order.", ex);
            }                  
        }

        public async Task<Order?> GetById(int id)
        {

            var sql = @"
                        SELECT 
                            ORD.Id, ORD.IdClient, ORD.DateOrder, ORD.Total, ORD.Status,
                            ORDI.Id, ORDI.IdOrder, ORDI.IdProduct, ORDI.Quantity, ORDI.UnitPrice
                        FROM [Order] ORD
                        LEFT JOIN [OrderItem] ORDI ON ORD.Id = ORDI.IdOrder
                        WHERE ORD.Id = @Id";

            using var connection = _context.CreateConnection();
            var orderDictionary = new Dictionary<int, Order>();

            var result = await connection.QueryAsync<Order, OrderItem, Order>
            (
                sql,
                (order, orderItem) =>
                {
                    if (!orderDictionary.TryGetValue(order.Id, out var currentOrder))
                    {
                        currentOrder = order;
                        currentOrder.OrderItems = new List<OrderItem>();
                        orderDictionary.Add(currentOrder.Id, currentOrder);
                    }

                    if (orderItem != null)
                        currentOrder.OrderItems.Add(orderItem);

                    return currentOrder;
                },
                new { Id = id },
                splitOn: "Id"
            );

            return result.FirstOrDefault();
        }

        public async Task<bool> UpdateStatusAsync(int orderId, StatusOrder newStatus)
        {
            const string sql = @"UPDATE [Order] SET Status = @Status WHERE Id = @Id";

            using var connection = _context.CreateConnection();
            var affectedRows = await connection.ExecuteAsync(sql, new
            {
                Id = orderId,
                Status = newStatus
            });

            return affectedRows > 0;
        }

    }
}
