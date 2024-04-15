using QuackersAPI_DDD.API.DTO.MessagexreactionxpersonDTO;
using QuackersAPI_DDD.Application.InterfaceService;
using QuackersAPI_DDD.Domain.Model;
using QuackersAPI_DDD.Infrastructure.InterfaceRepository;

namespace QuackersAPI_DDD.Application.Service
{
    public class MessageXReactionXPersonService : IMessageXReactionXPersonService
    {
        private readonly IMessageXReactionXPersonRepository _repository;

        public MessageXReactionXPersonService(IMessageXReactionXPersonRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<MessageXReactionXPerson>> GetAllReactions()
        {
            return await _repository.GetAllReactions();
        }

        public async Task<MessageXReactionXPerson> GetReactionById(int personId, int messageId, int reactionId)
        {
            return await _repository.GetReactionById(personId, messageId, reactionId);
        }

        public async Task<MessageXReactionXPerson> CreateReaction(CreateMessageXReactionXPersonDTO dto)
        {
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
                throw new KeyNotFoundException("Reaction not found.");

            reaction.MessageXreactionXpersonReactionDate = dto.ReactionDate ?? reaction.MessageXreactionXpersonReactionDate; //Useless//
            return await _repository.UpdateReaction(reaction);
        }

        public async Task<bool> DeleteReaction(int personId, int messageId, int reactionId)
        {
            return await _repository.DeleteReaction(personId, messageId, reactionId);
        }
    }
}
