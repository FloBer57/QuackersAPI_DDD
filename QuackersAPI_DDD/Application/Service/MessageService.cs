namespace QuackersAPI_DDD.Application.Service
{
    using global::QuackersAPI_DDD.API.DTO.MessageDTO;
    using global::QuackersAPI_DDD.Application.InterfaceService;
    using global::QuackersAPI_DDD.Domain.Model;
    using global::QuackersAPI_DDD.Infrastructure.InterfaceRepository;
    using global::QuackersAPI_DDD.Infrastructure.Repository;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    namespace QuackersAPI_DDD.Application.Service
    {
        public class MessageService : IMessageService
        {
            private readonly IMessageRepository _messageRepository;
            private readonly IChannelRepository _channelRepository;
            private readonly IPersonRepository _personRepository;

            public MessageService(IMessageRepository messageRepository, IChannelRepository channelRepository, IPersonRepository personRepository)
            {
                _messageRepository = messageRepository;
                _channelRepository = channelRepository;
                _personRepository = personRepository;
            }

            public async Task<IEnumerable<Message>> GetAllMessages()
            {
                return await _messageRepository.GetAllMessages();
            }

            public async Task<Message> GetMessageById(int messageId)
            {
                return await _messageRepository.GetMessageById(messageId)
                    ?? throw new KeyNotFoundException($"Message with id {messageId} not found.");
            }

            public async Task<Message> CreateMessage(CreateMessageDTO dto)
            {
                var channel = await _channelRepository.GetChannelById(dto.ChannelId);
                if (channel == null)
                {
                    throw new KeyNotFoundException($"Channel with id {dto.ChannelId} not found.");
                }

                var person = await _personRepository.GetPersonById(dto.PersonId);
                if (person == null)
                {
                    throw new KeyNotFoundException($"Person with id {dto.PersonId} not found.");
                }

                var message = new Message
                {
                    Message_Text = dto.MessageText,
                    Message_Date = DateTime.Now,
                    Message_IsNotArchived = true,
                    Channel_Id = dto.ChannelId,
                    Person_Id = dto.PersonId
                };

                return await _messageRepository.CreateMessage(message);
            }

            public async Task<Message> UpdateMessage(int messageId, Message updatedMessage)
            {
                var message = await _messageRepository.GetMessageById(messageId);
                if (message == null)
                {
                    throw new KeyNotFoundException($"Message with id {messageId} not found.");
                }

                message.Message_Text = updatedMessage.Message_Text;
                message.Message_IsNotArchived = updatedMessage.Message_IsNotArchived;
                message.Message_Date = updatedMessage.Message_Date ?? message.Message_Date;

                return await _messageRepository.UpdateMessage(message);
            }

            public async Task<bool> DeleteMessage(int messageId)
            {
                var message = await _messageRepository.GetMessageById(messageId);
                if (message == null)
                {
                    return false;
                }

                await _messageRepository.DeleteMessage(messageId);
                return true;
            }
        }
    }

}
