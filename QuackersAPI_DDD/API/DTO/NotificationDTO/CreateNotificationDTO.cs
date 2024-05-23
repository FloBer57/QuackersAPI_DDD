using System.ComponentModel.DataAnnotations;

namespace QuackersAPI_DDD.API.DTO.NotificationDTO
{
    public class CreateNotificationDTO
    {
        [Required(ErrorMessage = "Notification name is required.")]
        [StringLength(255, ErrorMessage = "Notification name must be at most 255 characters long.")]
        public string? Notification_Name { get; set; }
        [Required(ErrorMessage = "Notification Text is required.")]
        [StringLength(255, ErrorMessage = "Notification Text must be at most 255 characters long.")]
        public string? Notification_Text { get; set; }
        [Required(ErrorMessage = "NotificationType Id is required.")]
        public int Notification_TypeId { get; set; }
    }
}
