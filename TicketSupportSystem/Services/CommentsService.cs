using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TicketSupportSystem.Common.Exceptions;
using TicketSupportSystem.Data;
using TicketSupportSystem.Data.Entities;
using TicketSupportSystem.Data.Enums;
using TicketSupportSystem.DTOs.Requests;
using TicketSupportSystem.DTOs.Responses;
using TicketSupportSystem.Interfaces;

namespace TicketSupportSystem.Services
{
    public class CommentsService : ICommentsService
    {
        private readonly TicketSupportSystemContext _context;
        private readonly IMapper _mapper;

        public CommentsService(TicketSupportSystemContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Guid> CreateComment(CreateCommentDTO commentDTO)
        {
            var newComment = _mapper.Map<CreateCommentDTO, Comment>(commentDTO);
            newComment.CreatedAt = DateTimeOffset.Now;

            _context.Comments.Add(newComment);
            await _context.SaveChangesAsync();

            return newComment.Id;
        }

        public async Task DeleteComment(Guid id)
        {
            var comment = await _context.Comments.FindAsync(id);
            if (comment == null)
            {
                throw new NotFoundException();
            }
            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
        }

        public async Task<CommentDTO> GetComment(Guid id)
        {
            var comments = await _context.Comments
               .Include(comment => comment.User)
               .Include(comment => comment.Ticket)
               .ToListAsync();

            var comment = comments.FirstOrDefault(t => t.Id == id);
            if (comment == null)
            {
                throw new NotFoundException();
            }

            var commentDto = _mapper.Map<Comment, CommentDTO>(comment);

            return commentDto;
        }

        public async Task<IEnumerable<CommentDTO>> GetCommentsToTicket(Guid ticketId)
        {
            var ticket = await _context.Tickets
            .Include(t => t.Comments)
                .ThenInclude(c => c.User)
            .SingleOrDefaultAsync(t => t.Id == ticketId);

            if (ticket == null)
            {
                throw new NotFoundException();
            }

            var comments = ticket.Comments;

            var commentsDTOs = _mapper.Map<List<Comment>, List<CommentDTO>>(comments);

            return commentsDTOs;
        }

        public async Task UpdateComment(Guid id, UpdateCommentDTO commentDTO)
        {
            var comment = await _context.Comments.FindAsync(id);
            if (comment == null)
            {
                throw new NotFoundException();
            }

            comment.Text = commentDTO.Text;
            comment.UpdatedAt = DateTimeOffset.Now;

            await _context.SaveChangesAsync();
        }
    }
}
