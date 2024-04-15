using QuackersAPI_DDD.API.DTO.NotificationDTO;
using QuackersAPI_DDD.Domain.Model;

namespace QuackersAPI_DDD.Application.InterfaceService
{
    public interface INotificationService
    {
        Task<IEnumerable<Notification>> GetAllNotifications();
        Task<Notification> GetNotificationById(int id);
        Task<Notification> CreateNotification(CreateNotificationDTO dto);
        Task<Notification> UpdateNotification(int id, UpdateNotificationDTO dto);
        Task<bool> DeleteNotification(int id);
    }
}
