using QuackersAPI_DDD.API.DTO.ChannelDTO;
using QuackersAPI_DDD.API.DTO.NotificationDTO;
using QuackersAPI_DDD.Application.InterfaceService;
using QuackersAPI_DDD.Domain.Model;
using QuackersAPI_DDD.Infrastructure.InterfaceRepository;
using QuackersAPI_DDD.Infrastructure.Repository;

namespace QuackersAPI_DDD.Application.Service
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly INotificationTypeRepository _notificationTypeRepository;

        public NotificationService(INotificationRepository notificationRepository, INotificationTypeRepository notificationTypeRepository)
        {
            _notificationRepository = notificationRepository;
            _notificationTypeRepository = notificationTypeRepository;
        }

        public async Task<IEnumerable<Notification>> GetAllNotifications()
        {
            var notification = await _notificationRepository.GetAllNotifications();
            return notification ?? new List<Notification>();
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
            var notificationType = await _notificationTypeRepository.GetNotificationTypeById(dto.Notification_TypeId);
            if (notificationType == null)
            {
                throw new KeyNotFoundException($"Notification Type with id {dto.Notification_TypeId} not found.");
            }

            var notificationDateOnly = DateTime.Now;

            var newNotification = new Notification
            {
                Notification_Name = dto.Notification_Name,
                Notification_Text = dto.Notification_Text,
                Notification_DatePost = notificationDateOnly,
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

            if (dto.Notification_Name != null)
            {
                notification.Notification_Name = dto.Notification_Name;
            }

            if (dto.Notification_Text != null)
            {
                notification.Notification_Text = dto.Notification_Text;
            }

            if (dto.Notification_TypeId.HasValue)
            {
                var notificationType = await _notificationTypeRepository.GetNotificationTypeById(dto.Notification_TypeId.Value);
                if (notificationType == null)
                {
                    throw new KeyNotFoundException($"Notification Type with id {dto.Notification_TypeId} not found.");
                }

                notification.Notification_TypeId = dto.Notification_TypeId.Value;
            }

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
