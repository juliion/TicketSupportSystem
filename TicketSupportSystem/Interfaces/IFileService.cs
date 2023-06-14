using System.Net.Mail;
using TicketSupportSystem.DTOs.Responses;

namespace TicketSupportSystem.Interfaces
{
    public interface IFileService
    {
        public Task<AttachmentDTO> SaveAttachment(IFormFile file, Guid commentId);
        public Task<List<AttachmentDTO>> GetAttachmentsToComment(Guid commentId);
        public Task DeleteAttachment(Guid id);
    }
}
