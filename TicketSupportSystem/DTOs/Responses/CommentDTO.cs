using TicketSupportSystem.Data.Entities;

namespace TicketSupportSystem.DTOs.Responses
{
    public class CommentDTO
    {
        public Guid Id { get; set; }
        public string Text { get; set; } = null!;
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
        public Guid UserId { get; set; }
        public string UserEmail { get; set; } = null!;
        public Guid TicketId { get; set; }
        public string TicketTitle { get; set; } = null!;
    }
}
