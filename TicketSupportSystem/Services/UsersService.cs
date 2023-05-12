using TicketSupportSystem.Data;
using TicketSupportSystem.Data.Entities;
using TicketSupportSystem.DTOs.Requests;
using TicketSupportSystem.DTOs.Responses;
using TicketSupportSystem.Interfaces;
using TicketSupportSystem.Common.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace TicketSupportSystem.Services
{
    public class UsersService : IUsersService
    {
        private readonly TicketSupportSystemContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;

        public UsersService(TicketSupportSystemContext context, IPasswordHasher<User> passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        public async Task<Guid> CreateUser(CreateUserDTO userDTO)
        {
            var user = await _context.Users.FirstOrDefaultAsync(user => user.Email == userDTO.Email);
            if(user != null)
            {
                throw new ConflictException();
            }

            var newUser = new User
            {
                Id = new Guid(),
                Name = userDTO.Name,
                Email = userDTO.Email,
                Password = _passwordHasher.HashPassword(null, userDTO.Password)
            };
            _context.Add(newUser);
            await _context.SaveChangesAsync();

            return newUser.Id;
        }

        public async Task DeleteUser(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if(user == null)
            {
                throw new NotFoundException();
            }
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task<UserDTO> GetUser(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if(user == null)
            {
                throw new NotFoundException();
            }

            return new UserDTO
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email
            };
        }

        public async Task<IEnumerable<UserDTO>> GetUsers()
        {
            var users = await _context.Users.ToListAsync();

            var usersDTOs = new List<UserDTO>();
            foreach (var user in users)
            {
                usersDTOs.Add(new UserDTO
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email
                });
            }

            return usersDTOs;
        }

        public async Task UpdateUser(Guid id, UpdateUserDTO userDTO)
        {
            var user = await _context.Users.FindAsync(id);
            if(user == null)
            {
                throw new NotFoundException();
            }

            var userWithTheSameEmail = await _context.Users.FirstOrDefaultAsync(user => user.Email == userDTO.Email);
            if(userWithTheSameEmail != null && user.Id != userWithTheSameEmail.Id)
            {
                throw new ConflictException();
            }

            user.Name = userDTO.Name;
            user.Email = userDTO.Email;
            user.Password = _passwordHasher.HashPassword(null, userDTO.Password);

            await _context.SaveChangesAsync();
        }
    }
}
