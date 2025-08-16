using System;
using TManagementOrders.Domain.Enums;
namespace TManagementOrders.Domain.Entities
{
    public class OrderDetailViewModel
    {
        public int Id { get; set; }
        public int IdClient { get; set; }
        public string ClientName { get; set; } = string.Empty;
        public DateTime DateOrder { get; set; } = DateTime.Now;
        public decimal Total { get; set; } = 0;
        public StatusOrder Status { get; set; } = StatusOrder.NEW;
        public List<OrderItemDetailViewModel> OrderItems { get; set; } = [];
    }
}
