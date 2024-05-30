using Microsoft.AspNetCore.Mvc;
using QuackersAPI_DDD.API.DTO.PersonXNotificationDTO;
using QuackersAPI_DDD.Domain.Model;

namespace QuackersAPI_DDD.Application.InterfaceService
{

    public interface IPersonXNotificationService
    {
        Task<IEnumerable<PersonXNotification>> GetAllAssociations();
        Task<PersonXNotification> GetAssociationById(int personId, int notificationId);
        Task<PersonXNotification> CreateAssociation(CreatePersonXNotificationDTO dto);
        Task<PersonXNotification> UpdateAssociation(int personId, int notificationId, UpdatePersonXNotificationDTO dto);
        Task<bool> DeleteAssociation(int personId, int notificationId);
        Task<IEnumerable<Person>> GetPersonsByNotificationId(int notificationId);
        Task<IEnumerable<Notification>> GetNotificationsByPersonId(int personId);

    }

}
