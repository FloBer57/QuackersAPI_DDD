    using QuackersAPI_DDD.API.DTO.NotificationTypeDTO;
    using QuackersAPI_DDD.Domain.Model;

    namespace QuackersAPI_DDD.Application.InterfaceService
    {
        public interface INotificationTypeService
        {
            Task<IEnumerable<NotificationType>> GetAllNotificationTypes();
            Task<NotificationType> GetNotificationTypeById(int id);
            Task<NotificationType> CreateNotificationType(CreateNotificationTypeDTO dto);
            Task<NotificationType> UpdateNotificationType(int id, UpdateNotificationTypeDTO dto);
            Task<bool> DeleteNotificationType(int id);
        }
    }
