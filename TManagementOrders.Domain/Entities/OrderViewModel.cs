using TManagementOrders.Domain.Enums;

namespace TManagementOrders.Domain.Entities
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public int IdClient { get; set; }
        public string ClientName { get; set; }  
        public DateTime DateOrder { get; set; }
        public decimal Total { get; set; }
        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public string Status { get; set; } 
    }
}
