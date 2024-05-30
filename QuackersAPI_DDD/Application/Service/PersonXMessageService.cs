using QuackersAPI_DDD.API.DTO.PersonXMessageDTO;
using QuackersAPI_DDD.Application.InterfaceService;
using QuackersAPI_DDD.Domain.Model;
using QuackersAPI_DDD.Infrastructure.InterfaceRepository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuackersAPI_DDD.Application.Service
{
    public class PersonXMessageService : IPersonXMessageService
    {
        private readonly IPersonXMessageRepository _repository;
        private readonly IPersonRepository _personRepository;
        private readonly IMessageRepository _messageRepository;

        public PersonXMessageService(IPersonXMessageRepository repository, IPersonRepository personRepository, IMessageRepository messageRepository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _personRepository = personRepository ?? throw new ArgumentNullException(nameof(personRepository));
            _messageRepository = messageRepository ?? throw new ArgumentNullException(nameof(messageRepository));
        }

        public async Task<IEnumerable<PersonXMessage>> GetAllAssociations()
        {
            var associations = await _repository.GetAllAssociations();
            return associations ?? new List<PersonXMessage>();
        }

        public async Task<PersonXMessage> GetAssociationById(int personId, int messageId)
        {
            var association = await _repository.GetAssociationById(personId, messageId);
            if (association == null)
                throw new KeyNotFoundException($"Association not found with person ID {personId} and message ID {messageId}.");
            return association;
        }

        public async Task<IEnumerable<Message>> GetMessagesByPersonId(int personId)
        {
            var messageByPerson =  await _repository.GetMessagesByPersonId(personId);
            if (messageByPerson == null)
            {
                throw new KeyNotFoundException("No message found with this person.");
            }
            return messageByPerson;
        }

        public async Task<IEnumerable<Person>> GetPersonsByMessageId(int personId)
        {
           var personByMessage = await _repository.GetPersonsByMessageId(personId);
            if (personByMessage == null)
            {
                throw new KeyNotFoundException("No person found with this message.");
            }
            return personByMessage;
        }


        public async Task<PersonXMessage> CreateAssociation(CreatePersonXMessageDTO dto)
        {

            var person = await _personRepository.GetPersonById(dto.PersonId);
            if (person == null)
                throw new KeyNotFoundException($"Person ID {dto.PersonId} does not exist.");

            var message = await _messageRepository.GetMessageById(dto.MessageId);
            if (message == null)
                throw new KeyNotFoundException($"Message ID {dto.MessageId} does not exist.");

            var existing = await _repository.GetAssociationById(dto.PersonId, dto.MessageId);
            if (existing != null)
                throw new InvalidOperationException("An association already exists with the provided person and message IDs.");

            var newAssociation = new PersonXMessage
            {
                Person_Id = dto.PersonId,
                Message_Id = dto.MessageId,
                PersonXmessageReadDate = DateTime.Now,
            };

            return await _repository.CreateAssociation(newAssociation);
        }

        public async Task<PersonXMessage> UpdateAssociation(int personId, int messageId, UpdatePersonXMessageDTO dto)
        {
            var person = await _personRepository.GetPersonById(personId);
            if (person == null)
                throw new KeyNotFoundException($"Person ID {personId} does not exist.");

            var message = await _messageRepository.GetMessageById(messageId);
            if (message == null)
                throw new KeyNotFoundException($"Message ID {messageId} does not exist.");

            var association = await GetAssociationById(personId, messageId);
            association.PersonXmessageReadDate = dto.ReadDate ?? association.PersonXmessageReadDate;
            return await _repository.UpdateAssociation(association);
        }

        public async Task<bool> DeleteAssociation(int personId, int messageId)
        {
            var person = await _personRepository.GetPersonById(personId);
            if (person == null)
                throw new KeyNotFoundException($"Person ID {personId} does not exist.");

            var message = await _messageRepository.GetMessageById(messageId);
            if (message == null)
                throw new KeyNotFoundException($"Message ID {messageId} does not exist.");

            await _repository.DeleteAssociation(personId, messageId);
            return true;
        }
    }
}
