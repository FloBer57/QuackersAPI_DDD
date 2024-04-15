using QuackersAPI_DDD.API.DTO.MessageDTO;
using QuackersAPI_DDD.Domain.Model;

namespace QuackersAPI_DDD.Application.InterfaceService
{
    public interface IMessageService
    {
        Task<IEnumerable<Message>> GetAllMessages();
        Task<Message> GetMessageById(int messageId);
        Task<Message> CreateMessage(CreateMessageDTO dto);
        Task<Message> UpdateMessage(int messageId, Message message);
        Task<bool> DeleteMessage(int messageId);
    }
}
