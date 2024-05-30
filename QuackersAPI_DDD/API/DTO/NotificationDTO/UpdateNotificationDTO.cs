using System.ComponentModel.DataAnnotations;

namespace QuackersAPI_DDD.API.DTO.NotificationDTO
{
    public class UpdateNotificationDTO
    {
        [StringLength(50, ErrorMessage = "Notification name must be at most 50 characters long.")]
        public string? Notification_Name { get; set; }
        [StringLength(50, ErrorMessage = "Notification Text must be at most 255 characters long.")]
        public string? Notification_Text { get; set; }
        public int? Notification_TypeId { get; set; }
    }
}
