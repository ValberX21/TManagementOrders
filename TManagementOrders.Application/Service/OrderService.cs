using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TManagementOrders.Domain.Entities;
using TManagementOrders.Domain.Interfaces;
using TManagementOrders.Infrastructure.Repositories;

namespace TManagementOrders.Application.Service
{
    public class OrderService
    {
        private readonly OrderRepository _orderRepository;

        public OrderService(OrderRepository orderRepository)
        {
                _orderRepository = orderRepository;
        }
        
        public async Task<Orders> AddAsync(Orders order)
        {
            return await _orderRepository.CreateOrder(order);
        }
    }
}
