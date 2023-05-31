using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketSupportSystem.Common.Exceptions;
using TicketSupportSystem.Interfaces;

namespace TicketSupportSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttachmentsController : ControllerBase
    {
        private readonly IFileService _fileService;

        public AttachmentsController(IFileService fileService)
        {
            _fileService = fileService;
        }
        [HttpPost("Comments/UploadAttachment/{commentId}")]
        public async Task<IActionResult> UploadAttachment(Guid commentId, IFormFile file)
        {
            try
            {
                var attachment = await _fileService.SaveAttachment(file, commentId);
                return Ok(attachment);
            } 
            catch(ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("Comments/GetAttachmentsToComment/{commentId}")]
        public async Task<IActionResult> GetAttachmentsToComment(Guid commentId)
        {
            try
            {
                var attachments = await _fileService.GetAttachmentsToComment(commentId);
                return Ok(attachments);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpDelete("Comments/DeleteAttachment/{attachmentId}")]
        public async Task<IActionResult> DeleteAttachment(Guid attachmentId)
        {
            try
            {
                await _fileService.DeleteAttachment(attachmentId);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
