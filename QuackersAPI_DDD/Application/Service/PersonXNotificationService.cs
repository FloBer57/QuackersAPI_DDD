using QuackersAPI_DDD.API.DTO.PersonXNotificationDTO;
using QuackersAPI_DDD.Application.InterfaceService;
using QuackersAPI_DDD.Domain.Model;
using QuackersAPI_DDD.Infrastructure.InterfaceRepository;

namespace QuackersAPI_DDD.Application.Service
{
    public class PersonXNotificationService : IPersonXNotificationService
    {
        private readonly IPersonXNotificationRepository _repository;

        public PersonXNotificationService(IPersonXNotificationRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<PersonXNotification>> GetAllAssociations()
        {
            return await _repository.GetAllAssociations();
        }

        public async Task<PersonXNotification> GetAssociationById(int personId, int notificationId)
        {
            return await _repository.GetAssociationById(personId, notificationId)
                ?? throw new KeyNotFoundException("Association not found with the specified IDs.");
        }

        public async Task<PersonXNotification> CreateAssociation(CreatePersonXNotificationDTO dto)
        {
            var newAssociation = new PersonXNotification
            {
                Person_Id = dto.PersonId,
                Notification_Id = dto.NotificationId,
                PersonXnotification_ReadDate = DateTime.Now,
            };
            return await _repository.CreateAssociation(newAssociation);
        }

        public async Task<PersonXNotification> UpdateAssociation(int personId, int notificationId, UpdatePersonXNotificationDTO dto)
        {
            var existingAssociation = await _repository.GetAssociationById(personId, notificationId);
            if (existingAssociation == null)
                throw new KeyNotFoundException("Association not found.");

            existingAssociation.PersonXnotification_ReadDate = dto.ReadDate ?? existingAssociation.PersonXnotification_ReadDate;
            return await _repository.UpdateAssociation(existingAssociation);
        }

        public async Task<bool> DeleteAssociation(int personId, int notificationId)
        {
            return await _repository.DeleteAssociation(personId, notificationId);
        }
    }
}
