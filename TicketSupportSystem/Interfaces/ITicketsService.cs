using TicketSupportSystem.DTOs.Requests;
using TicketSupportSystem.DTOs.Responses;

namespace TicketSupportSystem.Interfaces
{
    public interface ITicketsService
    {
        public Task<Guid> CreateTicket(CreateTicketDTO ticketDTO);
        public Task UpdateTicket(Guid id, UpdateTicketDTO ticketDTO);
        public Task DeleteTicket(Guid id);
        public Task<IEnumerable<TicketDTO>> GetTickets();
        public Task<TicketDTO> GetTicket(Guid id);
    }
}
