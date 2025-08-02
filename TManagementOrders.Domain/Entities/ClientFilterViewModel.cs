namespace TManagementOrders.Domain.Entities
{
    public class ClientFilterViewModel
    {
        public string? Filter { get; set; }
        public List<Client> Clients { get; set; } = new();
    }
}
