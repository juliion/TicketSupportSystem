using TicketSupportSystem.Data.Enums;

namespace TicketSupportSystem.Data.Entities
{
    public class Ticket
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
        public Guid? AssignedToId { get; set; }
        public User User { get; set; } = null!;
        public User? AssignedTo { get; set; }
        public List<Comment> Comments { get; set; } = new List<Comment>();

    }
}
