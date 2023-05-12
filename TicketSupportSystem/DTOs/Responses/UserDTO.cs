namespace TicketSupportSystem.DTOs.Responses
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
    }
}
