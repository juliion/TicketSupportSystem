using TicketSupportSystem.Data;
using TicketSupportSystem.Data.Entities;
using TicketSupportSystem.DTOs.Requests;
using TicketSupportSystem.DTOs.Responses;
using TicketSupportSystem.Interfaces;
using System.Security.Cryptography;
using System.Text;
using TicketSupportSystem.Common.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace TicketSupportSystem.Services
{
    public class UsersService : IUsersService
    {
        private readonly TicketSupportSystemContext _context;

        public UsersService(TicketSupportSystemContext context)
        {
            _context = context;
        }

        public async Task<Guid> CreateUser(CreateUserDTO userDTO)
        {
            var user = await _context.Users.FirstOrDefaultAsync(user => user.Email == userDTO.Email);
            if(user != null)
            {
                throw new ConflictException();
            }

            var md5 = MD5.Create();
            var passwordHash = md5.ComputeHash(Encoding.UTF8.GetBytes(userDTO.Password));

            var newUser = new User
            {
                Id = new Guid(),
                Name = userDTO.Name,
                Email = userDTO.Email,
                Password = Convert.ToBase64String(passwordHash)
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

            var md5 = MD5.Create();
            var passwordHash = md5.ComputeHash(Encoding.UTF8.GetBytes(userDTO.Password));

            user.Name = userDTO.Name;
            user.Email = userDTO.Email;
            user.Password = Convert.ToBase64String(passwordHash);

            await _context.SaveChangesAsync();
        }
    }
}
