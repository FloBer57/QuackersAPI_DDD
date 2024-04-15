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
            _reactionRepository = reactionRepository;
        }

        public async Task<IEnumerable<Reaction>> GetAllReactions()
        {
            return await _reactionRepository.GetAllReactions();
        }

        public async Task<Reaction> GetReactionById(int id)
        {
            return await _reactionRepository.GetReactionById(id);
        }

        public async Task<Reaction> CreateReaction(CreateReactionDTO reactionDto)
        {
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
            {
                throw new KeyNotFoundException("Reaction not found.");
            }

            reaction.Reaction_Name = reactionDto.ReactionName;
            reaction.Reaction_PicturePath = reactionDto.ReactionPicturePath;

            return await _reactionRepository.UpdateReaction(reaction);
        }

        public async Task<bool> DeleteReaction(int id)
        {
            return await _reactionRepository.DeleteReaction(id);
        }
    }
}
