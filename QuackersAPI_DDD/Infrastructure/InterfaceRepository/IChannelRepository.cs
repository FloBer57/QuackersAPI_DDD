namespace QuackersAPI_DDD.Infrastructure.InterfaceRepository
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using QuackersAPI_DDD.Domain.Model;

    public interface IChannelRepository
    {
        Task CreateChannel(Channel channel);
        Task<Channel> GetChannelById(int id);
        Task<IEnumerable<Channel>> GetAllChannel();
        Task UpdateChannel(Channel channel);
        Task DeleteChannel(int id);
    }
}
