using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Net.Sockets;
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
        private readonly IMapper _mapper;
        public TicketsService(TicketSupportSystemContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Guid> CreateTicket(CreateTicketDTO ticketDTO)
        {
            var newTicket = _mapper.Map<CreateTicketDTO, Ticket>(ticketDTO);
            newTicket.CreatedAt = DateTimeOffset.Now;
            newTicket.Status = Status.Open;

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
                .Include(ticket => ticket.Comments)
                .ToListAsync();
            var ticket = tickets.FirstOrDefault(t => t.Id == id);
            if (ticket == null)
            {
                throw new NotFoundException();
            }

            var ticketDto = _mapper.Map<Ticket, TicketDTO>(ticket);

            return ticketDto;
        }

        public async Task<IEnumerable<TicketDTO>> GetTickets()
        {
            var tickets = await _context.Tickets
                .Include(ticket => ticket.User)
                .Include(ticket => ticket.AssignedTo)
                .Include(ticket => ticket.Comments)
                .ToListAsync();

            var ticketsDTOs = _mapper.Map<List<Ticket>, List<TicketDTO>>(tickets);

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
