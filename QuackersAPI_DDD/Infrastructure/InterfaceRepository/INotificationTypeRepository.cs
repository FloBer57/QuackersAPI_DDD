using QuackersAPI_DDD.Domain.Model;

namespace QuackersAPI_DDD.Infrastructure.InterfaceRepository
{
    public interface INotificationTypeRepository
    {
        Task<IEnumerable<NotificationType>> GetAllNotificationTypes();
        Task<NotificationType> GetNotificationTypeById(int id);
        Task<NotificationType> CreateNotificationType(NotificationType notificationType);
        Task<NotificationType> UpdateNotificationType(NotificationType notificationType);
        Task <bool> DeleteNotificationType(int id);
        Task<bool> NotificationTypeNameExists(string name);
    }
}
