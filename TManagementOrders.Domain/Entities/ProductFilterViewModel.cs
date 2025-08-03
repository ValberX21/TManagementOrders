namespace TManagementOrders.Domain.Entities
{
    public class ProductFilterViewModel
    {
        public string? Filter { get; set; }
        public List<Product> Products { get; set; } = new();
    }
}
