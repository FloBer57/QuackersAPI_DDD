using QuackersAPI_DDD.Domain.Model;

namespace QuackersAPI_DDD.Infrastructure.InterfaceRepository
{
    public interface IChannelPersonRoleRepository
    {
        Task<IEnumerable<ChannelPersonRole>> GetAllChannelPersonRoles();
        Task<ChannelPersonRole> GetChannelPersonRoleById(int id);
        Task<ChannelPersonRole> CreateChannelPersonRole(ChannelPersonRole channelPersonRole);
        Task<ChannelPersonRole> UpdateChannelPersonRole(ChannelPersonRole channelPersonRole);
        Task DeleteChannelPersonRole(int id);
    }
}
