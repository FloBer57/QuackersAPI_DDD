using QuackersAPI_DDD.Domain.Model;

namespace QuackersAPI_DDD.Infrastructure.InterfaceRepository
{
    public interface INotificationRepository
    {
        Task<IEnumerable<Notification>> GetAllNotifications();
        Task<Notification> GetNotificationById(int id);
        Task<Notification> CreateNotification(Notification notification);
        Task<Notification> UpdateNotification(Notification notification);
        Task<bool> DeleteNotification(int id);
    }
}
