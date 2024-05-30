using QuackersAPI_DDD.API.DTO.PersonXNotificationDTO;
using QuackersAPI_DDD.Application.Interface;
using QuackersAPI_DDD.Application.InterfaceService;
using QuackersAPI_DDD.Domain.Model;
using QuackersAPI_DDD.Infrastructure.InterfaceRepository;

namespace QuackersAPI_DDD.Application.Service
{
    public class PersonXNotificationService : IPersonXNotificationService
    {
        private readonly IPersonXNotificationRepository _repository;
        private readonly IPersonService _personService;
        private readonly INotificationService _notificationService;

        public PersonXNotificationService(IPersonXNotificationRepository repository, IPersonService personService, INotificationService notificationService)
        {
            _repository = repository;
            _personService = personService;
            _notificationService = notificationService;
        }

        public async Task<IEnumerable<PersonXNotification>> GetAllAssociations()
        {
            var associations = await _repository.GetAllAssociations();
            return associations ?? new List<PersonXNotification>();
        }

        public async Task<PersonXNotification> GetAssociationById(int personId, int notificationId)
        {
            var association = await _repository.GetAssociationById(personId, notificationId);
            if (association == null)
                throw new KeyNotFoundException($"Association not found with person ID {personId} and notification ID {notificationId}.");
            return association;
        }

        public async Task<IEnumerable<Person>> GetPersonsByNotificationId(int notificationId)
        {
            var personByNotification = await _repository.GetPersonsByNotificationId(notificationId);
            if (personByNotification == null)
                throw new KeyNotFoundException("No Person with this Notification");
            return personByNotification;
        }

        public async Task<IEnumerable<Notification>> GetNotificationsByPersonId(int personId)
        {
            var notificationByPerson = await _repository.GetNotificationsByPersonId(personId);
            if (notificationByPerson == null)
            {
                throw new KeyNotFoundException("No notification with this person");
            }
            return notificationByPerson;
        }

        public async Task<PersonXNotification> CreateAssociation(CreatePersonXNotificationDTO dto)
        {
            var checkPerson = await _personService.GetPersonById(dto.PersonId);
            if (checkPerson == null )
                throw new KeyNotFoundException($"Person id =  {dto.PersonId} do not exist");

            var checkNotification = await _notificationService.GetNotificationById(dto.NotificationId);
            if (checkNotification == null)
                throw new KeyNotFoundException($"Notification id =  {dto.PersonId} do not exist");

            var existing = await _repository.GetAssociationById(dto.PersonId, dto.NotificationId);
            if (existing != null)
                throw new InvalidOperationException("An association already exists with the provided person and notification IDs.");

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
            var checkPerson = await _personService.GetPersonById(personId);
            if (checkPerson == null)
                throw new KeyNotFoundException($"Person id =  {personId} do not exist");

            var checkNotification = await _notificationService.GetNotificationById(notificationId);
            if (checkNotification == null)
                throw new KeyNotFoundException($"Notification id =  {notificationId} do not exist");

            var existingAssociation = await GetAssociationById(personId, notificationId);
            
            existingAssociation.PersonXnotification_ReadDate = dto.ReadDate ?? existingAssociation.PersonXnotification_ReadDate;
            return await _repository.UpdateAssociation(existingAssociation);
        }

        public async Task<bool> DeleteAssociation(int personId, int notificationId)
        {
            var checkPerson = await _personService.GetPersonById(personId);
            if (checkPerson == null)
                throw new KeyNotFoundException($"Person id =  {personId} do not exist");

            var checkNotification = await _notificationService.GetNotificationById(notificationId);
            if (checkNotification == null)
                throw new KeyNotFoundException($"Notification id =  {personId} do not exist");

            await _repository.DeleteAssociation(personId,notificationId);
            return true;
        }
    }
}
