namespace TManagementOrders.Domain.Entities
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int Telephone { get; set; }
        public DateTime DateRegister { get; set; }
    }
}
