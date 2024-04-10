using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QuackersAPI_DDD.Application.DTO;
using QuackersAPI_DDD.Application.DTO.ChannelFolderDTO.Request;
using QuackersAPI_DDD.Application.DTO.ChannelFolderDTO.Response;
using QuackersAPI_DDD.Application.DTO.PersonFolderDTO;
using QuackersAPI_DDD.Application.DTO.PersonFolderDTO.ChannelFolderDTO;
using QuackersAPI_DDD.Application.Interface;
using QuackersAPI_DDD.Domain.Model;
using QuackersAPI_DDD.Domain.Utilitie;
using QuackersAPI_DDD.Infrastructure.InterfaceRepository;

namespace QuackersAPI_DDD.Application.Service
{
    public class ChannelService : IChannelService
    {
        private readonly IChannelRepository _channelRepository;

        public ChannelService(IChannelRepository channelRepository)
        {
            _channelRepository = channelRepository;
        }

        public async Task<Channel> CreateChannel(Channel channel)
        {
            return await _channelRepository.CreateChannel(channel);
        }

        public async Task<IEnumerable<Channel>> GetAllChannels()
        {
            return await _channelRepository.GetAllChannels();
        }

        public async Task<Channel> GetChannelById(int id)
        {
            return await _channelRepository.GetChannelById(id);
        }

        public async Task<Channel> UpdateChannel(int id, Channel updatedChannel)
        {
            var channel = await _channelRepository.GetChannelById(id);
            if (channel == null)
            {
                throw new InvalidOperationException($"Channel with id {id} not found.");
            }

            channel.Channel_Name = updatedChannel.Channel_Name;
            channel.Channel_ImagePath = updatedChannel.Channel_ImagePath;
            channel.ChannelType_Id = updatedChannel.ChannelType_Id;

            // Assume UpdateChannel method exists in the repository to update the channel
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
