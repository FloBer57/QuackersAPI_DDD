using Microsoft.AspNetCore.Mvc;
using QuackersAPI_DDD.API.DTO.PersonXNotificationDTO;
using QuackersAPI_DDD.Application.InterfaceService;

namespace QuackersAPI_DDD.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonXNotificationController : ControllerBase
    {
        private readonly IPersonXNotificationService _service;

        public PersonXNotificationController(IPersonXNotificationService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var associations = await _service.GetAllAssociations();
            return Ok(associations);
        }

        [HttpGet("{personId}/{notificationId}")]
        public async Task<IActionResult> GetById(int personId, int notificationId)
        {
            try
            {
                var association = await _service.GetAssociationById(personId, notificationId);
                return Ok(association);
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePersonXNotificationDTO dto)
        {
            try
            {
                var createdAssociation = await _service.CreateAssociation(dto);
                return CreatedAtAction(nameof(GetById), new { personId = dto.PersonId, notificationId = dto.NotificationId }, createdAssociation);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Internal server error: " + e.Message);
            }
        }

        [HttpPut("{personId}/{notificationId}")]
        public async Task<IActionResult> Update(int personId, int notificationId, [FromBody] UpdatePersonXNotificationDTO dto)
        {
            try
            {
                var updatedAssociation = await _service.UpdateAssociation(personId, notificationId, dto);
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

        [HttpDelete("{personId}/{notificationId}")]
        public async Task<IActionResult> Delete(int personId, int notificationId)
        {
            try
            {
                var success = await _service.DeleteAssociation(personId, notificationId);
                if (!success)
                {
                    return NotFound($"Association not found with person ID {personId} and notification ID {notificationId}.");
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
