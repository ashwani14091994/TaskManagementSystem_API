using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Mail;
using System.Threading.Tasks;
using TaskManagementSystem_API.Models;
using TaskManagementSystem_API.Services;

namespace TaskManagementSystem_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttachmentController : ControllerBase
    {
        private readonly AttachmentService _attachmentService;

        public AttachmentController(AttachmentService attachmentService)
        {
            _attachmentService = attachmentService;
        }

        [HttpGet]
        [Route("getAllAttachments")]
        public async Task<ActionResult<IEnumerable<AttachmentDetail>>> GetAttachments()
        {
            try
            {
                var attachments = await _attachmentService.GetAttachmentsAsync();
                return Ok(attachments);
            }
            catch (Exception ex)
            {
                // Log the exception (consider using a logging library)
                return StatusCode(500, new { Message = "An error occurred while retrieving attachments.", Detailed = ex.Message });
            }
        }

        [HttpGet("getAttachmentById/{id}")]      
        public async Task<ActionResult<AttachmentDetail>> GetAttachment(int id)
        {
            try
            {
                var attachment = await _attachmentService.GetAttachmentByIdAsync(id);
                if (attachment == null)
                {
                    return NotFound();
                }
                return Ok(attachment);
            }
            catch (Exception ex)
            {
                // Log the exception (consider using a logging library)
                return StatusCode(500, new { Message = $"An error occurred while retrieving the attachment with ID {id}.", Detailed = ex.Message });
            }
        }

        [HttpPost]
        [Route("createAttachment")]
        public async Task<ActionResult<AttachmentDetail>> CreateAttachment([FromBody] AttachmentDetail attachment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                await _attachmentService.AddAttachmentAsync(attachment);
                return CreatedAtAction(nameof(GetAttachment), new { id = attachment.AttachmentId }, attachment);
            }
            catch (Exception ex)
            {
                // Log the exception (consider using a logging library)
                return StatusCode(500, new { Message = "An error occurred while adding the attachment.", Detailed = ex.Message });
            }
        }

        [HttpPut("updateAttachment/{id}")]       
        public async Task<IActionResult> UpdateAttachment([FromRoute] int id, [FromBody] AttachmentDetail attachment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != attachment.AttachmentId)
            {
                return BadRequest("Attachment ID mismatch.");
            }
            try
            {
                var existingAttachment = await _attachmentService.GetAttachmentByIdAsync(id);
                if (existingAttachment == null)
                {
                    return NotFound();
                }
                await _attachmentService.UpdateAttachmentAsync(attachment);
                return NoContent();
            }
            catch (Exception ex)
            {
                // Log the exception (consider using a logging library)
                return StatusCode(500, new { Message = "An error occurred while updating the attachment.", Detailed = ex.Message });
            }
        }

        [HttpDelete("deleteAttachment/{id}")]       
        public async Task<IActionResult> DeleteAttachment(int id)
        {
            try
            {
                var existingAttachment = await _attachmentService.GetAttachmentByIdAsync(id);
                if (existingAttachment == null)
                {
                    return NotFound();
                }
                await _attachmentService.DeleteAttachmentAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                // Log the exception (consider using a logging library)
                return StatusCode(500, new { Message = $"An error occurred while deleting the attachment with ID {id}.", Detailed = ex.Message });
            }
        }
    }
}
