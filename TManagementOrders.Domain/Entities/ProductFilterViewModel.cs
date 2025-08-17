namespace TManagementOrders.Domain.Entities
{
    public class ProductFilterViewModel
    {
        public string? Filter { get; set; }
        public IEnumerable<Product> Products { get; set; } = [];
    }
}
