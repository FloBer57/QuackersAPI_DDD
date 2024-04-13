using QuackersAPI_DDD.Domain.Model;

namespace QuackersAPI_DDD.Application.InterfaceService
{
    public interface IChannelPersonRoleService
    {
        Task<IEnumerable<ChannelPersonRole>> GetAllChannelPersonRoles();
        Task<ChannelPersonRole> GetChannelPersonRoleById(int id);
        Task<ChannelPersonRole> CreateChannelPersonRole(ChannelPersonRole channelPersonRole);
        Task<ChannelPersonRole> UpdateChannelPersonRole(int id, ChannelPersonRole channelPersonRole);
        Task<bool> DeleteChannelPersonRole(int id);
    }
}
