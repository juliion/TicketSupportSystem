namespace TicketSupportSystem.DTOs.Responses
{
    public class AttachmentDTO
    {
        public Guid Id { get; set; }
        public string FileName { get; set; } = null!;
        public string Path { get; set; } = null!;
    }
}
