using System.ComponentModel.DataAnnotations;

namespace QuackersAPI_DDD.API.DTO.PersonXNotificationDTO
{
    public class CreatePersonXNotificationDTO
    {
        [Required(ErrorMessage = "Person_Id is required.")]
        public int PersonId { get; set; }
        [Required(ErrorMessage = "Notification_Id is required.")]
        public int NotificationId { get; set; }
    }
}
