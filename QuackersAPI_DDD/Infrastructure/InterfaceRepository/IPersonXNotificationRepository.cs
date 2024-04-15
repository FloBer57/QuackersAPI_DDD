using QuackersAPI_DDD.Domain.Model;

namespace QuackersAPI_DDD.Infrastructure.InterfaceRepository
{
    public interface IPersonXNotificationRepository
    {
        Task<IEnumerable<PersonXNotification>> GetAllAssociations();
        Task<PersonXNotification> GetAssociationById(int personId, int notificationId);
        Task<PersonXNotification> CreateAssociation(PersonXNotification association);
        Task<PersonXNotification> UpdateAssociation(PersonXNotification association);
        Task<bool> DeleteAssociation(int personId, int notificationId);
    }
}
