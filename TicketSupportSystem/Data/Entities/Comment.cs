namespace TicketSupportSystem.Data.Entities
{
    public class Comment
    {
        public Guid Id { get; set; }
        public string Text { get; set; } = null!;
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
        public Guid UserId { get; set; }
        public Guid TicketId { get; set; }
        public User User { get; set; } = null!;
        public Ticket Ticket { get; set; } = null!;

    }
}
