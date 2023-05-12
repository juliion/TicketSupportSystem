namespace TicketSupportSystem.DTOs.Requests
{
    public class CreateUserDTO
    {
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
