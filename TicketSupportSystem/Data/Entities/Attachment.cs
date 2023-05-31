namespace TicketSupportSystem.Data.Entities
{
    public class Attachment
    {
        public Guid Id { get; set; }
        public string FileName { get; set; } = null!;
        public string ContentType { get; set; } = null!;
        public int Size { get; set; }
        public byte[] Data { get; set; } = null!;
        public Guid CommentId { get; set; }
        public Comment Comment { get; set; } = null!;

    }
}
