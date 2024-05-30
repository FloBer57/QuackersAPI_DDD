using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAttachmentById(int id)
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

        [Authorize]
        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> CreateAttachments([FromForm] CreateAttachmentDTO dto, [FromForm] List<IFormFile> files)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (files == null || files.Count == 0)
            {
                return BadRequest("No files received.");
            }

            try
            {
                var newAttachments = await _attachmentService.CreateAttachments(dto, files);
                return CreatedAtAction(nameof(GetAttachmentById), new { id = newAttachments.First().Attachment_Id }, newAttachments.First());
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while creating attachments: " + ex.Message);
            }
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAttachmentById(int id)
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
