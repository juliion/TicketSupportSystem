using TicketSupportSystem.Data;
using TicketSupportSystem.Data.Entities;
using TicketSupportSystem.DTOs.Requests;
using TicketSupportSystem.DTOs.Responses;
using TicketSupportSystem.Interfaces;
using TicketSupportSystem.Common.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using AutoMapper;
using Microsoft.AspNet.Identity;

namespace TicketSupportSystem.Services
{
    public class UsersService : IUsersService
    {
        private readonly TicketSupportSystemContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IMapper _mapper;

        public UsersService(TicketSupportSystemContext context, IPasswordHasher<User> passwordHasher, IMapper mapper)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _mapper= mapper;
        }

        public async Task<Guid> CreateUser(CreateUserDTO userDTO)
        {
            var user = await _context.Users.FirstOrDefaultAsync(user => user.Email == userDTO.Email);
            if(user != null)
            {
                throw new ConflictException();
            }

            var newUser = _mapper.Map<CreateUserDTO, User>(userDTO);
            newUser.Password = _passwordHasher.HashPassword(null, userDTO.Password);

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

            var userDto = _mapper.Map<User, UserDTO>(user);

            return userDto;
        }

        public async Task<IEnumerable<UserDTO>> GetUsers()
        {
            var users = await _context.Users.ToListAsync();

            var usersDTOs = _mapper.Map<List<User>, List<UserDTO>>(users);

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
