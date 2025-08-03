using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TManagementOrders.Domain.Entities
{
    public class OrderFilterViewModel
    {
        public string SelectedClientName { get; set; }
        public List<OrderItem> OrderItems { get; set; } = new();
    }
}
