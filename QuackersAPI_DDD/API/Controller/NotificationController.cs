using Microsoft.AspNetCore.Mvc;
using QuackersAPI_DDD.API.DTO.NotificationDTO;
using QuackersAPI_DDD.Application.InterfaceService;
using QuackersAPI_DDD.Infrastructure.InterfaceRepository;

namespace QuackersAPI_DDD.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var notifications = await _notificationService.GetAllNotifications();
            return Ok(notifications);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var notification = await _notificationService.GetNotificationById(id);
            if (notification == null)
            {
                return NotFound($"Notification with id {id} not found.");
            }
            return Ok(notification);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateNotificationDTO dto)
        {
            var createdNotification = await _notificationService.CreateNotification(dto);
            return CreatedAtAction(nameof(GetById), new { id = createdNotification.Notification_Id }, createdNotification);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateNotificationDTO dto)
        {
            try
            {
                var updatedNotification = await _notificationService.UpdateNotification(id, dto);
                return Ok(updatedNotification);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Internal server error: " + e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _notificationService.DeleteNotification(id);
            if (!success)
            {
                return NotFound($"Notification with id {id} not found.");
            }
            return Ok($"Notification with id {id} deleted successfully.");
        }
    }
}
