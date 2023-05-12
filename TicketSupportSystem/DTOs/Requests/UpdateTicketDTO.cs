using TicketSupportSystem.Data.Enums;

namespace TicketSupportSystem.DTOs.Requests
{
    public class UpdateTicketDTO
    {
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public Priority Priority { get; set; }
        public Status Status { get; set; }
        public Guid? AssignedToId { get; set; }
    }
}
