using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using TicketSupportSystem.Common.Exceptions;
using TicketSupportSystem.Data;
using TicketSupportSystem.Data.Entities;
using TicketSupportSystem.Data.Enums;
using TicketSupportSystem.DTOs.Requests;
using TicketSupportSystem.DTOs.Responses;
using TicketSupportSystem.Interfaces;

namespace TicketSupportSystem.Services
{
    public class TicketsService : ITicketsService
    {
        private readonly TicketSupportSystemContext _context;

        public TicketsService(TicketSupportSystemContext context)
        {
            _context = context;
        }
        public async Task<Guid> CreateTicket(CreateTicketDTO ticketDTO)
        {
            var newTicket = new Ticket
            {
                Id = new Guid(),
                Title = ticketDTO.Title,
                Description = ticketDTO.Description,
                Priority = ticketDTO.Priority,
                Status = Status.Open,
                CreatedAt = DateTimeOffset.Now,
                UserId = ticketDTO.UserId
        };
            _context.Tickets.Add(newTicket);
            await _context.SaveChangesAsync();

            return newTicket.Id;
        }

        public async Task DeleteTicket(Guid id)
        {
            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket == null)
            {
                throw new NotFoundException();
            }
            _context.Tickets.Remove(ticket);
            await _context.SaveChangesAsync();
        }

        public async Task<TicketDTO> GetTicket(Guid id)
        {
            var tickets = await _context.Tickets
                .Include(ticket => ticket.User)
                .Include(ticket => ticket.AssignedTo)
                .ToListAsync();
            var ticket = tickets.FirstOrDefault(t => t.Id == id);
            if (ticket == null)
            {
                throw new NotFoundException();
            }

            return new TicketDTO
            {
                Id = ticket.Id,
                Title = ticket.Title,
                Description = ticket.Description,
                Priority = ticket.Priority,
                Status = ticket.Status,
                CreatedAt = ticket.CreatedAt,
                UpdatedAt = ticket.UpdatedAt,
                ClosedAt = ticket.ClosedAt,
                UserName = ticket.User.Name,
                UserEmail = ticket.User.Email,
                AssignedToEmail = ticket.AssignedTo?.Name,
                AssignedToName = ticket.AssignedTo?.Email
            };
        }

        public async Task<IEnumerable<TicketDTO>> GetTickets()
        {
            var tickets = await _context.Tickets
                .Include(ticket => ticket.User)
                .Include(ticket => ticket.AssignedTo)
                .ToListAsync();

            var ticketsDTOs = new List<TicketDTO>();
            foreach (var ticket in tickets)
            {
                ticketsDTOs.Add(new TicketDTO
                {
                    Id = ticket.Id,
                    Title = ticket.Title,
                    Description = ticket.Description,
                    Priority = ticket.Priority,
                    Status = ticket.Status,
                    CreatedAt = ticket.CreatedAt,
                    UpdatedAt = ticket.UpdatedAt,
                    ClosedAt = ticket.ClosedAt,
                    UserName = ticket.User.Name,
                    UserEmail = ticket.User.Email,
                    AssignedToEmail = ticket.AssignedTo?.Name,
                    AssignedToName = ticket.AssignedTo?.Email
                });
            }

            return ticketsDTOs;
        }

        public async Task UpdateTicket(Guid id, UpdateTicketDTO ticketDTO)
        {
            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket == null)
            {
                throw new NotFoundException();
            }

            ticket.Title = ticketDTO.Title;
            ticket.Description = ticketDTO.Description;
            ticket.Priority = ticketDTO.Priority;
            ticket.Status = ticketDTO.Status;
            ticket.UpdatedAt = DateTimeOffset.Now;
            ticket.AssignedToId = ticketDTO.AssignedToId;

            await _context.SaveChangesAsync();
        }
    }
}
