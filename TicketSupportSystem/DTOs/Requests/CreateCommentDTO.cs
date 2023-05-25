using TicketSupportSystem.Data.Entities;

namespace TicketSupportSystem.DTOs.Requests
{
    public class CreateCommentDTO
    {
        public string Text { get; set; } = null!;
        public Guid UserId { get; set; }
        public Guid TicketId { get; set; }
    }
}
