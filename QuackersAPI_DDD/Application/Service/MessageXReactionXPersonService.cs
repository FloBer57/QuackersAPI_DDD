using QuackersAPI_DDD.API.DTO.MessagexreactionxpersonDTO;
using QuackersAPI_DDD.Application.Interface;
using QuackersAPI_DDD.Application.InterfaceService;
using QuackersAPI_DDD.Domain.Model;
using QuackersAPI_DDD.Infrastructure.InterfaceRepository;

namespace QuackersAPI_DDD.Application.Service
{
    public class MessageXReactionXPersonService : IMessageXReactionXPersonService
    {
        private readonly IMessageXReactionXPersonRepository _repository;
        private readonly IMessageService _messageService; 
        private readonly IPersonService _personService;

        public MessageXReactionXPersonService(
            IMessageXReactionXPersonRepository repository,
            IMessageService messageService, 
            IPersonService personService)
        {
            _repository = repository;
            _messageService = messageService;
            _personService = personService;
        }

        public async Task<IEnumerable<MessageXReactionXPerson>> GetAllReactions()
        {
            return await _repository.GetAllReactions();
        }

        public async Task<MessageXReactionXPerson> GetReactionById(int personId, int messageId, int reactionId)
        {
            var reaction = await _repository.GetReactionById(personId, messageId, reactionId);
            if (reaction == null)
            {
                throw new KeyNotFoundException($"Reaction not found with person ID {personId}, message ID {messageId}, and reaction ID {reactionId}.");
            }
            return reaction;
        }

        public async Task<MessageXReactionXPerson> CreateReaction(CreateMessageXReactionXPersonDTO dto)
        {
            var message = await _messageService.GetMessageById(dto.MessageId);
            if (message == null)
            {
                throw new KeyNotFoundException($"Message with id {dto.MessageId} not found.");
            }

            var person = await _personService.GetPersonById(dto.PersonId);
            if (person == null)
            {
                throw new KeyNotFoundException($"Person with id {dto.PersonId} not found.");
            }

            var reaction = new MessageXReactionXPerson
            {
                Person_Id = dto.PersonId,
                Message_Id = dto.MessageId,
                Reaction_Id = dto.ReactionId,
                MessageXreactionXpersonReactionDate = DateTime.Now,
            };
            return await _repository.CreateReaction(reaction);
        }

        public async Task<MessageXReactionXPerson> UpdateReaction(int personId, int messageId, int reactionId, UpdateMessageXReactionXPersonDTO dto)
        {
            var reaction = await _repository.GetReactionById(personId, messageId, reactionId);
            if (reaction == null)
            {
                throw new KeyNotFoundException($"Reaction not found with person ID {personId}, message ID {messageId}, and reaction ID {reactionId}.");
            }

            reaction.MessageXreactionXpersonReactionDate = dto.ReactionDate ?? reaction.MessageXreactionXpersonReactionDate;
            return await _repository.UpdateReaction(reaction);
        }

        public async Task<bool> DeleteReaction(int personId, int messageId, int reactionId)
        {
            var exists = await _repository.GetReactionById(personId, messageId, reactionId);
            if (exists == null)
            {
                throw new KeyNotFoundException($"Reaction not found with person ID {personId}, message ID {messageId}, and reaction ID {reactionId}.");
            }

            await _repository.DeleteReaction(personId, messageId, reactionId);
            return true;
        }
    }
}