namespace TicketSupportSystem.Data.Entities
{
    public class Attachment
    {
        public Guid Id { get; set; }
        public string FileName { get; set; } = null!;
        public string Path { get; set; } = null!;
        public Guid CommentId { get; set; }
        public Comment Comment { get; set; } = null!;

    }
}
