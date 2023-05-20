using TicketSupportSystem.Data.Entities;

namespace TicketSupportSystem.Interfaces
{
    public interface IJwtTokenService
    {
        public Task<string> GenerateJwtToken(User user);
    }
}
