using TManagementOrders.Domain.Enums;

namespace TManagementOrders.Domain.Entities
{
    public class Orders
    {
        public int Id { get; set; }
        public int IdClient { get; set; }
        public DateTime DataPedido { get; set; }
        public decimal Total { get; set; }
        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public StatusOrder Status { get; set; }
    }
}
