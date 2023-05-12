using TicketSupportSystem.DTOs.Requests;
using TicketSupportSystem.DTOs.Responses;

namespace TicketSupportSystem.Interfaces
{
    public interface IUsersService
    {
        public Task<Guid> CreateUser(CreateUserDTO userDTO);
        public Task UpdateUser(Guid id, UpdateUserDTO userDTO);
        public Task<UserDTO> GetUser(Guid id);
        public Task<IEnumerable<UserDTO>> GetUsers();
        public Task DeleteUser(Guid id);
    }
}
