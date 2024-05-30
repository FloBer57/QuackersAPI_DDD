using QuackersAPI_DDD.API.DTO.ReactionDTO;
using QuackersAPI_DDD.Application.InterfaceService;
using QuackersAPI_DDD.Domain.Model;
using QuackersAPI_DDD.Infrastructure.InterfaceRepository;

namespace QuackersAPI_DDD.Application.Service
{
    public class ReactionService : IReactionService
    {
        private readonly IReactionRepository _reactionRepository;

        public ReactionService(IReactionRepository reactionRepository)
        {
            _reactionRepository = reactionRepository ?? throw new ArgumentNullException(nameof(reactionRepository));
        }

        public async Task<IEnumerable<Reaction>> GetAllReactions()
        {
            var reactions = await _reactionRepository.GetAllReactions();
            return reactions ?? new List <Reaction>(); 
        }

        public async Task<Reaction> GetReactionById(int id)
        {
            var reaction = await _reactionRepository.GetReactionById(id);
            if (reaction == null)
                throw new KeyNotFoundException($"Reaction with ID {id} not found.");
            return reaction;
        }

        public async Task<Reaction> CreateReaction(CreateReactionDTO reactionDto)
        {
            if (await _reactionRepository.ReactionNameExist(reactionDto.ReactionName))
                throw new InvalidOperationException($"A reaction with the name '{reactionDto.ReactionName}' already exists.");

            var reaction = new Reaction
            {
                Reaction_Name = reactionDto.ReactionName,
                Reaction_PicturePath = reactionDto.ReactionPicturePath
            };

            return await _reactionRepository.CreateReaction(reaction);
        }

        public async Task<Reaction> UpdateReaction(int id, UpdateReactionDTO reactionDto)
        {
            var reaction = await _reactionRepository.GetReactionById(id);
            if (reaction == null)
                throw new KeyNotFoundException($"Reaction with ID {id} not found.");

            if (await _reactionRepository.ReactionNameExist(reactionDto.ReactionName))
                throw new InvalidOperationException($"Another reaction with the name '{reactionDto.ReactionName}' already exists.");

            reaction.Reaction_Name = reactionDto.ReactionName;
            reaction.Reaction_PicturePath = reactionDto.ReactionPicturePath;

            return await _reactionRepository.UpdateReaction(reaction);
        }

        public async Task<bool> DeleteReaction(int id)
        {
            var reaction = await _reactionRepository.GetReactionById(id);
            if (reaction == null)
                throw new KeyNotFoundException($"Reaction with ID {id} not found.");

            await _reactionRepository.DeleteReaction(id);
            return true;
        }
    }
}
