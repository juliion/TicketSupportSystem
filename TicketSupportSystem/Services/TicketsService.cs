using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using System.Linq.Expressions;
using System.Net.Sockets;
using System.Reflection.Metadata;
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

        private async Task<FilteredTicketsDTO> ApplyFilters(TicketsQueryFilters filters)
        {
            var query = _context.Tickets
                .Include(ticket => ticket.User)
                .Include(ticket => ticket.AssignedTo)
                .Include(ticket => ticket.Comments)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(filters.SortColumn) && !string.IsNullOrWhiteSpace(filters.Order))
            {
                if (filters.Order == "asc")
                {
                    query = query.OrderBy(e => EF.Property<object>(e, filters.SortColumn));
                }
                else if (filters.Order == "desc")
                {
                    query = query.OrderByDescending(e => EF.Property<object>(e, filters.SortColumn));
                }
            }

            if(filters.Priority != null)
            {
                query = query.Where(t => t.Priority == filters.Priority);
            }

            if (filters.Status != null)
            {
                query = query.Where(t => t.Status == filters.Status);
            }

            if (filters.UserId != null)
            {
                query = query.Where(t => t.UserId == filters.UserId);
            }

            if (filters.AsignedToId != null)
            {
                query = query.Where(t => t.AssignedToId == filters.AsignedToId);
            }

            var total = await query.CountAsync();
            var take = filters.Take;
            var skip = filters.Skip;

            var tickets = await query
                .Skip(skip)
                .Take(take)
                .ToListAsync();

            var ticketsDTOs = _mapper.Map<List<Ticket>, List<TicketDTO>>(tickets);


            var filteredTickets = new FilteredTicketsDTO
            {
                Take = take,
                Skip = skip,
                Total = total,
                Tickets = ticketsDTOs
            };

            return filteredTickets;
        } 

        public async Task<FilteredTicketsDTO> GetTickets(TicketsQueryFilters filters)
        {
            var filteredTickets = await ApplyFilters(filters);

            return filteredTickets;
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
