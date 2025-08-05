using System.Dynamic;

namespace TManagementOrders.Domain.Entities
{
    public class OrderItemDetailViewModel
    {
        public int Id { get; set; }
        public string ProductName { get ; set;}
        public int IdProduct { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Subtotal => Quantity * UnitPrice;
    }
}
