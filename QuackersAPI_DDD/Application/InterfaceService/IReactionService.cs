using QuackersAPI_DDD.API.DTO.ReactionDTO;
using QuackersAPI_DDD.Domain.Model;

namespace QuackersAPI_DDD.Application.InterfaceService
{
    public interface IReactionService
    {
        Task<IEnumerable<Reaction>> GetAllReactions();
        Task<Reaction> GetReactionById(int id);
        Task<Reaction> CreateReaction(CreateReactionDTO reactionDto);
        Task<Reaction> UpdateReaction(int id, UpdateReactionDTO reactionDto);
        Task<bool> DeleteReaction(int id);
    }
}
