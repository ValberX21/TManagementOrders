using System;
using TManagementOrders.Domain.Enums;
namespace TManagementOrders.Domain.Entities
{
    public class OrderDetailViewModel
    {
        public int Id { get; set; }
        public int IdClient { get; set; }
        public string ClientName { get; set; } = "";
        public DateTime DateOrder { get; set; }
        public decimal Total { get; set; }
        public StatusOrder Status { get; set; }
        public List<OrderItemDetailViewModel> OrderItems { get; set; } = new List<OrderItemDetailViewModel>();
    }
}
