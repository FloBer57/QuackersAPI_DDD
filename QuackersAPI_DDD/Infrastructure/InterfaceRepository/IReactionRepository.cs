using QuackersAPI_DDD.Domain.Model;

namespace QuackersAPI_DDD.Infrastructure.InterfaceRepository
{
    public interface IReactionRepository
    {
        Task<IEnumerable<Reaction>> GetAllReactions();
        Task<Reaction> GetReactionById(int id);
        Task<Reaction> CreateReaction(Reaction reaction);
        Task<Reaction> UpdateReaction(Reaction reaction);
        Task<bool> DeleteReaction(int id);
    }
}
