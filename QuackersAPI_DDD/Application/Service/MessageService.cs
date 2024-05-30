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
            private readonly IAttachmentRepository _attachmentRepository;

            public MessageService(IMessageRepository messageRepository, IChannelRepository channelRepository, IPersonRepository personRepository, IAttachmentRepository attachmentRepository)
            {
                _messageRepository = messageRepository;
                _channelRepository = channelRepository;
                _personRepository = personRepository;
                _attachmentRepository = attachmentRepository;
            }

            public async Task<IEnumerable<Message>> GetAllMessages()
            {
                var message = await _messageRepository.GetAllMessages();
                return message ?? new List<Message>();
            }

            public async Task<Message> GetMessageById(int messageId)
            {
                var message = await _messageRepository.GetMessageById(messageId);
                if (message == null)
                {
                    throw new KeyNotFoundException($"Message with id {messageId} not found.");
                }
                return message;
            }
            public async Task<IEnumerable<Attachment>> GetMessageAttachments(int messageId)
            {
                var message = await _messageRepository.GetMessageById(messageId);
                if (message == null)
                {
                    throw new KeyNotFoundException($"Message with id {messageId} not found.");
                }
                return await _attachmentRepository.GetAttachmentsByMessageId(messageId);
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
                    Message_HasAttachment = dto.Message_HasAttachment,
                    Channel_Id = dto.ChannelId,
                    Person_Id = dto.PersonId
                };

                return await _messageRepository.CreateMessage(message);
            }

            public async Task<Message> UpdateMessage(int messageId, UpdateMessageDTO dto)
            {
                var message = await _messageRepository.GetMessageById(messageId);
                if (message == null)
                {
                    throw new KeyNotFoundException($"Message with id {messageId} not found.");
                }

                message.Message_Text = dto.Message_Text ?? message.Message_Text;
                if (dto.Message_HasAttachment.HasValue)
                {
                    message.Message_HasAttachment = dto.Message_HasAttachment.Value;
                }

                return await _messageRepository.UpdateMessage(message);
            }

            public async Task<bool> DeleteMessage(int messageId)
            {
                var message = await _messageRepository.GetMessageById(messageId);
                if (message == null)
                {
                    throw new KeyNotFoundException($"Message with id {messageId} not found.");
                }

                await _messageRepository.DeleteMessage(messageId);
                return true;
            }

            public async Task<IEnumerable<Message>> GetMessagesByChannelId(int channelId)
            {
                var messages = await _messageRepository.GetMessagesByChannelId(channelId);
                if (messages == null)
                {
                    throw new KeyNotFoundException($"no message found in the channel with id {channelId}");
                }
                return messages;
            }
        }
    }

}
