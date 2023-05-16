using Microsoft.AspNetCore.Identity;

namespace TicketSupportSystem.Data.Entities
{
    public class User : IdentityUser<Guid>
    {
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public List<Ticket> CreatedTickets { get; set; } = new List<Ticket>();
        public List<Ticket> AssignedTickets { get; set; } = new List<Ticket>();
    }
}
