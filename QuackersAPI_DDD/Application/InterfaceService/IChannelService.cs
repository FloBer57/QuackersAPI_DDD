namespace QuackersAPI_DDD.Application.Interface
{
    using QuackersAPI_DDD.API.DTO.ChannelDTO;
    using QuackersAPI_DDD.Domain.Model;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IChannelService
    {
        Task<Channel> CreateChannel(CreateChannelDTO channel);
        Task<IEnumerable<Channel>> GetChannelsByChannelType(int channelTypeId);
        Task<IEnumerable<Channel>> GetAllChannels();
        Task<Channel> GetChannelById(int id);
        Task<Channel> UpdateChannel(int id, UpdateChannelDTO channel);
        Task<bool> DeleteChannel(int id);
        Task<IEnumerable<Message>> GetAllMessagesFromChannel(int channelId);
    }
}
