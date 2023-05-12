using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketSupportSystem.Common.Exceptions;
using TicketSupportSystem.Data.Entities;
using TicketSupportSystem.DTOs.Requests;
using TicketSupportSystem.Interfaces;

namespace TicketSupportSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;

        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpGet("GetUsers")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _usersService.GetUsers();

            return Ok(users);
        }

        [HttpGet("UserDetails/{id}")]
        public async Task<IActionResult> UserDetails(Guid id)
        {
            try
            {
                var user = await _usersService.GetUser(id);

                return Ok(user);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser(CreateUserDTO userDTO)
        {
            try
            {
                var userId = await _usersService.CreateUser(userDTO);

                return Ok(userId);
            }
            catch (ConflictException)
            {
                return Conflict();
            }
        }

        [HttpPut("UpdateUser/{id}")]
        public async Task<IActionResult> UpdateUser(Guid id, UpdateUserDTO userDTO)
        {
            try
            {
                await _usersService.UpdateUser(id, userDTO);

                return Ok();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch (ConflictException)
            {
                return Conflict();
            }
        }

        [HttpDelete("DeleteUser/{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            try
            {
                await _usersService.DeleteUser(id);

                return Ok();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }
    }
}
