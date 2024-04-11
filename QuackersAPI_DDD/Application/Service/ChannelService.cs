using Microsoft.EntityFrameworkCore;
using QuackersAPI_DDD.API.DTO.ChannelDTO;
using QuackersAPI_DDD.Application.Interface;
using QuackersAPI_DDD.Domain.Model;
using QuackersAPI_DDD.Infrastructure.InterfaceRepository;
using QuackersAPI_DDD.Application.InterfaceService;
using QuackersAPI_DDD.Infrastructure.Repository;
using QuackersAPI_DDD.API.DTO.ChannelTypeDTO;

namespace QuackersAPI_DDD.Application.Service
{
    public class ChannelService : IChannelService
    {
        private readonly IChannelRepository _channelRepository;
        private readonly IChannelTypeService _channelTypeService;

        public ChannelService(IChannelRepository channelRepository, IChannelTypeService channelTypeService)
        {
            _channelRepository = channelRepository;
            _channelTypeService = channelTypeService;
        }

        public async Task<Channel> CreateChannel(CreateChannelDTO createChannelDTO)
        {
            var defaultChannelTypeId = 1; // ID du type de canal par défaut
            var channelType = await _channelTypeService.GetChannelTypeById(defaultChannelTypeId);
            if (channelType == null)
            {
                throw new InvalidOperationException($"ChannelType with id {defaultChannelTypeId} not found.");
            }

            // Créer le nouveau canal
            var channel = new Channel
            {
                Channel_Name = createChannelDTO.Channel_Name,
                Channel_ImagePath = createChannelDTO.Channel_ImagePath,
                ChannelType_Id = defaultChannelTypeId,
                ChannelType = channelType
            };

            var createdChannel = await _channelRepository.CreateChannel(channel);

            channelType.Channels.Add(createdChannel);

            return createdChannel;
        }


        public async Task<IEnumerable<Channel>> GetAllChannels()
        {
            return await _channelRepository.GetAllChannels();
        }

        public async Task<IEnumerable<Channel>> GetChannelsByChannelType(int channelTypeId)
        {
            return await _channelRepository.GetChannelsByChannelType(channelTypeId);
        }


        public async Task<Channel> GetChannelById(int id)
        {
            return await _channelRepository.GetChannelById(id);
        }

        public async Task<Channel> UpdateChannel(int id, UpdateChannelDTO updateChannelDTO)
        {
            var channel = await _channelRepository.GetChannelById(id);
            if (channel == null)
            {
                throw new InvalidOperationException($"Channel with id {id} not found.");
            }

            channel.Channel_Name = updateChannelDTO.Channel_Name ?? channel.Channel_Name;
            channel.Channel_ImagePath = updateChannelDTO.Channel_ImagePath ?? channel.Channel_ImagePath;

            return await _channelRepository.UpdateChannel(channel);
        }

        public async Task<bool> DeleteChannel(int id)
        {
            var channel = await _channelRepository.GetChannelById(id);
            if (channel == null)
            {
                return false;
            }

            await _channelRepository.DeleteChannel(channel);
            return true;
        }
    }
}
