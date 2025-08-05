using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TManagementOrders.Domain.Enums;

namespace TManagementOrders.Domain.Entities
{
    public class OrderFilterViewModel
    {
        
        public string? SelectedClientName { get; set; }
        public StatusOrder? StatusFilter { get; set; } 
        public IEnumerable<OrderViewModel> OrderItems { get; set; }
    }
}
