using TManagementOrders.Domain.Entities;
using TManagementOrders.Domain.Enums;
using TManagementOrders.Domain.Interfaces;
using TManagementOrders.Domain.Interfaces.Repository;
using TManagementOrders.Infrastructure.Repositories;

namespace TManagementOrders.Application.Service
{
    public class OrderService 
    {
        private readonly OrderRepository _orderRepository;
        private readonly IBaseInterfaceRepository<Product> _productInterface;
        private readonly ProductRepository _productRepository;

        public OrderService(OrderRepository orderRepository,
                            IBaseInterfaceRepository<Product> productInterface,
                            ProductRepository productRepository)
        {
            _productInterface = productInterface;
            _orderRepository = orderRepository;
            _productRepository = productRepository;
        }        
          
        public async Task<Order> AddAsync(Order order)
        {
            foreach (var item in order.OrderItems)
            {
                var product = await _productInterface.GetByIdAsync(item.IdProduct);

                if (product == null)
                    throw new Exception($"Produto {item.Id} não encontrado.");

                if (product.Quantity < item.Quantity)
                    throw new Exception($"Infelizmente não temos a quantidade requisitada do produto {product.Name}. \n Quantidade disponivel: {product.Quantity}");
            }

            var createdOrder = await _orderRepository.CreateOrder(order);

            foreach (var item in createdOrder.OrderItems)
            {
                await _productRepository.DecreaseStockAsync(item.IdProduct, item.Quantity);
            }

            return createdOrder;
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            return await _orderRepository.GetAllAsync();
        }

        public async Task<Order> GetById(int id)
        {
            return await _orderRepository.GetById(id);
        }

        public async Task<bool> UpdateStatusAsync(int orderId, StatusOrder newStatus)
        {
            return await _orderRepository.UpdateStatusAsync(orderId, newStatus);
        }
    }
}
