using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuackersAPI_DDD.API.DTO.PersonXNotificationDTO;
using QuackersAPI_DDD.Application.InterfaceService;
using System.Threading.Tasks;

namespace QuackersAPI_DDD.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonXNotificationController : ControllerBase
    {
        private readonly IPersonXNotificationService _service;

        public PersonXNotificationController(IPersonXNotificationService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAssociation()
        {
                var associations = await _service.GetAllAssociations();
                return Ok(associations);
        }

        [HttpGet("{personId}/{notificationId}")]
        public async Task<IActionResult> GetAssociationById(int personId, int notificationId)
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
            catch (Exception e)
            {
                return StatusCode(500, "Internal server error: " + e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateAssociation([FromBody] CreatePersonXNotificationDTO dto)
        {
            try
            {
                var createdAssociation = await _service.CreateAssociation(dto);
                return CreatedAtAction(nameof(GetAssociationById), new { personId = dto.PersonId, notificationId = dto.NotificationId }, createdAssociation);
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
                return StatusCode(500, "Internal server error: " + e.Message);
            }
        }

        [HttpPut("{personId}/{notificationId}")]
        public async Task<IActionResult> UpdateAssociation(int personId, int notificationId, [FromBody] UpdatePersonXNotificationDTO dto)
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
        public async Task<IActionResult> DeleteAssociation(int personId, int notificationId)
        {
            try
            {
                var success = await _service.DeleteAssociation(personId, notificationId);
                return NoContent();
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

        [HttpGet("person/{personId}/notification")]
        public async Task<IActionResult> GetNotificationsByPersonId(int personId)
        {
            try
            {
                var persons = await _service.GetNotificationsByPersonId(personId);
                return Ok(persons);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An internal server error has occurred: " + ex.Message);
            }
        }

        [HttpGet("notification/{notificationId}/person")]
        public async Task<IActionResult> GetPersonsByNotificationId(int notificationId)
        {
            try
            {
                var channels = await _service.GetPersonsByNotificationId(notificationId);
                return Ok(channels);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An internal server error has occurred: " + ex.Message);
            }
        }
    }
}