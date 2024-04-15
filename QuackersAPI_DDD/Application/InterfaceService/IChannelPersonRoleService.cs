using QuackersAPI_DDD.API.DTO.ChannelPersonRoleDTO;
using QuackersAPI_DDD.Domain.Model;

namespace QuackersAPI_DDD.Application.InterfaceService
{
    public interface IChannelPersonRoleService
    {
        Task<IEnumerable<ChannelPersonRole>> GetAllChannelPersonRoles();
        Task<ChannelPersonRole> GetChannelPersonRoleById(int id);
        Task<ChannelPersonRole> CreateChannelPersonRole(CreateChannelPersonRoleDTO dto);
        Task<ChannelPersonRole> UpdateChannelPersonRole(int id, UpdateChannelPersonRoleDTO dto);
        Task<bool> DeleteChannelPersonRole(int id);
    }
}
