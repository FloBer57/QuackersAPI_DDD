using System.ComponentModel.DataAnnotations;

namespace QuackersAPI_DDD.API.DTO.NotificationTypeDTO
{
    public class UpdateNotificationTypeDTO
    {
        [Required(ErrorMessage = "NotificationType name is required.")]
        [StringLength(255, ErrorMessage = "NotificationTypename must be at most 50 characters long.")]
        public string NotificationType_Name { get; set; }
    }
}
