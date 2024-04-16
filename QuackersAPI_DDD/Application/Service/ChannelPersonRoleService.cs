using QuackersAPI_DDD.API.DTO.ChannelPersonRoleDTO;
using QuackersAPI_DDD.Application.InterfaceService;
using QuackersAPI_DDD.Domain.Model;
using QuackersAPI_DDD.Infrastructure.InterfaceRepository;

namespace QuackersAPI_DDD.Application.Service
{
    public class ChannelPersonRoleService : IChannelPersonRoleService
    {
        private readonly IChannelPersonRoleRepository _channelPersonRoleRepository;

        public ChannelPersonRoleService(IChannelPersonRoleRepository channelPersonRoleRepository)
        {
            _channelPersonRoleRepository = channelPersonRoleRepository;
        }

        public async Task<IEnumerable<ChannelPersonRole>> GetAllChannelPersonRoles()
        {
            return await _channelPersonRoleRepository.GetAllChannelPersonRoles();
        }

        public async Task<ChannelPersonRole> GetChannelPersonRoleById(int id)
        {
            return await _channelPersonRoleRepository.GetChannelPersonRoleById(id);
        }

        public async Task<ChannelPersonRole> CreateChannelPersonRole(CreateChannelPersonRoleDTO dto)
        {
            if (await _channelPersonRoleRepository.ChannelPersonRoleNameExists(dto.ChannelPersonRole_Name))
            {
                throw new InvalidOperationException("A channel person role with the same name already exists.");
            }

            var newRole = new ChannelPersonRole
            {
                ChannelPersonRole_Name = dto.ChannelPersonRole_Name
            };

            return await _channelPersonRoleRepository.CreateChannelPersonRole(newRole);
        }

        public async Task<ChannelPersonRole> UpdateChannelPersonRole(int id, UpdateChannelPersonRoleDTO dto)
        {
            var channelPersonRole = await _channelPersonRoleRepository.GetChannelPersonRoleById(id);
            if (channelPersonRole == null)
            {
                return null;
            }
            if (await _channelPersonRoleRepository.ChannelPersonRoleNameExists(dto.ChannelPersonRole_Name))
            {
                throw new InvalidOperationException("A channel person role with the same name already exists.");
            }
            channelPersonRole.ChannelPersonRole_Name = dto.ChannelPersonRole_Name;
            return await _channelPersonRoleRepository.UpdateChannelPersonRole(channelPersonRole);
        }

        public async Task<bool> DeleteChannelPersonRole(int id)
        {
            var role = await _channelPersonRoleRepository.GetChannelPersonRoleById(id);
            if (role == null)
            {
                return false;
            }

            await _channelPersonRoleRepository.DeleteChannelPersonRole(id);
            return true;
        }
    }
}
