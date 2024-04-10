namespace QuackersAPI_DDD.Infrastructure.InterfaceRepository
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using QuackersAPI_DDD.Domain.Model;

    public interface IChannelRepository
    {
        Task<Channel> CreateChannel(Channel channel);
        Task<IEnumerable<Channel>> GetAllChannels();
        Task<Channel> GetChannelById(int id);
        Task<Channel> UpdateChannel(Channel channel);
        Task DeleteChannel(Channel channel);
    }
}
