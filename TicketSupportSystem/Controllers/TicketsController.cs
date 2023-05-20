using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketSupportSystem.Common.Exceptions;
using TicketSupportSystem.Data.Entities;
using TicketSupportSystem.DTOs.Requests;
using TicketSupportSystem.Interfaces;
using System.Net.Sockets;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace TicketSupportSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TicketsController : ControllerBase
    {
        private readonly ITicketsService _ticketsService;
        private readonly UserManager<User> _userManager;

        public TicketsController(ITicketsService ticketsService, UserManager<User> userManager)
        {
            _ticketsService = ticketsService;
            _userManager = userManager;
        }

        [HttpGet("GetTickets")]
        [Authorize(Roles = "Admin,SupportAgent")]
        public async Task<IActionResult> GetTickets()
        {
            var tickets = await _ticketsService.GetTickets();

            return Ok(tickets);
        }

        [HttpGet("TicketDetails/{id}")]
        public async Task<IActionResult> TicketDetails(Guid id)
        {
            try
            {
                var ticket = await _ticketsService.GetTicket(id);

                var currentUserEmail = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                var user = await _userManager.FindByEmailAsync(currentUserEmail);
                var isCustomer = await _userManager.IsInRoleAsync(user, "Customer");

                if (isCustomer && ticket.UserId != user.Id)
                {
                    return Forbid();
                }

                return Ok(ticket);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost("CreateTicket")]
        public async Task<IActionResult> CreateTicket(CreateTicketDTO ticketDTO)
        {
            var ticketId = await _ticketsService.CreateTicket(ticketDTO);

            return Ok(ticketId);
        }

        [HttpPut("UpdateTicket/{id}")]
        public async Task<IActionResult> UpdateTicket(Guid id, UpdateTicketDTO ticketDTO)
        {
            try
            {
                var currentUserEmail = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                var user = await _userManager.FindByEmailAsync(currentUserEmail);
                var isCustomer = await _userManager.IsInRoleAsync(user, "Customer");

                if (isCustomer && ticketDTO.UserId != user.Id)
                {
                    return Forbid();
                }

                await _ticketsService.UpdateTicket(id, ticketDTO);

                return Ok();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        [HttpDelete("DeleteTicket/{id}")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> DeleteTicket(Guid id)
        {
            try
            {
                await _ticketsService.DeleteTicket(id);

                return Ok();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }
    }
}
