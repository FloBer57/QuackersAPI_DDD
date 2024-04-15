using QuackersAPI_DDD.Domain.Model;

namespace QuackersAPI_DDD.Infrastructure.InterfaceRepository
{
    public interface IMessageRepository
    {
        Task<IEnumerable<Message>> GetAllMessages();
        Task<Message> GetMessageById(int messageId);
        Task<Message> CreateMessage(Message message);
        Task<Message> UpdateMessage(Message message);
        Task DeleteMessage(int messageId);
    }
}
