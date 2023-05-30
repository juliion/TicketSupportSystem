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
using TicketSupportSystem.Services;
using TicketSupportSystem.DTOs.Responses;
using System.Xml.Linq;
using FluentValidation;
using TicketSupportSystem.Validators;

namespace TicketSupportSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TicketsController : ControllerBase
    {
        private readonly ITicketsService _ticketsService;
        private readonly UserManager<User> _userManager;
        private readonly ICommentsService _commentsService;
        private IValidator<CreateCommentDTO> _createCommentValidator;
        private IValidator<CreateTicketDTO> _createTicketValidator;
        private IValidator<UpdateTicketDTO> _updateTicketValidator;

        public TicketsController(ITicketsService ticketsService, UserManager<User> userManager, ICommentsService commentsService, IValidator<CreateTicketDTO> createTicketValidator, IValidator<UpdateTicketDTO> updateTicketValidator, IValidator<CreateCommentDTO> createCommentValidator)
        {
            _ticketsService = ticketsService;
            _userManager = userManager;
            _commentsService = commentsService;
            _createTicketValidator = createTicketValidator;
            _createCommentValidator = createCommentValidator;
            _updateTicketValidator = updateTicketValidator;
        }

        [HttpGet("GetTickets")]
        [Authorize(Roles = "Admin,SupportAgent")]
        public async Task<IActionResult> GetTickets([FromQuery]TicketsQueryFilters filters)
        {
            var tickets = await _ticketsService.GetTickets(filters);

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
            var validationRes = _createTicketValidator.Validate(ticketDTO);
            if (!validationRes.IsValid)
                return BadRequest(validationRes);

            var ticketId = await _ticketsService.CreateTicket(ticketDTO);

            return Ok(ticketId);
        }

        [HttpPut("UpdateTicket/{id}")]
        public async Task<IActionResult> UpdateTicket(Guid id, UpdateTicketDTO ticketDTO)
        {
            var validationRes = _updateTicketValidator.Validate(ticketDTO);
            if (!validationRes.IsValid)
                return BadRequest(validationRes);
            try
            {
                var currentUserEmail = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                var user = await _userManager.FindByEmailAsync(currentUserEmail);
                var isCustomer = await _userManager.IsInRoleAsync(user, "Customer");

                if (isCustomer && user.CreatedTickets.FirstOrDefault(t => t.Id == id) == null)
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
            catch (ForbiddenException)
            {
                return Forbid();
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


        [HttpPost("Comments/AddCommentToTicket")]

        public async Task<IActionResult> CreateComment(CreateCommentDTO commentDTO)
        {
            var validationRes = _createCommentValidator.Validate(commentDTO);
            if (!validationRes.IsValid)
                return BadRequest(validationRes);

            var commentId = await _commentsService.CreateComment(commentDTO);

            return Ok(commentId);
        }

        [HttpGet("Comments/GetCommentsToTicket/{ticketId}")]
        public async Task<IActionResult> GetCommentsToTicket(Guid ticketId)
        {

            var currentUserEmail = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var user = await _userManager.FindByEmailAsync(currentUserEmail);
            var isCustomer = await _userManager.IsInRoleAsync(user, "Customer");


            if (isCustomer && user.CreatedTickets.FirstOrDefault(t => t.Id == ticketId) == null)
            {
                return Forbid();
            }

            try
            {
               var comments  = await _commentsService.GetCommentsToTicket(ticketId);
               return Ok(comments);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }

        }
    }
}
