using Microsoft.AspNetCore.Mvc;
using QuackersAPI_DDD.API.DTO.PersonXMessageDTO;
using QuackersAPI_DDD.Application.InterfaceService;

namespace QuackersAPI_DDD.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonXMessageController : ControllerBase
    {
        private readonly IPersonXMessageService _service;

        public PersonXMessageController(IPersonXMessageService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var associations = await _service.GetAllAssociations();
            return Ok(associations);
        }

        [HttpGet("{personId}/{messageId}")]
        public async Task<IActionResult> GetById(int personId, int messageId)
        {
            try
            {
                var association = await _service.GetAssociationById(personId, messageId);
                return Ok(association);
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePersonXMessageDTO dto)
        {
            try
            {
                var createdAssociation = await _service.CreateAssociation(dto);
                return CreatedAtAction(nameof(GetById), new { personId = dto.PersonId, messageId = dto.MessageId }, createdAssociation);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Internal server error: " + e.Message);
            }
        }

        [HttpPut("{personId}/{messageId}")]
        public async Task<IActionResult> Update(int personId, int messageId, [FromBody] UpdatePersonXMessageDTO dto)
        {
            try
            {
                var updatedAssociation = await _service.UpdateAssociation(personId, messageId, dto);
                return Ok(updatedAssociation);
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Internal server error: " + e.Message);
            }
        }

        [HttpDelete("{personId}/{messageId}")]
        public async Task<IActionResult> Delete(int personId, int messageId)
        {
            try
            {
                var success = await _service.DeleteAssociation(personId, messageId);
                if (!success)
                {
                    return NotFound($"Association not found with person ID {personId} and message ID {messageId}.");
                }
                return Ok($"Association successfully deleted.");
            }
            catch (Exception e)
            {
                return StatusCode(500, "Internal server error: " + e.Message);
            }
        }
    }
}
