using QuackersAPI_DDD.API.DTO.PersonXMessageDTO;
using QuackersAPI_DDD.Application.InterfaceService;
using QuackersAPI_DDD.Domain.Model;
using QuackersAPI_DDD.Infrastructure.InterfaceRepository;

namespace QuackersAPI_DDD.Application.Service
{
    public class PersonXMessageService : IPersonXMessageService
    {
        private readonly IPersonXMessageRepository _repository;

        public PersonXMessageService(IPersonXMessageRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<PersonXMessage>> GetAllAssociations()
        {
            return await _repository.GetAllAssociations();
        }

        public async Task<PersonXMessage> GetAssociationById(int personId, int messageId)
        {
            return await _repository.GetAssociationById(personId, messageId);
        }

        public async Task<PersonXMessage> CreateAssociation(CreatePersonXMessageDTO dto)
        {
            var association = new PersonXMessage
            {
                Person_Id = dto.PersonId,
                Message_Id = dto.MessageId,
                PersonXmessageReadDate = DateTime.Now,
            };
            return await _repository.CreateAssociation(association);
        }

        public async Task<PersonXMessage> UpdateAssociation(int personId, int messageId, UpdatePersonXMessageDTO dto)
        {
            var association = await _repository.GetAssociationById(personId, messageId);
            if (association == null)
            {
                throw new KeyNotFoundException("Association not found.");
            }

            association.PersonXmessageReadDate = dto.ReadDate ?? association.PersonXmessageReadDate;
            return await _repository.UpdateAssociation(association);
        }

        public async Task<bool> DeleteAssociation(int personId, int messageId)
        {
            return await _repository.DeleteAssociation(personId, messageId);
        }
    }
}
