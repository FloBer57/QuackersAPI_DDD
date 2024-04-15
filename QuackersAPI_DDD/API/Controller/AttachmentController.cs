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
            var attachment = await _attachmentService.GetAttachmentById(id);
            if (attachment == null)
            {
                return NotFound($"Attachment with id {id} not found.");
            }
            return Ok(attachment);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAttachmentDTO dto)
        {
            var createdAttachment = await _attachmentService.CreateAttachment(dto);
            return CreatedAtAction(nameof(GetById), new { id = createdAttachment.Attachment_Id }, createdAttachment);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _attachmentService.DeleteAttachment(id);
            if (!success)
            {
                return NotFound($"Attachment with id {id} not found.");
            }
            return Ok($"Attachment with id {id} deleted successfully.");
        }
    }
}
