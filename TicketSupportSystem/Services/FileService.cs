using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TicketSupportSystem.Common.Exceptions;
using TicketSupportSystem.Data;
using TicketSupportSystem.Data.Entities;
using TicketSupportSystem.DTOs.Responses;
using TicketSupportSystem.Interfaces;

namespace TicketSupportSystem.Services
{
    public class FileService : IFileService
    {
        private readonly string _attachmentsDir;
        private readonly TicketSupportSystemContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IMapper _mapper;

        public FileService(IWebHostEnvironment env, TicketSupportSystemContext context, IMapper mapper)
        {
            _env = env;
            _attachmentsDir = "attachments";
            _context = context;
            _mapper = mapper;
        }
        public async Task<AttachmentDTO> SaveAttachment(IFormFile file, Guid commentId)
        {
            if (file == null || file.Length == 0)
            {
                throw new ArgumentException("File not selected");
            }

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var fullPath = Path.Combine(_env.WebRootPath, _attachmentsDir, fileName);

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var filePath = Path.Combine(_attachmentsDir, fileName);

            var attachment = new Attachment
            {
                FileName = fileName,
                Path = filePath,
                CommentId = commentId
            };

            _context.Attachments.Add(attachment);
            await _context.SaveChangesAsync();

            var attachmentDTO = _mapper.Map<Attachment, AttachmentDTO>(attachment);

            return attachmentDTO;
        }

        public async Task<List<AttachmentDTO>> GetAttachmentsToComment(Guid commentId)
        {
            var comment = await _context.Comments
                .Include(c => c.Attachments)
                .SingleOrDefaultAsync(c => c.Id == commentId);
            if (comment == null)
            {
                throw new NotFoundException();
            }

            var attachments = comment.Attachments.ToList();

            var attachmentsDTOs = _mapper.Map<List<Attachment>, List<AttachmentDTO>>(attachments);

            return attachmentsDTOs;
        }

        public async Task DeleteAttachment(Guid id)
        {
            var attachment = await _context.Attachments
                .FirstOrDefaultAsync(f => f.Id == id);

            if (attachment == null)
            {
                throw new NotFoundException("File not found");
            }

            var fullPath = Path.Combine(_env.WebRootPath, attachment.Path);

            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }

            _context.Attachments.Remove(attachment);
            await _context.SaveChangesAsync();
        }
    }
}
