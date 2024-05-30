using Microsoft.EntityFrameworkCore;
using QuackersAPI_DDD.API.DTO.ChannelDTO;
using QuackersAPI_DDD.Application.Interface;
using QuackersAPI_DDD.Domain.Model;
using QuackersAPI_DDD.Infrastructure.InterfaceRepository;
using QuackersAPI_DDD.Application.InterfaceService;
using QuackersAPI_DDD.Infrastructure.Repository;
using QuackersAPI_DDD.API.DTO.ChannelTypeDTO;
using QuackersAPI_DDD.Application.Utilitie.InterfaceUtilitiesServices;

namespace QuackersAPI_DDD.Application.Service
{
    public class ChannelService : IChannelService
    {
        private readonly IChannelRepository _channelRepository;
        private readonly IChannelTypeService _channelTypeService;
        private readonly IMessageService _messageService;
        private readonly ISecurityService _securityService;
        public ChannelService(IChannelRepository channelRepository, IChannelTypeService channelTypeService, IMessageService messageService, ISecurityService securityService)
        {
            _channelRepository = channelRepository;
            _channelTypeService = channelTypeService;
            _messageService = messageService;
            _securityService = securityService;
        }

        public async Task<Channel> CreateChannel(CreateChannelDTO createChannelDTO)
        {
            var channelType = await _channelTypeService.GetChannelTypeById(createChannelDTO.ChannelType_Id);
            if (channelType == null)
            {
                throw new KeyNotFoundException($"ChannelType with id {createChannelDTO.ChannelType_Id} not found.");
            }

            if (await _channelRepository.ChannelNameExists(createChannelDTO.Channel_Name))
            {
                throw new InvalidOperationException($"A channel with the name '{createChannelDTO.Channel_Name}' already exists.");
            }

            // Créer le nouveau canal
            var channel = new Channel
            {
                Channel_Name = createChannelDTO.Channel_Name,
                Channel_ImagePath = "/Image/ChannelPicture/default.jpg",
                ChannelType_Id = createChannelDTO.ChannelType_Id, 
                ChannelType = channelType
            };

            var createdChannel = await _channelRepository.CreateChannel(channel);
            channelType.Channels.Add(createdChannel); 

            return createdChannel;
        }


        public async Task<IEnumerable<Channel>> GetAllChannels()
        {
            var channels = await _channelRepository.GetAllChannels();
            return channels ?? new List<Channel>(); 
        }


        public async Task<IEnumerable<Channel>> GetChannelsByChannelType(int channelTypeId)
        {
            var channelType = await _channelTypeService.GetChannelTypeById(channelTypeId);
            if (channelType == null)
            {
                throw new KeyNotFoundException($"ChannelType with id {channelTypeId} not found.");
            }

            var channels = await _channelRepository.GetChannelsByChannelType(channelTypeId);
            return channels ?? new List<Channel>(); 
        }

        public async Task<Channel> GetChannelById(int id)
        {
            var channel = await _channelRepository.GetChannelById(id);
            if (channel == null)
            {
                throw new KeyNotFoundException($"Channel with id {id} not found.");
            }
            return channel;
        }


        public async Task<Channel> UpdateChannel(int id, UpdateChannelDTO dto)
        {
            var existingChannel = await _channelRepository.GetChannelById(id);

            if (existingChannel == null)
            {
                throw new KeyNotFoundException($"Channel with id {id} not found.");
            }

            if (dto.Channel_Name != null && dto.Channel_Name != existingChannel.Channel_Name)
            {
                if (await _channelRepository.ChannelNameExists(dto.Channel_Name))
                {
                    throw new InvalidOperationException($"A channel with the name '{dto.Channel_Name}' already exists.");
                }
                existingChannel.Channel_Name = dto.Channel_Name;
            }

            if (dto.Channel_ImagePath != null)
            {
                existingChannel.Channel_ImagePath = dto.Channel_ImagePath;
            }

            if (dto.ChannelType_Id != null)
            {
                var channelType = await _channelTypeService.GetChannelTypeById((int)dto.ChannelType_Id);
                if (channelType == null)
                {
                    throw new KeyNotFoundException($"ChannelType with id {dto.ChannelType_Id} not found.");
                }
                existingChannel.ChannelType_Id = (int)dto.ChannelType_Id;
            }

            return await _channelRepository.UpdateChannel(existingChannel);
        }

        public async Task<bool> DeleteChannel(int id)
        {
            var channel = await _channelRepository.GetChannelById(id);
            if (channel == null)
            {
                throw new KeyNotFoundException($"Channel with id {id} not found.");
            }

            await _channelRepository.DeleteChannel(channel);
            return true;
        }

        public async Task<IEnumerable<Message>> GetAllMessagesFromChannel(int channelId)
        {

            var messages = await _messageService.GetMessagesByChannelId(channelId);
            if (messages == null)
            {
                throw new KeyNotFoundException($"Messages on this channel not found");
            }
            return messages;
        }
    }
}
