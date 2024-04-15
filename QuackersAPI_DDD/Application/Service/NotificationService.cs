using QuackersAPI_DDD.API.DTO.NotificationDTO;
using QuackersAPI_DDD.Application.InterfaceService;
using QuackersAPI_DDD.Domain.Model;
using QuackersAPI_DDD.Infrastructure.InterfaceRepository;

namespace QuackersAPI_DDD.Application.Service
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepository;

        public NotificationService(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public async Task<IEnumerable<Notification>> GetAllNotifications()
        {
            return await _notificationRepository.GetAllNotifications();
        }

        public async Task<Notification> GetNotificationById(int id)
        {
            var notification = await _notificationRepository.GetNotificationById(id);
            if (notification == null)
            {
                throw new KeyNotFoundException($"Notification with id {id} not found.");
            }
            return notification;
        }

        public async Task<Notification> CreateNotification(CreateNotificationDTO dto)
        {
            var newNotification = new Notification
            {
                Notification_Name = dto.Notification_Name,
                Notification_Text = dto.Notification_Text,
                Notification_DatePost = DateOnly.FromDateTime(DateTime.Now),
                Notification_TypeId = dto.Notification_TypeId
            };
            return await _notificationRepository.CreateNotification(newNotification);
        }

        public async Task<Notification> UpdateNotification(int id, UpdateNotificationDTO dto)
        {
            var notification = await _notificationRepository.GetNotificationById(id);
            if (notification == null)
            {
                throw new KeyNotFoundException($"Notification with id {id} not found.");
            }

            notification.Notification_Name = dto.Notification_Name;
            notification.Notification_Text = dto.Notification_Text;
            notification.Notification_DatePost = DateOnly.FromDateTime(DateTime.Now);
            notification.Notification_TypeId = dto.Notification_TypeId;

            return await _notificationRepository.UpdateNotification(notification);
        }

        public async Task<bool> DeleteNotification(int id)
        {
            var notification = await _notificationRepository.GetNotificationById(id);
            if (notification == null)
            {
                throw new KeyNotFoundException($"Notification with id {id} not found.");
            }
            return await _notificationRepository.DeleteNotification(id);
        }
    }
}
