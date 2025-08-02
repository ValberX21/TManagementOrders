using TManagementOrders.Domain.Enums;

namespace TManagementOrders.Domain.Entities
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int IdOrder { get; set; }
        public int IdProduct { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
