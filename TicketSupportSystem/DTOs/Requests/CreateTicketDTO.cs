using TicketSupportSystem.Data.Entities;
using TicketSupportSystem.Data.Enums;

namespace TicketSupportSystem.DTOs.Requests
{
    public class CreateTicketDTO
    {
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public Priority Priority { get; set; }
        public Guid UserId { get; set; }
        public Guid? AssignedToId { get; set; }
    }
}
