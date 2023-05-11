namespace TicketSupportSystem.Data.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public List<Ticket> CreatedTickets { get; set; } = new List<Ticket>();
        public List<Ticket> AssignedTickets { get; set; } = new List<Ticket>();
    }
}
