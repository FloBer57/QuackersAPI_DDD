using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuackersAPI_DDD.API.DTO.AttachmentDTO;
using QuackersAPI_DDD.Application.InterfaceService;

namespace QuackersAPI_DDD.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttachmentController : ControllerBase
    {
        private readonly IAttachmentService _attachmentService;

        public AttachmentController(IAttachmentService attachmentService)
        {
            _attachmentService = attachmentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
                var attachments = await _attachmentService.GetAllAttachments();
                return Ok(attachments);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var attachment = await _attachmentService.GetAttachmentById(id);
                return Ok(attachment);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while get the attachment: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateAttachment([FromBody] CreateAttachmentDTO dto)
        {
            try
            {
                var newAttachment = await _attachmentService.CreateAttachment(dto);
                return CreatedAtAction(nameof(GetById), new { id = newAttachment.Attachment_Id }, newAttachment);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while creating the attachment.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var success = await _attachmentService.DeleteAttachment(id);
                return Ok($"Attachment with id {id} deleted successfully.");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while deleting the attachment: {ex.Message}");
            }
        }
    }
}
