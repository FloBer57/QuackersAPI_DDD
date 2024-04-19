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
        [Consumes("multipart/form-data")]
        
        public async Task<IActionResult> CreateAttachments([FromForm] CreateAttachmentDTO dto, [FromForm] List<IFormFile> files)
        {
            if (files == null || files.Count == 0)
            {
                return BadRequest("No files received.");
            }

            try
            {
                var newAttachments = await _attachmentService.CreateAttachments(dto, files);
                if (newAttachments.Count == 0)
                {
                    return BadRequest("No attachments were created. All files failed to process.");
                }
                return CreatedAtAction(nameof(GetById), new { ids = newAttachments.Select(a => a.Attachment_Id).ToList() }, newAttachments);
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
