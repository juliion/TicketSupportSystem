using Microsoft.AspNetCore.Identity;
using TicketSupportSystem.Data.Entities;

namespace TicketSupportSystem.Common.Helpers
{
    public class PasswordHasher<TUser> : IPasswordHasher<TUser> where TUser : class
    {
        public string HashPassword(TUser user, string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public PasswordVerificationResult VerifyHashedPassword(TUser user, string hashedPassword, string providedPassword)
        {
            throw new NotImplementedException();
        }
    }
}
