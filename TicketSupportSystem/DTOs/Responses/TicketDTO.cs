using TicketSupportSystem.Data.Enums;

namespace TicketSupportSystem.DTOs.Responses
{
    public class TicketDTO
    {
        public Guid Id { get; set; } 
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public Priority Priority { get; set; }
        public Status Status { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
        public DateTimeOffset? ClosedAt { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; } = null!;
        public string UserEmail { get; set; } = null!;
        public string? AssignedToName { get; set; }
        public string? AssignedToEmail { get; set; }
    }
}
