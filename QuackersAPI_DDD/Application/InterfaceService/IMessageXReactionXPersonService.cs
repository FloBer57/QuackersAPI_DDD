using QuackersAPI_DDD.API.DTO.MessagexreactionxpersonDTO;
using QuackersAPI_DDD.Domain.Model;

namespace QuackersAPI_DDD.Application.InterfaceService
{
    public interface IMessageXReactionXPersonService
    {
        Task<IEnumerable<MessageXReactionXPerson>> GetAllReactions();
        Task<MessageXReactionXPerson> GetReactionById(int personId, int messageId, int reactionId);
        Task<MessageXReactionXPerson> CreateReaction(CreateMessageXReactionXPersonDTO dto);
        Task<MessageXReactionXPerson> UpdateReaction(int personId, int messageId, int reactionId, UpdateMessageXReactionXPersonDTO dto);
        Task<bool> DeleteReaction(int personId, int messageId, int reactionId);
        Task<Dictionary<string, int>> GetReactionCountsByMessageId(int messageId);
    }
}
