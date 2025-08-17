namespace TManagementOrders.Domain.Entities
{
    public class ClientFilterViewModel
    {
        public string? Filter { get; set; }
        public IEnumerable<Client> Clients { get; set; } = [];
    }
}
