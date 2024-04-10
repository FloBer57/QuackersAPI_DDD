namespace QuackersAPI_DDD.Application.Interface
{
    using QuackersAPI_DDD.Domain.Model;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IChannelService
    {
        Task<Channel> CreateChannel(Channel channel);
        Task<IEnumerable<Channel>> GetAllChannels();
        Task<Channel> GetChannelById(int id);
        Task<Channel> UpdateChannel(int id, Channel channel);
        Task<bool> DeleteChannel(int id);
    }
}
