using QuackersAPI_DDD.API.DTO.ChannelDTO;
using QuackersAPI_DDD.API.DTO.NotificationTypeDTO;
using QuackersAPI_DDD.Application.InterfaceService;
using QuackersAPI_DDD.Domain.Model;
using QuackersAPI_DDD.Infrastructure.InterfaceRepository;
using QuackersAPI_DDD.Infrastructure.Repository;

namespace QuackersAPI_DDD.Application.Service
{
    public class NotificationTypeService : INotificationTypeService
    {
        private readonly INotificationTypeRepository _notificationTypeRepository;

        public NotificationTypeService(INotificationTypeRepository notificationTypeRepository)
        {
            _notificationTypeRepository = notificationTypeRepository;
        }

        public async Task<IEnumerable<NotificationType>> GetAllNotificationTypes()
        {
            var notification = await _notificationTypeRepository.GetAllNotificationTypes();
            return notification ?? new List<NotificationType>();
        }

        public async Task<NotificationType> GetNotificationTypeById(int id)
        {
            var notificationType = await _notificationTypeRepository.GetNotificationTypeById(id);
            if (notificationType == null)
            {
                throw new KeyNotFoundException($"NotificationType with id {id} not found.");
            }
            return notificationType;
        }

        public async Task<NotificationType> CreateNotificationType(CreateNotificationTypeDTO dto)
        {
            if (await _notificationTypeRepository.NotificationTypeNameExists(dto.NotificationType_Name))
            {
                throw new InvalidOperationException($"A Notification Type with the name '{dto.NotificationType_Name}' already exists.");
            }

            var newNotificationType = new NotificationType
            {
                NotificationType_Name = dto.NotificationType_Name
            };

            return await _notificationTypeRepository.CreateNotificationType(newNotificationType);
        }

        public async Task<NotificationType> UpdateNotificationType(int id, UpdateNotificationTypeDTO dto)
        {
            var notificationType = await _notificationTypeRepository.GetNotificationTypeById(id);
            if (notificationType == null)
            {
                throw new KeyNotFoundException($"NotificationType with id {id} not found.");
            }

            notificationType.NotificationType_Name = dto.NotificationType_Name;
            return await _notificationTypeRepository.UpdateNotificationType(notificationType);
        }

        public async Task<bool> DeleteNotificationType(int id)
        {
            var notificationType = await _notificationTypeRepository.GetNotificationTypeById(id);
            if (notificationType == null)
            {
                throw new KeyNotFoundException($"NotificationType with id {id} not found.");
            }

            return await _notificationTypeRepository.DeleteNotificationType(id);
        }
    }
}
