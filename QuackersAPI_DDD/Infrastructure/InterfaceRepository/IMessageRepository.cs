using QuackersAPI_DDD.Domain.Model;

namespace QuackersAPI_DDD.Infrastructure.InterfaceRepository
{
    public interface IMessageRepository
    {
        Task CreateMessageOnChannel(Message message);
        Task<Message> GetMessageById(int id);
        Task<IEnumerable<Message>> GetAllMessageByChannelId();
        Task<IEnumerable<Message>> GetAllMessageByPerson();
        Task UpdateMessage(Message message);
        Task DeleteMessage(int id);
        Task DeleteAllMessageFromChannel(int id);
    }
}
