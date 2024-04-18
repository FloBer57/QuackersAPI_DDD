using QuackersAPI_DDD.Domain.Model;

namespace QuackersAPI_DDD.Infrastructure.InterfaceRepository
{
    public interface IMessageXReactionXPersonRepository
    {
        Task<IEnumerable<MessageXReactionXPerson>> GetAllReactions();
        Task<MessageXReactionXPerson> GetReactionById(int personId, int messageId, int reactionId);
        Task<MessageXReactionXPerson> CreateReaction(MessageXReactionXPerson reaction);
        Task<MessageXReactionXPerson> UpdateReaction(MessageXReactionXPerson reaction);
        Task<bool> DeleteReaction(int personId, int messageId, int reactionId);
        Task<IEnumerable<MessageXReactionXPerson>> GetReactionsByMessageId(int messageId);
    }
}
