using System.Linq.Expressions;
using TicketSupportSystem.Data.Entities;
using TicketSupportSystem.DTOs.Requests;
using TicketSupportSystem.DTOs.Responses;

namespace TicketSupportSystem.Interfaces
{
    public interface ITicketsService
    {
        public Task<Guid> CreateTicket(CreateTicketDTO ticketDTO);
        public Task UpdateTicket(Guid id, UpdateTicketDTO ticketDTO);
        public Task DeleteTicket(Guid id);
        public Task<FilteredTicketsDTO> GetTickets(TicketsQueryFilters filters);
        public Task<TicketDetailsDTO> GetTicket(Guid id);
    }
}
