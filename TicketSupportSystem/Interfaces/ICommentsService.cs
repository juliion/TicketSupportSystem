using TicketSupportSystem.Data;
using TicketSupportSystem.DTOs.Requests;
using TicketSupportSystem.DTOs.Responses;

namespace TicketSupportSystem.Interfaces
{
    public interface ICommentsService
    {
        public Task<Guid> CreateComment(CreateCommentDTO commentDTO);
        public Task UpdateComment(Guid id, UpdateCommentDTO commentDTO);
        public Task DeleteComment(Guid id);
        public Task<IEnumerable<CommentDTO>> GetCommentsToTicket(Guid ticketId);
        public Task<CommentDTO> GetComment(Guid id);
    }
}
