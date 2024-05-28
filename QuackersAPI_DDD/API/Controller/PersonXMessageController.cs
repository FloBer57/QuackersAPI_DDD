using Microsoft.AspNetCore.Authorization;
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
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }
        [Authorize(Roles ="Administrateur")]
        [HttpGet]
        public async Task<IActionResult> GetAllAssociations()
        {

                var associations = await _service.GetAllAssociations();
                return Ok(associations);
        }
        [Authorize]
        [HttpGet("{personId}/{messageId}")]
        public async Task<IActionResult> GetAssociationById(int personId, int messageId)
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
            catch (Exception e)
            {
                return StatusCode(500, $"Internal server error: {e.Message}");
            }
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateAssociation([FromBody] CreatePersonXMessageDTO dto)
        {
            try
            {
                var createdAssociation = await _service.CreateAssociation(dto);
                return CreatedAtAction(nameof(GetAssociationById), new { personId = dto.PersonId, messageId = dto.MessageId }, createdAssociation);
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (InvalidOperationException e)
            {
                return Conflict(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Internal server error: {e.Message}");
            }
        }
        [Authorize]
        [HttpPut("{personId}/{messageId}")]
        public async Task<IActionResult> UpdateAssociation(int personId, int messageId, [FromBody] UpdatePersonXMessageDTO dto)
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
                return StatusCode(500, $"Internal server error: {e.Message}");
            }
        }
        [Authorize]
        [HttpDelete("{personId}/{messageId}")]
        public async Task<IActionResult> DeleteAssociation(int personId, int messageId)
        {
            try
            {
                bool success = await _service.DeleteAssociation(personId, messageId);
                return NoContent();
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Internal server error: {e.Message}");
            }
        }
        [Authorize]
        [HttpGet("message/{messageId}/persons")]
        public async Task<IActionResult> GetPersonsByMessageId(int messageId)
        {
            try
            {
                var persons = await _service.GetPersonsByMessageId(messageId);
                return Ok(persons);
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An internal server error has occurred: " + ex.Message);
            }
        }
        [Authorize]
        [HttpGet("persons/{personId}/messages")]
        public async Task<IActionResult> GetMessagesByPersonId(int personId)
        {
            try
            {
                var messages = await _service.GetMessagesByPersonId(personId);
                return Ok(messages);
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An internal server error has occurred: " + ex.Message);
            }
        }
    }
}
