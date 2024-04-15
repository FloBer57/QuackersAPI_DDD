using Microsoft.AspNetCore.Mvc;
using QuackersAPI_DDD.API.DTO.NotificationTypeDTO;
using QuackersAPI_DDD.Application.InterfaceService;

namespace QuackersAPI_DDD.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationTypeController : ControllerBase
    {
        private readonly INotificationTypeService _notificationTypeService;

        public NotificationTypeController(INotificationTypeService notificationTypeService)
        {
            _notificationTypeService = notificationTypeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllNotificationTypes()
        {
            var types = await _notificationTypeService.GetAllNotificationTypes();
            if (types == null)
                return NotFound("No notification types found.");
            return Ok(types);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetNotificationTypeById(int id)
        {
            var type = await _notificationTypeService.GetNotificationTypeById(id);
            if (type == null)
                return NotFound($"NotificationType with id {id} not found.");
            return Ok(type);
        }

        [HttpPost]
        public async Task<IActionResult> CreateNotificationType([FromBody] CreateNotificationTypeDTO dto)
        {
            var createdType = await _notificationTypeService.CreateNotificationType(dto);
            return CreatedAtAction(nameof(GetNotificationTypeById), new { id = createdType.NotificationType_Id }, createdType);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateNotificationType(int id, [FromBody] UpdateNotificationTypeDTO dto)
        {
            var updatedType = await _notificationTypeService.UpdateNotificationType(id, dto);
            if (updatedType == null)
                return NotFound($"NotificationType with id {id} not found.");
            return Ok(updatedType);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNotificationType(int id)
        {
            var success = await _notificationTypeService.DeleteNotificationType(id);
            if (!success)
                return NotFound($"NotificationType with id {id} not found.");
            return Ok($"NotificationType with id {id} deleted successfully.");
        }
    }
}
